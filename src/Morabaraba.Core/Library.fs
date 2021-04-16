/// Logical kernel of the Morabaraba game
module Morabaraba.Core

/// Models the shade of the game
type Shade = Dark | Light

/// Models a player object
type Player = { Shade : Shade; Cows : int }

/// Models a non-shot move
type MainMove = Placement of int

/// Models a game's move
type Move = { Main : MainMove; Shot : int option }

/// Models a single historical event
type Event = { Occupations : Map<int, Shade>; Player : Player }

/// The game object's initial state
let initial =
    let issue shade = { Shade = shade; Cows = 12 }
    {| 
        Occupations = (Map.empty : Map<int, Shade>)
        DarkPlayer = issue Dark
        LightPlayer = issue Light
        History = ([] : Event list)
    |}

/// Applies move with historical context
let play { Main = mainMove } history =
    let occupy junction shade occupations = Map.add junction shade occupations
    match mainMove, history with
    | Placement junction, [] -> Some [{ Occupations = occupy junction Dark Map.empty; Player = { Shade = Dark; Cows = 11 } }]
    | Placement junction, [{ Occupations = occupations; Player = { Shade = Dark }}] ->
        { Occupations = occupy junction Light occupations; Player = { Shade = Light; Cows = 11 } } :: history |> Some
    | _ -> None
