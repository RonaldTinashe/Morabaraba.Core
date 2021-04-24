module Morabaraba.Core.Board

let occupy (junction: Junction) (shade: Shade) occupations = 
    if Map.containsKey junction occupations then Error UnexpectedOccupation
    else Map.add junction shade occupations |> Ok

let empty (target: Junction) (occupations: Occupations) = 
    if Map.containsKey target occupations then 
        Map.remove target occupations |> Ok
    else Error UnexpectedEmptying

let lines =
    let rows =
        seq 
            { 
                for start in seq { 1 .. 3 .. 24 } do 
                    [start; start + 1; start + 2] 
            }
    let columns =
        [
            [ 1; 10; 22 ] // starts at outer ring
            [ 2; 5; 8 ]
            [ 3; 15; 24 ]
            [ 4; 11; 19 ] // starts at mid ring
            [ 6; 14; 21 ]
            [ 7; 12; 16 ] // starts at inner ring
            [ 9; 13; 18 ]
            [ 17; 20; 23 ]
        ]
    let diagonals =
        seq
            { 
                for start in [1; 3; 16; 18] do
                    [start; start + 3; start + 6]
            }
    seq { rows; columns; diagonals } |> Seq.concat |> List.ofSeq

let isMill occupations shade line = 
    List.forall 
        (fun junction -> 
            Map.containsKey junction occupations &&
            occupations.[junction] = shade)
        line

let isInMillPredicate junction mill =
    match mill with
    | [a; b; c] -> junction = a || junction = b || junction = c
    | _ -> false
    
let isInMill (junction: Junction) mills = 
    List.exists (isInMillPredicate junction) mills

let getMills history shade =
    match history with
    | [] -> []
    | { Occupations = occupations } :: _ -> 
        List.filter (isMill occupations shade) lines

let getDefenceMills history =
    match history with
    | [] -> []
    | [_] -> []
    | _ :: ({ Player = defender } :: _ as defenceHistory) -> 
        getMills defenceHistory defender.Shade

let getShootingMills history =
    match history with
    | [] -> []
    | { Player = player } :: previousHistory ->
        let currentMills = getMills history player.Shade
        let previousMills = getMills previousHistory player.Shade
        List.except previousMills currentMills

let getJunctionsInDefenceMills history = 
    getDefenceMills history |> 
    List.concat |>
    List.distinct |>
    List.sort

let getOccupants shade = Map.filter (fun _ cow -> cow = shade)

let getDefenceJunctions history =
    match history with
    | []
    | [_] -> []
    | { Occupations = occupations } :: { Player = { Shade = shade }} :: _ ->
        getOccupants shade occupations |>
        Map.toList |>
        List.map (fun (junction, _) -> junction) |>
        List.sort

let areNeighbours junction1 junction2 =
    let areNextToEachOther line = 
        match line with
        | [a; b; _]
        | [_; a; b] -> 
            a = junction1 && b = junction2 ||
            a = junction2 && b = junction1
        | _ -> false
    List.exists areNextToEachOther lines

let areAllDefenceJunctionsInMills history =
    getJunctionsInDefenceMills history = getDefenceJunctions history

let validateJunction junction = 
    if junction > 0 && junction < 25 then ()
    else invalidArg "junction" <| sprintf "Value passed is %i" junction

let canShoot target history =
    (getShootingMills history |> List.isEmpty ||
        getDefenceMills history |> List.exists (List.contains target) &&
        areAllDefenceJunctionsInMills history |> not) |> not

let neighbours junction =
    Seq.except [junction] { 1 .. 24 } |>
    Seq.filter (fun candidate -> areNeighbours junction candidate) |>
    Seq.toList

let areNeighboursOccupied occupations neighbours =
    List.forall 
        (fun neighbour -> Map.containsKey neighbour occupations) neighbours

let areBlocked (shade: Shade) occupations =
    getOccupants shade occupations |>
    Map.map (fun junction _ -> neighbours junction) |>
    Map.forall (fun _ -> areNeighboursOccupied occupations)
