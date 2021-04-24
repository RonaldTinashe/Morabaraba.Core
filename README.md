# Morabaraba.Core

Morabaraba.Core is a pure logical kernel for the Morabaraba game.

## Rules Under Development

https://web.archive.org/web/20210211041357/http://esportscommentator.blogspot.com/2015/04/generally-accepted-rules-for-game-of_4.html

## Roadmap

### Completed

* Initialisation
* Placing
* Shooting
* Movement
    * Unvalidated movements except for junction validation
    * Player cannot move an opponent's cow

### TODO

* Movement
    * Movement cannot happen in placing phase
    * Cow cannot fly if player is not in flying phase
    * Mill cannot be reformed if broken to form a mill
* Termination
    * Draw if there have been no shots in 10 moves and both players are flying
    * Win if the opponent cannot move
    * Win if the opponent concedes
    * Win if the opponent has 2 cows on the board and is flying
* Format
* Documentation
* CI
* Package
* Publish
