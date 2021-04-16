module Morabaraba.Core

type Shade = Dark | Light

type Player = { Shade : Shade; Cows : int }

type MainMove = Placement of int

type Move = { Main : MainMove; Shot : int option }

type Event = { Occupations: Map<int, Shade> }

let initial<'J, 'O> =
    let issue shade = { Shade = shade; Cows = 12 }
    {| 
        Occupations = Map.empty
        DarkPlayer = issue Dark
        LightPlayer = issue Light
        History = []
    |}

let play { Main = mainMove } history =
    match mainMove with
    | Placement 4 -> Some [ { Occupations = Map.add 4 Dark Map.empty } ]
    | _ -> None
