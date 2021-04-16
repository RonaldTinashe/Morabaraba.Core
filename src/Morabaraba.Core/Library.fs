module Morabaraba.Core

type Shade = Dark | Light

type Player = { Shade : Shade; Cows : int }

type MainMove = Placement of int

type Move = { Main : MainMove; Shot : int option }

type Event = { Occupations: Map<int, Shade> }

let initial =
    let issue shade = { Shade = shade; Cows = 12 }
    {| 
        Occupations = (Map.empty : Map<int, Shade>)
        DarkPlayer = issue Dark
        LightPlayer = issue Light
        History = ([] : Event list)
    |}

let play { Main = mainMove } history =
    match mainMove with
    | Placement junction -> 
        Some [ { Occupations = Map.add junction Dark Map.empty } ]
