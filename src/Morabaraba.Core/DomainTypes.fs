module Morabaraba.Core.DomainTypes

type Shade = Dark | Light

type Player = { Shade : Shade; Cows : int }

type internal Phase = Placing | Moving | Flying

type Junction = int

type MainMove = 
    | Placement of junction : Junction
    | Movement of source : Junction * junction : Junction

type Move = { Main : MainMove; Shot : Junction option }

type Event = { Occupations : Map<Junction, Shade>; Player : Player }

type Error = UnexpectedOccupation | UnexpectedEmptying
