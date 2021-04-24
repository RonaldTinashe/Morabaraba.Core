﻿module Morabaraba.Core.Initialisation

let issue shade = { Shade = shade; Cows = 12 }

let initial =
    {| 
        Occupations = (Map.empty : Map<Junction, Shade>)
        DarkPlayer = issue Dark
        LightPlayer = issue Light
        History = ([] : Event list)
    |}
