/// Initialisation module of Morabaraba
module Morabaraba.Core.Initialisation

/// The game object's initial state
val initial : 
    {|
        Occupations : Map<Junction, Shade>
        DarkPlayer : Player
        LightPlayer : Player
        History : Event list
    |}
