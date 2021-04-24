module Morabaraba.Core.Placement

open Board
open Turn
open Player
open PlayingHelpers

let place junction history =
    validateJunction junction
    let occupations, player = getTurn history

    match getPhase history with
    | Placing ->
        let occupations = occupy junction player.Shade occupations
        let player = decrementHand player
        Result.bind (occupationBinder history player) occupations
    | _ -> Error UnexpectedOccupation
