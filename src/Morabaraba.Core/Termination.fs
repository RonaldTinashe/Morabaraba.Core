module Morabaraba.Core.Termination

let drawBinder (history : History) =
    if List.length history < 10 then Ok history
    else
        let history = List.take 10 history
        let bothAreFlyingWithoutShot =
            List.forall 
                (fun event -> 
                    Map.count event.Occupations = 6 &&
                    event.Player.Cows = 0)
                history
        if bothAreFlyingWithoutShot then
            Draw history |> Error
        else history |> Ok

let draw = Result.bind drawBinder
