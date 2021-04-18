module Morabaraba.Core.Tests.Placing

open System
open Expecto
open Morabaraba.Core
open Morabaraba.Core.Playing

let optionToResult = 
    function
    | Some value -> Ok value
    | None -> Error UnexpectedOccupation

/// Act for the tests involving cow count
let actForCowCount move history =
    play move history |> 
    let eventBinder history = List.tryHead history |> optionToResult
    let cowBinder event =  Ok event.Player.Cows
    Result.bind eventBinder >> Result.bind cowBinder

[<Tests>]
let ``placement on board`` =
    testList 
        "Placing on an empty board"
        [   
            testCase
                "Placement by dark player on empty board's 4th junction"
                (fun () -> 
                    let junction = 4
                    let move = { Main = Placement junction; Shot = None }
                    let history = []
                    let expected =
                        [
                            {
                                Occupations = Map.ofList [ junction, Dark ]
                                Player = { Shade = Dark; Cows = 11}
                            }
                        ] |> Ok
                    let actual = play move history
                    let message = "Dark cow should be placed"
                    Expect.equal actual expected message)

            testCase
                "Placement by dark player on empty board's 1st junction"
                (fun () ->
                    let junction = 1
                    let move = { Main = Placement junction; Shot = None }
                    let history = []
                    let expected =
                        [
                            {
                                Occupations = Map.ofList [ junction, Dark ]
                                Player = { Shade = Dark; Cows = 11}
                            }
                        ] |> Ok
                    let actual = play move history
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
                    let junction = 1
                    let move = { Main = Placement junction; Shot = None }
                    let history = 
                        [ 
                            { 
                                Occupations = Map.add 10 Dark Map.empty
                                Player = { Shade = Dark; Cows = 11 }
                            }
                        ]
                    let expected =
                        {
                            Occupations =
                                [
                                    junction, Light
                                    10, Dark
                                ] |> Map.ofList
                            Player = { Shade = Light; Cows = 11}
                        } :: history |> Ok
                    let actual = play move history
                    let message = "Light cow should be placed"
                    Expect.equal actual expected message)
        ]

[<Tests>]
let ``placement by dark player`` =
    testList
        "Placement by dark player"
        [
            testCase
                "Placement by dark player on a board's 3rd junction"
                (fun () ->
                    let junction = 3
                    let move = { Main = Placement junction; Shot = None }
                    let history =
                        [
                            {
                                Occupations =
                                    [
                                        2, Light
                                        10, Dark
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 11 }
                            }           
                            { 
                                Occupations = Map.ofList [ 10, Dark ]
                                Player = { Shade = Dark; Cows = 11 }
                            }
                        ]
                    let expected =
                        {
                            Occupations =
                                [
                                    junction, Dark
                                    2, Light
                                    10, Dark
                                ] |> Map.ofList
                            Player = { Shade = Dark; Cows = 10 }
                        } :: history |> Ok
                    let actual = play move history
                    let message = "Dark cow should be placed"
                    Expect.equal actual expected message)
        ]

[<Tests>]
let ``count-related tests`` =
    testList
        "Placement cannot take place with no cows"
        [
            testCase
                "Placement after 4 turns on a board's 8th junction"
                (fun () ->
                    let junction = 8
                    let move = { Main = Placement junction; Shot = None }
                    let history =
                        [
                            {
                                Occupations = 
                                    [
                                        7, Light
                                        6, Dark
                                        2, Light
                                        1, Dark 
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 10 }
                            }
                            {
                                Occupations = 
                                    [
                                        6, Dark
                                        2, Light
                                        1, Dark 
                                    ] |> Map.ofList
                                Player = { Shade = Dark; Cows = 10 }
                            }
                            {
                                Occupations = 
                                    [
                                        2, Light
                                        1, Dark 
                                    ] |> Map.ofList
                                Player = { Shade = Light; Cows = 11 }
                            }
                            {
                                Occupations = Map.ofList [ 1, Dark ]
                                Player = { Shade = Dark; Cows = 11 }
                            }
                        ]
                    let expected =
                        {
                            Occupations =
                                [
                                    junction, Dark
                                    7, Light
                                    6, Dark
                                    2, Light
                                    1, Dark 
                                ] |> Map.ofList
                            Player = { Shade = Dark; Cows = 9 }
                        } :: history |> Ok
                    let actual = play move history
                    let message = "Dark cow must be placed"
                    Expect.equal actual expected message)

            testCase
                "Placement with 0 cows"
                (fun () ->
                    let expected = Error UnexpectedOccupation
                    let actual =
                        let junction = 1
                        let move = { Main = Placement junction; Shot = None }
                        let history =
                            let occupations = Map.add 2 Dark Map.empty
                            let history = 
                                {
                                    Occupations = occupations
                                    Player = { Shade = Dark; Cows = 0 }
                                } :: []
                            let occupations = Map.add 3 Light occupations
                            let history =
                                {
                                    Occupations = occupations
                                    Player = { Shade = Light; Cows = 0 }
                                } :: history
                            history
                        actForCowCount move history
                    let message = "Cow should not be placed"
                    Expect.equal actual expected message)
        ]

[<Tests>]
let ``failure cases`` =
    testList
        "fail when invalid data is provided"
        [
            testCase
                "fail when junction is < 1"
                (fun () ->
                    let junction = -3
                    let move = { Main = Placement junction; Shot = None }
                    let history = []
                    let actor () = play move history |> ignore
                    let message = 
                        "Function should throw invalid argument exception"
                    Expect.throwsT<ArgumentException> actor message)

            testCase
                "fail when junction is > 23"
                (fun () ->
                    let junction = 25
                    let move = { Main = Placement junction; Shot = None }
                    let history = []
                    let actor () = play move history |> ignore
                    let message = 
                        "Function should throw invalid argument exception"
                    Expect.throwsT<ArgumentException> actor message)

            testCase
                "fail when junction is occupied"
                (fun () ->
                    let junction = 1
                    let move = { Main = Placement junction; Shot = None }
                    let history =
                        [
                            {
                                Occupations = Map.ofList [ 1, Dark ]
                                Player = { Shade = Dark; Cows = 11 }
                            }
                        ]
                    let message = "Error UnxpectedPlacement expected"
                    let expected = Error UnexpectedOccupation
                    let actual = play move history
                    Expect.equal expected actual message)
        ]
