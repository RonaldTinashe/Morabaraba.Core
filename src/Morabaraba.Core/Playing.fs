module Morabaraba.Core.Playing

open Initialisation

let occupy junction shade occupations = 
    if Map.containsKey junction occupations then Error UnexpectedOccupation
    else Map.add junction shade occupations |> Ok

let empty target occupations = 
    if Map.containsKey target occupations then 
        Map.remove target occupations |> Ok
    else Error UnexpectedEmptying

let getPlayer history = 
    match history with
    | [] -> initial.DarkPlayer
    | [_] -> initial.LightPlayer
    | { Player = { Shade = Dark }} :: { Player = player } :: _
    | { Player = { Shade = Light }} :: { Player = player } :: _ -> player

let getOccupations history =
    match history with
    | [] -> initial.Occupations
    | event :: _ -> event.Occupations

let getTurn history = getOccupations history, getPlayer history

let decrementHand player = { player with Cows = player.Cows - 1 }

let validate junction = 
    if junction > 0 && junction < 25 then ()
    else invalidArg "junction" <| sprintf "Value passed is %i" junction

let lines =
    let rows =
        seq 
            { 
                for start in seq { 1 .. 3 .. 24 } do 
                    [start; start + 1; start + 2] 
            }
    let columns =
        [
            [ 1; 10; 22 ] // starts at outer ring
            [ 2; 5; 8 ]
            [ 3; 15; 24 ]
            [ 4; 11; 19 ] // starts at mid ring
            [ 6; 14; 21 ]
            [ 7; 12; 16 ] // starts at inner ring
            [ 9; 13; 18 ]
            [ 17; 20; 23 ]
        ]
    let diagonals =
        seq
            { 
                for start in [1; 3; 16] do
                    [start; start + 3; start + 6]
            }
    seq { rows; columns; diagonals } |> Seq.concat |> List.ofSeq

let filterMills occupations shade line = 
    List.forall 
        (fun junction -> 
            Map.containsKey junction occupations &&
            occupations.[junction] = shade)
        line

let getMills history shade =
    match history with
    | [] -> []
    | { Occupations = occupations } :: _ -> 
        List.filter (filterMills occupations shade) lines

let getDefenceMills history =
    match history with
    | [] -> []
    | [_] -> []
    | _ :: ({ Player = defender } :: _ as defenceHistory) -> 
        getMills defenceHistory defender.Shade

let getShootingMills history =
    match history with
    | [] -> []
    | { Player = player } :: previousHistory ->
        let currentMills = getMills history player.Shade
        let previousMills = getMills previousHistory player.Shade
        List.except previousMills currentMills

let getJunctionsInDefenceMills history = 
    getDefenceMills history |> 
    List.concat |>
    List.distinct |>
    List.sort

let getDefenceJunctions history =
    match history with
    | []
    | [_] -> []
    | { Occupations = occupations } :: { Player = { Shade = shade }} :: _ ->
        Map.filter (fun _ cow -> cow = shade) occupations |>
        Map.toList |>
        List.map (fun (junction, _) -> junction) |>
        List.sort

let areNeighbours junction1 junction2 =
    let areNextToEachOther line = 
        match line with
        | [a; b; _]
        | [_; a; b] -> 
            a = junction1 && b = junction2 ||
            a = junction2 && b = junction1
        | _ -> false
    List.exists areNextToEachOther lines

let areAllDefenceJunctionsInMills history =
    getJunctionsInDefenceMills history = getDefenceJunctions history

let canShoot target history =
    getShootingMills history |> List.isEmpty ||
    getDefenceMills history |> List.exists (List.contains target) &&
    areAllDefenceJunctionsInMills history |> not

let isPlayerMovingOwnCow history source =
    let occupations, player = getTurn history
    Some player.Shade = Map.tryFind source occupations

let occupationBinder history player occupations =
    let event = { Occupations = occupations; Player = player }
    event :: history |> Ok

let phase history =
    let occupations, player = getTurn history
    if player.Cows > 0 then Placing
    else if player.Cows = 0 then
        let cowCount =
            occupations |> 
            Map.filter (fun _ shade -> player.Shade = shade) |> 
            Map.count
        if cowCount > 3 then Moving
        else Flying
    else invalidArg "player" "has less than 0 cows on their hand"

let place junction history =
    validate junction
    let occupations, player = getTurn history
    match phase history with
    | Placing ->
        let occupations = occupy junction player.Shade occupations
        let player = decrementHand player
        Result.bind (occupationBinder history player) occupations
    | _ -> Error UnexpectedOccupation

let shoot target history =
    validate target
    if canShoot target history then Error UnexpectedEmptying 
    else
        match history with
        | [] -> Error UnexpectedEmptying
        | { Occupations = occupations; Player = player } :: history ->
            let occupations = empty target occupations
            Result.bind (occupationBinder history player) occupations

let rawMove source destination history =
    let occupations, player = getTurn history
    let emptiedOccupations = empty source occupations
    let occupiedOccupations =
        Result.bind (occupy destination player.Shade) emptiedOccupations
    Result.bind (occupationBinder history player) occupiedOccupations

let move source destination history =
    validate source
    validate destination
    let validMove =
        match
            phase history, 
            areNeighbours source destination,
            isPlayerMovingOwnCow history source with
        | Moving, true, true
        | Flying, _, true -> Ok ()
        | Moving, false, _ -> Error UnexpectedOccupation
        | _ -> Error UnexpectedEmptying
    Result.bind (fun () -> rawMove source destination history) validMove
        
let play move' history =
    let history =
        match move'.Main with
        | Placement junction -> place junction history
        | Movement (source, destination) -> move source destination history
    match move'.Shot with
    | Some target -> Result.bind (shoot target) history
    | None -> history
