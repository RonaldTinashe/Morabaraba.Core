[<AutoOpen>]
module Morabaraba.Core.DomainTypes

/// Models the shade of the game
type Shade = Dark | Light

/// Models a player object
type Player = { Shade : Shade; Cows : int }

/// Modeles a player phase
type internal Phase = Placing | Moving | Flying

/// Junctions are simply 32-bit integers
type Junction = int

/// Models a non-shot move
type MainMove = 
    | Placement of junction : Junction
    | Movement of source : Junction * junction : Junction

/// Models a game's move
type Move = { Main : MainMove; Shot : Junction option }

/// Models a single historical event
type Event = { Occupations : Map<Junction, Shade>; Player : Player }

/// Player-rooted error states
type Error = UnexpectedOccupation | UnexpectedEmptying
