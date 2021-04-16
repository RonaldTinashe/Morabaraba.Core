/// Logical kernel of the Morabaraba game
module Morabaraba.Core.Initialisation

/// The game object's initial state
let initial =
    let issue shade = { Shade = shade; Cows = 12 }
    {| 
        Occupations = (Map.empty : Map<int, Shade>)
        DarkPlayer = issue Dark
        LightPlayer = issue Light
        History = ([] : Event list)
    |}

