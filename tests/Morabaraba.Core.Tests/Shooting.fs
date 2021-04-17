module Morabaraba.Core.Tests.Shooting

open Expecto
open Morabaraba.Core
open Morabaraba.Core.Playing

[<Tests>]
let ``successful shooting`` =
    testList 
        "shooting when placing"
        [   
            testCase
                "shoots having formed a new mill"
                (fun () -> 
                    let move = { Main = Placement 3; Shot = Some 4 }
                    let history =
                        [
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Light; Cows = 10 }
                            }
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Dark; Cows = 10 }
                            }
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Light; Cows = 11 }
                            }
                            {
                                Occupations = Map.ofList [1, Dark]
                                Player = { Shade = Dark; Cows = 11 }
                            }
                        ]
                    let expected =
                        Ok                           
                           ({
                                Occupations = 
                                    Map.ofList 
                                        [
                                            3, Dark
                                            5, Light
                                            2, Dark
                                            1, Dark
                                        ]
                                Player = { Shade = Dark; Cows = 9 }
                           } :: history)
                    let actual =
                        play move history
                    let message = "Dark cow should be placed"
                    Expect.equal actual expected message)
        ]

[<Tests>]
let ``unsuccessful shooting`` =
    testList
        "shooting when placing"
        [
            testCase
                "error when no mill is found"
                (fun () ->
                    let move = { Main = Placement 11; Shot = Some 4 }
                    let history =
                        [
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Light; Cows = 10 }
                            }
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Dark; Cows = 10 }
                            }
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Light; Cows = 11 }
                            }
                            {
                                Occupations = Map.ofList [1, Dark]
                                Player = { Shade = Dark; Cows = 11 }
                            }
                        ]
                    let expected = Error UnexpectedShot
                    let actual =
                        play move history
                    let message = "Dark cow should be placed"
                    Expect.equal actual expected message)
        ]