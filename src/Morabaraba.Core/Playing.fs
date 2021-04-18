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

let areAllDefenceJunctionsInMills history =
    getJunctionsInDefenceMills history = getDefenceJunctions history

let canShoot target history =
    getShootingMills history |> List.isEmpty ||
    getDefenceMills history |> List.exists (List.contains target) &&
    areAllDefenceJunctionsInMills history |> not

let place junction history =
    validate junction
    let event =
        let occupations, player = getTurn history
        if player.Cows > 0 then
            let occupations = occupy junction player.Shade occupations
            let occupationBinder occupations =
                {
                    Occupations = occupations
                    Player = decrementHand player
                } |> Ok
            Result.bind occupationBinder occupations
        else Error UnexpectedOccupation
    Result.bind (fun event -> event :: history |> Ok ) event

let shoot target history =
    validate target
    if canShoot target history then Error UnexpectedEmptying 
    else
        match history with
        | [] -> Error UnexpectedEmptying
        | { Occupations = occupations } as event :: history ->
            let occupations = empty target occupations
            let occupationBinder event history occupations =
                { event with Occupations = occupations } :: history |> Ok
            Result.bind (occupationBinder event history) occupations

let move source destination history =
    match history with
    | [] -> Error UnexpectedEmptying
    | history ->
        let occupations, player = getTurn history
        let emptiedOccupations = empty source occupations
        let occupiedOccupations =
            Result.bind (occupy destination player.Shade) emptiedOccupations
        Result.bind 
            (fun occupiedOccupations -> 
                {
                    Occupations = occupiedOccupations
                    Player = player
                } :: history |> Ok)
            occupiedOccupations
        
let play move' history =
    match move' with
    | { Main = Placement junction; Shot = None } -> place junction history
    | { Main = Placement junction; Shot = Some target } ->
        let history = place junction history
        Result.bind (shoot target) history
    | { Main = Movement (source, destination); Shot = None } ->
        move source destination history
    | { Main = Movement (source, destination); Shot = Some target } ->
        let history = move source destination history
        Result.bind (shoot target) history
