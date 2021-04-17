module Morabaraba.Core.Initialisation

let initial =
    let issue shade = { Shade = shade; Cows = 12 }
    {| 
        Occupations = (Map.empty : Map<int, Shade>)
        DarkPlayer = issue Dark
        LightPlayer = issue Light
        History = ([] : Event list)
    |}
