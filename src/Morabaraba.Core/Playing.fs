module Morabaraba.Core.Playing

open Placement
open Shooting
open Movement

let drawBinder history =
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

let play move' history =
    let history =
        match move'.Main with
        | Placement junction -> place junction history
        | Movement (source, destination) -> move source destination history
    match move' with
    | { Shot = Some target } -> Result.bind (shoot target) history
    | { Main = Movement (_,_); Shot = None } -> Result.bind drawBinder history
    | _ -> history
        