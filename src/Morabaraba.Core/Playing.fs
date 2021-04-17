module Morabaraba.Core.Playing

open Initialisation

/// Applies move with historical context
let play { Main = mainMove } history =
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
    match mainMove, history with
    // First move
    | Placement junction, [] ->
        let history =
            {
                Occupations = occupy junction Dark <| getOccupations history
                Player = { getPlayer history with Cows = 11 }
            } :: history
        Some history
    // Dark player's second turn
    | Placement junction,
        { Occupations = occupations; Player = { Shade = Light }} :: _ ->
        let history =
            {
                Occupations = occupy junction Dark occupations
                Player = { getPlayer history with Cows = 10 }
            } :: history
        Some history
    // Light player's turn
    | Placement junction, _ ->
        let history =
            let occupations = getOccupations history
            let player = getPlayer history
            {
                Occupations = occupy junction player.Shade occupations
                Player = { player with Cows = 11 }
            } :: history
        Some history
    