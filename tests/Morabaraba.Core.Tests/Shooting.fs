module Morabaraba.Core.Tests.Shooting

open System
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
                    let actual = play move history
                    let message = "Light cow should be shot"
                    Expect.equal actual expected message)

            testCase
                "shoots when all the opponents' cows are in mills"
                (fun () -> 
                    let move = { Main = Placement 6; Shot = Some 3 }
                    let history =
                        [
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            3, Dark
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Dark; Cows = 9 }
                            }
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
                                            6, Light
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Light; Cows = 9 }
                           } :: history)
                    let actual = play move history
                    let message = "Dark cow should be shot"
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
                    let expected = Error UnexpectedEmptying
                    let actual = play move history
                    let message = "Error UnexpectedEmptying should be output"
                    Expect.equal actual expected message)

            testCase
                "error when no new mill is found"
                (fun () -> 
                    let move = { Main = Placement 8; Shot = Some 4 }
                    let history =
                        [
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            7, Light
                                            3, Dark
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Light; Cows = 9 }
                            }
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            3, Dark
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Dark; Cows = 9 }
                            }
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
                    let expected = Error UnexpectedEmptying
                    let actual = play move history
                    let message = "Error UnexpectedEmptying should be output"
                    Expect.equal actual expected message)
            
            testCase
                "error when the target is in a mill"
                (fun () -> 
                    let move = { Main = Placement 6; Shot = Some 1 }
                    let history =
                        [
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            24, Dark
                                            23, Light
                                            3, Dark
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Dark; Cows = 8 }
                            }
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            23, Light
                                            3, Dark
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Light; Cows = 9 }
                            }
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            3, Dark
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Dark; Cows = 9 }
                            }
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
                    let expected = Error UnexpectedEmptying
                    let actual = play move history
                    let message = "Error UnexpectedEmptying should be output"
                    Expect.equal actual expected message)

            testCase
                "error when shooting non-existed target"
                (fun () -> 
                    let move = { Main = Placement 3; Shot = Some 20 }
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
                    let expected = Error UnexpectedEmptying
                    let actual = play move history
                    let message = "Error UnexpectedEmptying should be output"
                    Expect.equal actual expected message)

            testCase
                "error when the target was invalid"
                (fun () -> 
                    let move = { Main = Placement 6; Shot = Some -4 }
                    let history =
                        [
                            {
                                Occupations = 
                                    Map.ofList 
                                        [
                                            3, Dark
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Dark; Cows = 9 }
                            }
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
                                            6, Light
                                            5, Light
                                            2, Dark
                                            4, Light
                                            1, Dark
                                        ]
                                Player = { Shade = Light; Cows = 9 }
                           } :: history)
                    let actor () = play move history |> ignore
                    let message = "Dark cow should be shot"
                    Expect.throwsT<ArgumentException> actor message)
        ]