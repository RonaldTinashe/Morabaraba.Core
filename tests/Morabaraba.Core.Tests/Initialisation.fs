module Morabaraba.Core.Tests.Initialisation

open Expecto
open Morabaraba.Core

[<Tests>]
let tests =
  testList 
    "Initialisation"
    [
      testCase 
        "Game initialises with empty board"
        (fun () -> 
          let expected = Map.empty
          let actual = initial.Occupations
          Expect.equal actual expected "Board should be empty")

      testCase 
        "Game issues dark player with 12 cows"
        (fun () -> 
          let expected = { Shade = Dark; Cows = 12; }
          let actual = initial.DarkPlayer
          Expect.equal actual expected "Dark player should have 12 cows")

      testCase 
        "Game issues light player with 12 cows"
        (fun () -> 
          let expected = { Shade = Light; Cows = 12; }
          let actual = initial.LightPlayer
          Expect.equal actual expected "Light player should have 12 cows")

      testCase 
        "Game starts with empty history"
        (fun () -> 
          let expected = []
          let actual = initial.History
          Expect.equal actual expected "History should be empty")
    ]
    