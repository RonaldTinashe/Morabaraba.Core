module Morabaraba.Core

type Board<'J, 'O> 
    when 'J : comparison 
    and 'O : comparison = 
    Occupants of Map<'J, 'O>

let initial<'J, 'O> = 
    {| 
        Board = Occupants Map.empty
    |}
