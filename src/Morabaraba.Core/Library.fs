module Morabaraba.Core

type Board<'J, 'O> 
    when 'J : comparison 
    and 'O : comparison = 
    Occupants of Map<'J, 'O>

type Shade = Dark | Light

type Player = { Shade : Shade; Cows : int }

let initial<'J, 'O> = 
    {| 
        Board = Occupants Map.empty
        DarkPlayer = { Shade = Dark; Cows = 12 }
        LightPlayer = { Shade = Light; Cows = 12 }
    |}
