module Morabaraba.Core.PlayingHelpers

let occupationBinder (history: History) player occupations : Result<History, Error> =
    let event =
        { Occupations = occupations
          Player = player }

    event :: history |> Ok
