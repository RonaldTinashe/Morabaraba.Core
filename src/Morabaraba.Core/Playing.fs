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
    match mainMove, history with
    // First move
    | Placement junction, [] ->
        let history =
            [
                {
                    Occupations = occupy junction Dark Map.empty
                    Player = { getPlayer history with Cows = 11 }
                }
            ]
        Some history
    // Light player's turn
    | Placement junction, 
        [{ Occupations = occupations; Player = { Shade = Dark }}] ->
        let history = 
            {
                Occupations = occupy junction Light occupations
                Player = { getPlayer history with Cows = 11 }
            } :: history
        Some history
    // Dark player's turn
    | Placement junction,
        { Occupations = occupations; Player = { Shade = Light }} :: _ ->
        let history =
            {
                Occupations = occupy junction Dark occupations
                Player = { Shade = Dark; Cows = 10 }
            } :: history
        Some history
    | _ -> None
    