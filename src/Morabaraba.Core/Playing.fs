module Morabaraba.Core.Playing

/// Applies move with historical context
let play { Main = mainMove } history =
    let occupy junction shade occupations = Map.add junction shade occupations
    match mainMove, history with
    | Placement junction, [] -> Some [{ Occupations = occupy junction Dark Map.empty; Player = { Shade = Dark; Cows = 11 } }]
    | Placement junction, [{ Occupations = occupations; Player = { Shade = Dark }}] ->
        { Occupations = occupy junction Light occupations; Player = { Shade = Light; Cows = 11 } } :: history |> Some
    | _ -> None
    