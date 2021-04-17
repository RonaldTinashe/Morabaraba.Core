module Morabaraba.Core.Playing

open Initialisation

let occupy junction shade occupations = Map.add junction shade occupations

let getPlayer history = 
    match history with
    | [] -> initial.DarkPlayer
    | [_] -> initial.LightPlayer
    | { Player = { Shade = Dark }} :: { Player = player } :: _
    | { Player = { Shade = Light }} :: { Player = player } :: _ -> player

let getOccupations history =
    match history with
    | [] -> initial.Occupations
    | event :: _ -> event.Occupations

let getTurn history = getOccupations history, getPlayer history

let validate junction = 
    if junction > 0 && junction < 25 then ()
    else invalidArg "junction" <| sprintf "Value passed is %i" junction

let place history junction =
    validate junction
    let event =
        let occupations, player = getTurn history
        if player.Cows > 0 then
            {
                Occupations = occupy junction player.Shade occupations
                Player = { player with Cows = player.Cows - 1 }
            } |> Ok
        else Error UnexpectedPlacement
    Result.bind (fun event -> event :: history |> Ok ) event

let shoot target history =
    match history with
        | [] -> Error UnexpectedShot
        | { Occupations = occupations } as event :: history ->
            let occupations = Map.remove target occupations
            Ok ({ event with Occupations = occupations } :: history)

let play move history =
    match move with
    | { Main = Placement junction; Shot = None } -> place history junction
    | { Main = Placement junction; Shot = Some target } ->
        let history = place history junction
        Result.bind (shoot target) history
