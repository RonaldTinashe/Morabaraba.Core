module Morabaraba.Core.Playing

open Board
open Turn
open Player
open Movement
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

let shoot target history =
    validateJunction target
    if canShoot target history then
        match history with
        | [] -> Error UnexpectedEmptying
        | { Occupations = occupations; Player = player } :: history ->
            let occupations = empty target occupations
            Result.bind (occupationBinder history player) occupations 
    else Error UnexpectedEmptying


        
let play move' history =
    let history =
        match move'.Main with
        | Placement junction -> place junction history
        | Movement (source, destination) -> move source destination history
    match move'.Shot with
    | Some target -> Result.bind (shoot target) history
    | None -> history
    