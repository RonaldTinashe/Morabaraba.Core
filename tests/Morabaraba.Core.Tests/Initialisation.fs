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
    ]
    