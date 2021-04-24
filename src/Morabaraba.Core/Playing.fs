module Morabaraba.Core.Playing

open Initialisation
open Board

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

let rawMove source destination history =
    let occupations, player = getTurn history
    let emptiedOccupations = empty source occupations
    let occupiedOccupations =
        Result.bind (occupy destination player.Shade) emptiedOccupations
    Result.bind (occupationBinder history player) occupiedOccupations

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
