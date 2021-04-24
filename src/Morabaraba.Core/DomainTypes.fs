module Morabaraba.Core.DomainTypes

type Shade = Dark | Light

type Player = { Shade : Shade; Cows : int }

type Phase = Placing | Moving | Flying

type MainMove = 
    | Placement of junction : int
    | Movement of source : int * junction : int

type Move = { Main : MainMove; Shot : int option }

type Event = { Occupations : Map<int, Shade>; Player : Player }

type Error = UnexpectedOccupation | UnexpectedEmptying
