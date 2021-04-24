module Morabaraba.Core.Tests.Termination

open Expecto
open Morabaraba.Core
open Morabaraba.Core.Playing

[<Tests>]
let ``draw tests`` =
    testList
        "draw"
        [
            testCase
                "draw after 10 non-shot flying moves"
                (fun () ->
                    let move = { Main = Movement (3, 24); Shot = None }
                    let history =
                        [
                            {
                                Occupations =
                                    [
                                        1, Dark
                                        3, Light
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                    ] |> Map.ofList
                                Player = { Shade = Dark; Cows = 0 }
                            }
                            {
                                Occupations =
                                    [
                                        3, Light
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                        16, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 0 }
                            }
                            {
                                Occupations =
                                    [
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                        16, Dark
                                        17, Light
                                    ] |> Map.ofList
                                Player = { Shade = Dark; Cows = 0 }
                            }
                            {
                                Occupations =
                                    [
                                        3, Light
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                        16, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 0 }
                            }
                            {
                                Occupations =
                                    [
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                        16, Dark
                                        17, Light
                                    ] |> Map.ofList
                                Player = { Shade = Dark; Cows = 0 }
                            }
                            {
                                Occupations =
                                    [
                                        3, Light
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                        16, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 0 }
                            }
                            {
                                Occupations =
                                    [
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                        16, Dark
                                        17, Light
                                    ] |> Map.ofList
                                Player = { Shade = Dark; Cows = 0 }
                            }
                            {
                                Occupations =
                                    [
                                        3, Light
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                        16, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 0 }
                            }
                            {
                                Occupations =
                                    [
                                        9, Dark
                                        7, Light
                                        12, Dark
                                        15, Light
                                        16, Dark
                                        17, Light
                                    ] |> Map.ofList
                                Player = { Shade = Dark; Cows = 0 }
                            }
                        ]
                    let event =
                        {
                            Occupations =
                                [
                                    24, Light
                                    1, Dark
                                    9, Dark
                                    7, Light
                                    12, Dark
                                    15, Light  
                                ] |> Map.ofList
                            Player = { Shade = Light; Cows = 0 }
                        }
                    let expected = Draw (event :: history) |> Error
                    let actual = play move history
                    let message = "Game should draw"
                    Expect.equal expected actual message)
        ]
