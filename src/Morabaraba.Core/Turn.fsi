module internal Morabaraba.Core.Turn

/// Retrieves the current occupations and player that the player
val getTurn : list<Event> -> Map<int, Shade> * Player
