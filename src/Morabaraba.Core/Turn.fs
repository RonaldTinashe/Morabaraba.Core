module Morabaraba.Core.Turn

open Initialisation

let getPlayer history =
    match history with
    | [] -> initial.DarkPlayer
    | [ _ ] -> initial.LightPlayer
    | { Player = { Shade = Dark } } :: { Player = player } :: _
    | { Player = { Shade = Light } } :: { Player = player } :: _ -> player

let getOccupations history =
    match history with
    | [] -> initial.Occupations
    | event :: _ -> event.Occupations

let getTurn history =
    getOccupations history, getPlayer history
