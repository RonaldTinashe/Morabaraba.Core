module Morabaraba.Core.Tests.Initialisation

open Expecto
open Morabaraba.Core
open Morabaraba.Core.Initialisation

[<Tests>]
let tests =
    testList
        "Initialisation"
        [ testCase
            "Game initialises with empty board"
            (fun () ->
                let expected = Map.empty
                let actual = initial.Occupations
                let message = "Board should be empty"
                Expect.equal actual expected message)

          testCase
              "Game issues dark player with 12 cows"
              (fun () ->
                  let expected = { Shade = Dark; Cows = 12 }
                  let actual = initial.DarkPlayer
                  let message = "Dark player should have 12 cows"
                  Expect.equal actual expected message)

          testCase
              "Game issues light player with 12 cows"
              (fun () ->
                  let expected = { Shade = Light; Cows = 12 }
                  let actual = initial.LightPlayer
                  let message = "Light player should have 12 cows"
                  Expect.equal actual expected message)

          testCase
              "Game starts with empty history"
              (fun () ->
                  let expected = []
                  let actual = initial.History
                  let message = "History should be empty"
                  Expect.equal actual expected message) ]
