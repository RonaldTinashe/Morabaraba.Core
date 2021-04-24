module Morabaraba.Core.Playing

open Board
open Turn
open Player

let isPlayerMovingOwnCow history source =
    let occupations, player = getTurn history
    Some player.Shade = Map.tryFind source occupations

let occupationBinder history player occupations =
    let event = { Occupations = occupations; Player = player }
    event :: history |> Ok

let isLegalMove history source destination =
    match history with
    | _ :: _ :: oldEvents ->
        let _, { Shade = shade } = getTurn history
        let oldMills = getMills oldEvents shade
        let recentMills = getMills history shade
        let destinationWasInMill = isInMill destination oldMills
        let destinationIsInMill = isInMill destination recentMills
        let millWasBroken = destinationWasInMill && not destinationIsInMill
        let sourceIsInMIll = isInMill source recentMills
        (millWasBroken && sourceIsInMIll) |> not
    | _ -> true

let rawMove source destination history =
    let occupations, player = getTurn history
    let emptiedOccupations = empty source occupations
    let occupiedOccupations =
        Result.bind (occupy destination player.Shade) emptiedOccupations
    Result.bind (occupationBinder history player) occupiedOccupations

let place junction history =
    validateJunction junction
    let occupations, player = getTurn history
    match getPhase history with
    | Placing ->
        let occupations = occupy junction player.Shade occupations
        let player = decrementHand player
        Result.bind (occupationBinder history player) occupations
    | _ -> Error UnexpectedOccupation

let shoot target history =
    validateJunction target
    if canShoot target history then
        match history with
        | [] -> Error UnexpectedEmptying
        | { Occupations = occupations; Player = player } :: history ->
            let occupations = empty target occupations
            Result.bind (occupationBinder history player) occupations 
    else Error UnexpectedEmptying

let move source destination history =
    validateJunction source
    validateJunction destination
    let validMove =
        match
            getPhase history, 
            areNeighbours source destination,
            isPlayerMovingOwnCow history source,
            isLegalMove history source destination with
        | Moving, true, true, true
        | Flying, _, true, _ -> Ok ()
        | Moving, _, _, false
        | Moving, false, _, _-> Error UnexpectedOccupation
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
    