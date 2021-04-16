module Morabaraba.Core

type Board<'J, 'O> 
    when 'J : comparison 
    and 'O : comparison = 
    Occupants of Map<'J, 'O>

type Shade = Dark | Light

type Player = { Shade : Shade; Cows : int }

let initial<'J, 'O> =
    let issue shade = { Shade = shade; Cows = 12 }
    {| 
        Board = Occupants Map.empty
        DarkPlayer = issue Dark
        LightPlayer = issue Light
    |}
