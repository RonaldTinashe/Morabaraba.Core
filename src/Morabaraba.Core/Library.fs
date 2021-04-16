module Morabaraba.Core

type Shade = Dark | Light

type Player = { Shade : Shade; Cows : int }

let initial<'J, 'O> =
    let issue shade = { Shade = shade; Cows = 12 }
    {| 
        Occupations = Map.empty
        DarkPlayer = issue Dark
        LightPlayer = issue Light
        History = []
    |}
