# Morabaraba.Core

Morabaraba.Core is a pure logical kernel for the Morabaraba game.

## Rules

https://web.archive.org/web/20210211041357/http://esportscommentator.blogspot.com/2015/04/generally-accepted-rules-for-game-of_4.html

## Installation

```
dotnet add package Morabaraba.Core
```

## Usage

This F# library is intended to be used as a logical kernel for the game.
It can be used in a variety of usage of contexts.

The following examples assume the code is being run in F# interactive.

### Initial game state

```fsharp
open Morabaraba.Core.Initialisation

// Please refer to initialisation tests
initial |> sprintf "%A"
```

### Gameplay

```fsharp
open Morabaraba.Core
open Morabaraba.Core.Initialisation
open Morabaraba.Core.Playing

(*  Please refer to playing tests
    This function is meant to be used in a game loop
    The output history becomes non-user input
    for its next application for continuity *)
play { Main = Placement 2; Shot = Some 4 } initial.History
```