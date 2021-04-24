module internal Morabaraba.Core.Board

/// Occupies a junction on the board with a cow of the given shade
val occupy : int -> Shade -> Map<int, Shade> -> Result<Map<int, Shade>, Error>

/// Empties a junction
val empty : int -> Map<int, Shade> -> Result<Map<int, Shade>, Error>

/// Retrieves the mills justifying defence
val getDefenceMills : list<Event> -> list<list<int>>

/// Retrieves the mills justifying shooting
val getShootingMills : list<Event> -> list<list<int>>

/// Check whether defence junctions are in mills
val areAllDefenceJunctionsInMills : list<Event> -> bool

/// Checks whether two junctions are neighbours
val areNeighbours : int -> int -> bool
