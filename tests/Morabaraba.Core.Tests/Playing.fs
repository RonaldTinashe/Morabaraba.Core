module Morabaraba.Core.Tests.Placing

open Expecto
open Morabaraba.Core
open Morabaraba.Core.Playing

[<Tests>]
let ``placement on board`` =
    testList 
        "Placing on an empty board"
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
    
[<Tests>]
let ``placement by light player`` =
    testList
        "Placement by light player"
        [
            testCase
                "Placement by light player on a board's 1st junction"
                (fun () -> 
                  let expected = Some Light
                  let history = [ { Occupations = Map.add 10 Dark Map.empty; Player = { Shade = Dark; Cows = 11} }]
                  let actual =
                    play { Main = Placement 1; Shot = None } history |> 
                        let eventBinder history = List.tryHead history
                        let occupationBinder event = Map.tryFind 1 event.Occupations
                        Option.bind eventBinder >> Option.bind occupationBinder
                  Expect.equal actual expected "Cow should be placed")
        ]