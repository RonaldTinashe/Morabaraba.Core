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
    let getTurn history = getOccupations history, getPlayer history
    let place history junction =
        let history =
            let occupations, player = getTurn history
            {
                Occupations = occupy junction player.Shade occupations
                Player = { player with Cows = player.Cows - 1 }
            } :: history
        Some history
    match mainMove, history with
    | Placement junction, _ -> place history junction
    