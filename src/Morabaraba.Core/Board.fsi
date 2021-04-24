module internal Morabaraba.Core.Board

/// Occupies a junction on the board with a cow of the given shade
val occupy : 
    Junction -> Shade -> Map<int, Shade> -> Result<Map<Junction, Shade>, Error>

/// Empties a junction
val empty : Junction -> Map<int, Shade> -> Result<Map<int, Shade>, Error>

/// Checks whether two junctions are neighbours
val areNeighbours : Junction -> Junction -> bool

/// Validates a junction
val validateJunction : Junction -> unit

/// Checks to observe if a shot is possible
val canShoot : Junction -> list<Event> -> bool
