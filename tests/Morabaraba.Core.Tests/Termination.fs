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
                    Expect.equal actual expected message)
        ]

[<Tests>]
let ``win tests`` =
    testList
        "win"
        [
            testCase
                "win if opponent cannot move"
                (fun () ->
                    let source = 18
                    let destination = 21
                    let move = 
                        { Main = Movement (source, destination); Shot = None }
                    let history =
                        [
                            {
                                Occupations = 
                                    [
                                        1, Dark
                                        source, Light
                                        2, Light
                                        4, Light
                                        10, Light
                                        15, Light
                                        6, Light
                                        19, Light
                                        23, Light
                                        3, Dark
                                        22, Dark
                                        24, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Dark; Cows = 0 }
                            }
                            {
                                Occupations = 
                                    [
                                        13, Light
                                        1, Dark
                                        2, Light
                                        4, Light
                                        10, Light
                                        15, Light
                                        6, Light
                                        19, Light
                                        23, Light
                                        3, Dark
                                        22, Dark
                                        24, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 0 }
                            }
                        ]
                    let event =
                        {
                            Occupations = 
                                [
                                    destination, Light
                                    1, Dark
                                    2, Light
                                    4, Light
                                    10, Light
                                    15, Light
                                    6, Light
                                    19, Light
                                    23, Light
                                    3, Dark
                                    22, Dark
                                    24, Dark
                                ] |> Map.ofList
                            Player = { Shade = Light; Cows = 0 }
                        }
                    let expected = event :: history |> Win |> Error
                    let actual = play move history
                    let message = "Light player should win"
                    Expect.equal actual expected message)

            testCase
                "placement by dark player on empty board's 4th junction"
                (fun () -> 
                    let move = { Main = Concession; Shot = None }
                    let history = []
                    let expected = history |> Concede |> Error
                    let actual = play move history
                    let message = "Dark cow should be placed"
                    Expect.equal actual expected message)
        ]
