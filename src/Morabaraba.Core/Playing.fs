module Morabaraba.Core.Playing

open Placement
open Shooting
open Movement
open Termination

let play move' history =
    let history =
        match move'.Main with
        | Placement junction -> place junction history
        | Movement (source, destination) -> move source destination history
        | Concession -> concede history

    match move' with
    | { Shot = Some target } -> Result.bind (shoot target) history |> win
    | { Main = Movement _; Shot = None } -> history |> win |> draw
    | _ -> history
