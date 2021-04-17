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
    // Dark player's second turn
    | Placement junction, { Player = { Shade = Light }} :: _ ->
        let history =
            let occupations = getOccupations history
            let player = getPlayer history
            {
                Occupations = occupy junction player.Shade occupations
                Player = { player with Cows = 10 }
            } :: history
        Some history
    // All turns but the dark player's second turn
    | Placement junction, _ ->
        let history =
            let occupations = getOccupations history
            let player = getPlayer history
            {
                Occupations = occupy junction player.Shade occupations
                Player = { player with Cows = 11 }
            } :: history
        Some history
    