[<AutoOpen>]
module Morabaraba.Core.DomainTypes

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
