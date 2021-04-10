# Morabaraba

## Board

![Board](Board.png)

*An example of a Morabaraba board.
Source: http://esportscommentator.blogspot.com/2015/04/generally-accepted-rules-for-game-of_4.html*

* The board consists of 24 junctions
    * They may have different names
    * But their positions remain the same
    * Their connections also remain the same
* Two junctions are connected to each other via
    * Rows
    * Columns
    * Diagonals
* They form lines where three of them have the same kind of connection
    * For example, A1 Row A2 Row A3
* Each junction may be either empty or occupied by a cow
* A cow can be placed onto or removed from a junction

## Players

* Each player has a hand of cows of single shade
* The shades may either be dark or light
* The phase of a player can be computed from their hand and board
    after a move

## History

* The history keeps collects combinations of
    * Each move
    * The board after the move was actioned
    * The player who made the move
* The history can report a particular player's previous turn
* The history can report if there has been a shot in the past ten moves
* The history can replay the game till the most recent move

## Gameplay

## Initialisation

* The board is completely empty
* Both dark and light players are issued 12 cows
* Start with an empty history
