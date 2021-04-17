module Morabaraba.Core.Tests.Placing

open Expecto
open Morabaraba.Core
open Morabaraba.Core.Playing

/// Act for the tests
let act move history junction =
    play move history |> 
    let eventBinder history = List.tryHead history
    let occupationBinder event =  Map.tryFind junction event.Occupations
    Option.bind eventBinder >> Option.bind occupationBinder


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
                        let junction = 4
                        let move = { Main = Placement junction; Shot = None }
                        let history = []
                        act move history junction
                    let message = "Dark cow should be placed"
                    Expect.equal actual expected message)

            testCase
                "Placement by dark player on empty board's 1st junction"
                (fun () -> 
                    let expected = Some Dark
                    let actual =
                        let junction = 1
                        let move = { Main = Placement junction; Shot = None }
                        let history = []
                        act move history junction
                    let message = "Dark cow should be placed"
                    Expect.equal actual expected message)
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
                    let actual =
                        let junction = 1
                        let move = { Main = Placement junction; Shot = None }
                        let history = 
                            [ 
                                { 
                                    Occupations = Map.add 10 Dark Map.empty
                                    Player = { Shade = Dark; Cows = 11 }
                                }
                            ]
                        act move history junction
                    let message = "Light cow should be placed"
                    Expect.equal actual expected message)
        ]

[<Tests>]
let ``placemet by dark player`` =
    testList
        "Placement by dark player"
        [
            testCase
                "Placement by dark player on a board's 3rd junction"
                (fun () ->
                    let expected = Some Dark
                    let actual =
                        let junction = 3
                        let move = { Main = Placement junction; Shot = None }
                        let history =
                            let darkOccupation = Map.add 10 Dark Map.empty
                            let occupations = Map.add 2 Light darkOccupation
                            [
                                {
                                    Occupations = occupations
                                    Player = { Shade = Light; Cows = 11 }
                                }           
                                { 
                                    Occupations = darkOccupation
                                    Player = { Shade = Dark; Cows = 11 }
                                }
                            ]
                        act move history junction
                    let message = "Dark cow should be placed"
                    Expect.equal actual expected message)
        ]
