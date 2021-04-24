module Morabaraba.Core.Playing

open Board
open Placement
open Movement
open PlayingHelpers

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
    