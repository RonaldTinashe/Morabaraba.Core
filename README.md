# Morabaraba.Core

Morabaraba.Core is a pure logical kernel for the Morabaraba game.

## Rules

https://web.archive.org/web/20210211041357/http://esportscommentator.blogspot.com/2015/04/generally-accepted-rules-for-game-of_4.html

## Installation

```
dotnet install Morabaraba.Core
```

## Usage

This F# library is intended to be used as a logical kernel for the game.
It can be used in a variety of usage of contexts.

### Initial game state

```fsharp
open Morabaraba.Core.Initialisation

initial // Please refer to initialisation tests
```

### Gameplay

```fsharp
open Morabaraba.Core.Playing

// Please refer to playing tests
play { Move = Placement 2; Shot = Some 4 } initial.History
```