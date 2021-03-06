module Morabaraba.Core.Tests.Moving

open Expecto
open Morabaraba.Core
open Morabaraba.Core.Playing

[<Tests>]
let ``successful movement tests`` =
    testList
        "successful movements"
        [ testCase
            "moves cow from 2nd to 1st junction"
            (fun () ->
                let source = 2
                let destination = 1

                let move =
                    { Main = Movement(source, destination)
                      Shot = None }

                let history =
                    [ { Occupations =
                            [ 19, Light
                              18, Dark
                              16, Light
                              15, Dark
                              13, Light
                              12, Dark
                              3, Light
                              source, Dark ]
                            |> Map.ofList
                        Player = { Shade = Light; Cows = 0 } }
                      { Occupations =
                            [ 18, Dark
                              17, Light
                              15, Dark
                              13, Light
                              12, Dark
                              3, Light
                              source, Dark
                              14, Light ]
                            |> Map.ofList
                        Player = { Shade = Dark; Cows = 0 } } ]

                let expected =
                    { Occupations =
                          [ destination, Dark
                            19, Light
                            18, Dark
                            16, Light
                            15, Dark
                            13, Light
                            12, Dark
                            3, Light ]
                          |> Map.ofList
                      Player = { Shade = Dark; Cows = 0 } }
                    :: history
                    |> Ok

                let actual = play move history
                let message = "Dark cow should be moved"
                Expect.equal actual expected message)

          testCase
              "moves cow from 2nd to 11th junction"
              (fun () ->
                  let source = 2
                  let destination = 11

                  let move =
                      { Main = Movement(source, destination)
                        Shot = None }

                  let history =
                      [ { Occupations =
                              [ 16, Light
                                15, Dark
                                13, Light
                                12, Dark
                                3, Light
                                source, Dark ]
                              |> Map.ofList
                          Player = { Shade = Light; Cows = 0 } }
                        { Occupations =
                              [ 15, Dark
                                13, Light
                                12, Dark
                                3, Light
                                source, Dark
                                14, Light ]
                              |> Map.ofList
                          Player = { Shade = Dark; Cows = 0 } } ]

                  let expected =
                      { Occupations =
                            [ destination, Dark
                              16, Light
                              15, Dark
                              13, Light
                              12, Dark
                              3, Light ]
                            |> Map.ofList
                        Player = { Shade = Dark; Cows = 0 } }
                      :: history
                      |> Ok

                  let actual = play move history
                  let message = "Dark cow should fly"
                  Expect.equal actual expected message) ]

[<Tests>]
let ``unsuccessful movements tests`` =
    testList
        "unsuccessful movements"
        [ testCase
            "player cannot move opponent's cow"
            (fun () ->
                let source = 2
                let destination = 1

                let move =
                    { Main = Movement(source, destination)
                      Shot = None }

                let history =
                    [ { Occupations = [ 11, Dark; 3, Light; source, Dark ] |> Map.ofList
                        Player = { Shade = Dark; Cows = 0 } }
                      { Occupations = [ 3, Light; source, Dark ] |> Map.ofList
                        Player = { Shade = Light; Cows = 0 } }
                      { Occupations = Map.ofList [ source, Dark ]
                        Player = { Shade = Dark; Cows = 0 } } ]

                let expected = Error UnexpectedEmptying
                let actual = play move history
                let message = "Dark cow should not be moved"
                Expect.equal actual expected message)

          testCase
              "player cannot move in placing phase"
              (fun () ->
                  let move = { Main = Movement(10, 11); Shot = None }

                  let history =
                      [ { Occupations = [ 2, Light; 10, Dark ] |> Map.ofList
                          Player = { Shade = Light; Cows = 11 } }
                        { Occupations = Map.ofList [ 10, Dark ]
                          Player = { Shade = Dark; Cows = 11 } } ]

                  let expected = Error UnexpectedEmptying
                  let actual = play move history
                  let message = "Dark cow should not be moved"
                  Expect.equal actual expected message)

          testCase
              "cannot fly from 2nd to 11th junction if not flying"
              (fun () ->
                  let source = 2
                  let destination = 11

                  let move =
                      { Main = Movement(source, destination)
                        Shot = None }

                  let history =
                      [ { Occupations =
                              [ 19, Light
                                18, Dark
                                16, Light
                                15, Dark
                                13, Light
                                12, Dark
                                3, Light
                                source, Dark ]
                              |> Map.ofList
                          Player = { Shade = Light; Cows = 0 } }
                        { Occupations =
                              [ 18, Dark
                                17, Light
                                15, Dark
                                13, Light
                                12, Dark
                                3, Light
                                source, Dark
                                14, Light ]
                              |> Map.ofList
                          Player = { Shade = Dark; Cows = 0 } } ]

                  let expected = Error UnexpectedOccupation
                  let actual = play move history
                  let message = "Dark cow should not be moved"
                  Expect.equal actual expected message)

          testCase
              "cannot reform mill broken to form mill being broken"
              (fun () ->
                  let source = 2
                  let destination = 5

                  let move = // reforming original mill from new one
                      { Main = Movement(source, destination)
                        Shot = None }

                  let history =
                      [ { Occupations =
                              [ 9, Light
                                source, Dark
                                4, Dark
                                11, Light
                                6, Dark
                                12, Light
                                1, Dark
                                13, Light
                                3, Dark ]
                              |> Map.ofList
                          Player = { Shade = Light; Cows = 0 } }
                        { Occupations =
                              [ source, Dark
                                10, Light
                                4, Dark
                                11, Light
                                6, Dark
                                12, Light
                                1, Dark
                                13, Light
                                3, Dark ]
                              |> Map.ofList
                          Player = { Shade = Dark; Cows = 0 } }
                        { Occupations =
                              [ 10, Light
                                4, Dark
                                11, Light
                                destination, Dark
                                6, Dark
                                12, Light
                                1, Dark
                                13, Light
                                3, Dark ]
                              |> Map.ofList
                          Player = { Shade = Light; Cows = 0 } }
                        { Occupations =
                              [ 4, Dark
                                11, Light
                                destination, Dark
                                22, Light
                                6, Dark
                                12, Light
                                1, Dark
                                13, Light
                                3, Dark ]
                              |> Map.ofList
                          Player = { Shade = Dark; Cows = 0 } } ]

                  let expected = Error UnexpectedOccupation
                  let actual = play move history
                  let message = "Dark cow should not be moved"
                  Expect.equal actual expected message) ]
