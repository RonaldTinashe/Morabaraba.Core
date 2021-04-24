module internal Morabaraba.Core.Board

/// Occupies a junction on the board with a cow of the given shade
val occupy : int -> Shade -> Map<int, Shade> -> Result<Map<int, Shade>, Error>

/// Empties a junction
val empty : int -> Map<int, Shade> -> Result<Map<int, Shade>, Error>

/// Checks whether two junctions are neighbours
val areNeighbours : int -> int -> bool

/// Validates a junction
val validateJunction : int -> unit

/// Checks to observe if a shot is possible
val canShoot : int -> list<Event> -> bool
