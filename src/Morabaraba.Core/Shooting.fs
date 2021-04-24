module Morabaraba.Core.Shooting

open Board
open PlayingHelpers

let shoot target history =
    validateJunction target

    if canShoot target history then
        match history with
        | [] -> Error UnexpectedEmptying
        | { Occupations = occupations
            Player = player } :: history ->
            let occupations = empty target occupations
            Result.bind (occupationBinder history player) occupations
    else
        Error UnexpectedEmptying
