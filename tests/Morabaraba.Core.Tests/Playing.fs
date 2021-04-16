module Morabaraba.Core.Tests.Placing

open Expecto
open Morabaraba.Core

[<Tests>]
let tests =
  testList 
    "Placing"
    [
        testCase
            "Placement by dark player on empty board's 4th junction"
            (fun () -> 
              let expected = Some Dark
              let actual =
                play { Main = Placement 4; Shot = None } [] |> 
                    let eventBinder history = List.tryHead history
                    let occupationBinder event = Map.tryFind 4 event.Occupations
                    Option.bind eventBinder >> Option.bind occupationBinder
              Expect.equal actual expected "Cow should be placed")

        testCase
            "Placement by dark player on empty board's 1st junction"
            (fun () -> 
              let expected = Some Dark
              let actual =
                play { Main = Placement 1; Shot = None } [] |> 
                    let eventBinder history = List.tryHead history
                    let occupationBinder event = Map.tryFind 1 event.Occupations
                    Option.bind eventBinder >> Option.bind occupationBinder
              Expect.equal actual expected "Cow should be placed")
    ]
    