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
type Event = { Occupations: Map<int, Shade> }

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
    match mainMove with
    | Placement junction -> 
        Some [ { Occupations = Map.add junction Dark Map.empty } ]
