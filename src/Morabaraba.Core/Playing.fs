module Morabaraba.Core.Playing

/// Applies move with historical context
let play { Main = mainMove } history =
    let occupy junction shade occupations = Map.add junction shade occupations
    match mainMove, history with
    // First move
    | Placement junction, [] ->
        let history =
            [
                {
                    Occupations = occupy junction Dark Map.empty
                    Player = { Shade = Dark; Cows = 11 }
                }
            ]
        Some history
    // Light player's turn
    | Placement junction, 
        [{ Occupations = occupations; Player = { Shade = Dark }}] ->
        let history = 
            {
                Occupations = occupy junction Light occupations
                Player = { Shade = Light; Cows = 11 }
            } :: history
        Some history
    | _ -> None
    