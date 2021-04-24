module Morabaraba.Core.Movement

open Board
open Turn
open Player
open PlayingHelpers

let isPlayerMovingOwnCow history source =
    let occupations, player = getTurn history
    Some player.Shade = Map.tryFind source occupations

let isLegalMove history source destination =
    let _, player = getTurn history
    let playerHistory = List.filter (fun event -> player = event.Player) history
    match playerHistory with
    | _ :: oldEvents ->
        let oldMills = getMills oldEvents player.Shade
        let recentMills = getMills history player.Shade
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
