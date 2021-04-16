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
          let expected = Occupants Map.empty
          let actual = initial.Board
          Expect.equal actual expected "Board should be empty")

      testCase 
        "Game issues dark player with 12 cows"
        (fun () -> 
          let expected = { Shade = Dark; Cows = 12; }
          let actual = initial.DarkPlayer
          Expect.equal actual expected "Dark player should have 12 cows")
    ]
    