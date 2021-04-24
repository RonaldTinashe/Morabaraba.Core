module Morabaraba.Core.DomainTypes

type Shade = Dark | Light

type Player = { Shade : Shade; Cows : int }

type internal Phase = Placing | Moving | Flying

type Junction = int

type MainMove = 
    | Placement of junction : Junction
    | Movement of source : Junction * junction : Junction

type Move = { Main : MainMove; Shot : Junction option }

type Occupations = Map<int, Shade>

type Event = { Occupations : Occupations; Player : Player }

type History = list<Event>

type Error = 
    | UnexpectedOccupation
    | UnexpectedEmptying
    | Draw of History
    | Win of History
    