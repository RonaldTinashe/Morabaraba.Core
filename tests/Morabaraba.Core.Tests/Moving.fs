module Morabaraba.Core.Tests.Moving

open Expecto
open Morabaraba.Core
open Morabaraba.Core.Playing

[<Tests>]
let ``successful movement tests`` =
    testList
        "successful movements"
        [
            testCase
                "moves cow from 2nd to 1st junction"
                (fun () ->
                    let source = 2
                    let destination = 1
                    let move = 
                        { Main = Movement (source, destination); Shot = None }
                    let history =
                        [
                            {
                                Occupations =
                                    [
                                        3, Light
                                        source, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 0 }
                            }
                            {
                                Occupations = Map.ofList [ source, Dark ]
                                Player = { Shade = Dark; Cows = 0 }
                            }
                        ]
                    let expected =
                        {
                            Occupations =
                                [
                                    destination, Dark
                                    3, Light
                                ] |> Map.ofList
                            Player = { Shade = Dark; Cows = 0 }
                        } :: history |> Ok
                    let actual = play move history
                    let message = "Dark cow should be moved"
                    Expect.equal actual expected message)
        ]