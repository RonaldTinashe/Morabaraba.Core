[<AutoOpen>]
module Morabaraba.Core.DomainTypes

/// Models the shade of the game
type Shade = Dark | Light

/// Models a player object
type Player = { Shade : Shade; Cows : int }

/// Modeles a player phase
type Phase = Placing | Moving

/// Models a non-shot move
type MainMove = 
    | Placement of junction : int
    | Movement of source : int * junction : int

/// Models a game's move
type Move = { Main : MainMove; Shot : int option }

/// Models a single historical event
type Event = { Occupations : Map<int, Shade>; Player : Player }

type Error = UnexpectedOccupation | UnexpectedEmptying
