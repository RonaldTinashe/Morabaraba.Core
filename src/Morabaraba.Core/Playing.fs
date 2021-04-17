module Morabaraba.Core.Playing

open Initialisation

let occupy junction shade occupations = Map.add junction shade occupations

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

let filterMills occupations player line = 
    List.forall 
        (fun junction -> 
            Map.containsKey junction occupations &&
            occupations.[junction] = player.Shade)
        line

let getMills history =
    match history with
    | [] -> []
    | { Occupations = occupations; Player = player } :: _ ->
        List.filter (filterMills occupations player) lines

let getDefenceMills history =
    match history with
    | [] -> []
    | _ :: defenceHistory -> getMills defenceHistory

let getShootingMills history =
    match history with
    | [] -> []
    | { Occupations = occupations; Player = player } :: 
        { Occupations = previousOccupations } :: _ ->
        let currentMills = List.filter (filterMills occupations player) lines
        let previousMills = 
            List.filter (filterMills previousOccupations player) lines
        List.except previousMills currentMills
    | history -> getMills history

let place junction history =
    validate junction
    let event =
        let occupations, player = getTurn history
        if player.Cows > 0 then
            {
                Occupations = occupy junction player.Shade occupations
                Player = { player with Cows = player.Cows - 1 }
            } |> Ok
        else Error UnexpectedPlacement
    Result.bind (fun event -> event :: history |> Ok ) event

let shoot target history =
    if getShootingMills history |> List.isEmpty ||
        getDefenceMills history |> List.exists (List.contains target) then
        Error UnexpectedShot 
    else
        match history with
        | [] -> Error UnexpectedShot
        | { Occupations = occupations } as event :: history ->
            let occupations = Map.remove target occupations
            Ok ({ event with Occupations = occupations } :: history)

let play move history =
    match move with
    | { Main = Placement junction; Shot = None } -> place junction history
    | { Main = Placement junction; Shot = Some target } ->
        let history = place junction history
        Result.bind (shoot target) history
