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
    | Concession

/// Models a game's move
type Move = { Main : MainMove; Shot : Junction option }

/// Occupations are a map from junction to shade
type Occupations = Map<Junction, Shade>

/// Models a single historical event
type Event = { Occupations : Occupations; Player : Player }

///  A history is a list of events
type History = list<Event>

/// Player-rooted error states
type Error = 
    | UnexpectedOccupation
    | UnexpectedEmptying
    | Draw of History
    | Win of History
    | Concede of History
