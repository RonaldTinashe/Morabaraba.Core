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
                                        2, Light
                                        3, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 0 }
                            }
                            {
                                Occupations = Map.ofList [ 3, Dark ]
                                Player = { Shade = Dark; Cows = 0 }
                            }
                        ]
                    let expected =
                        {
                            Occupations =
                                [
                                    1, Dark
                                    2, Light
                                ] |> Map.ofList
                            Player = { Shade = Dark; Cows = 0 }
                        } :: history |> Ok
                    let actual = play move history
                    let message = "Dark cow should be moved"
                    Expect.equal actual expected message)
        ]
        