module Morabaraba.Core.Playing

open Placement
open Shooting
open Movement

let play move' history =
    let history =
        match move'.Main with
        | Placement junction -> place junction history
        | Movement (source, destination) -> move source destination history
    match move'.Shot with
    | Some target -> Result.bind (shoot target) history
    | None -> history
    