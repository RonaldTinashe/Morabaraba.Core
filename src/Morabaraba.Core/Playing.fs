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

let place history junction =
    let event =
        if junction > 0 && junction < 25 then
            let occupations, player = getTurn history
            if player.Cows > 0 then
                {
                    Occupations = occupy junction player.Shade occupations
                    Player = { player with Cows = player.Cows - 1 }
                } |> Ok
            else Error UnexpectedPlacement
        else invalidArg "junction" <| sprintf "Value passed is %i" junction
    Result.bind (fun event -> event :: history |> Ok ) event

let play { Main = mainMove } history =
    match mainMove with
    | Placement junction -> place history junction
    