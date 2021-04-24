module Morabaraba.Core.Player

open Turn

let decrementHand player = { player with Cows = player.Cows - 1 }

let getPhase history =
    let occupations, player = getTurn history
    if player.Cows > 0 then Placing
    else if player.Cows = 0 then
        let cowCount =
            occupations |> 
            Map.filter (fun _ shade -> player.Shade = shade) |> 
            Map.count
        if cowCount > 3 then Moving
        else Flying
    else invalidArg "player" "has less than 0 cows on their hand"