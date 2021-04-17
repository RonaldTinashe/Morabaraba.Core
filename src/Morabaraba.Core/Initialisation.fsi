/// Initialisation module of Morabaraba
module Morabaraba.Core.Initialisation

/// The game object's initial state
val initial : 
    {|
        Occupations : Map<int, Shade>
        DarkPlayer : Player
        LightPlayer : Player
        History : Event list
    |}
