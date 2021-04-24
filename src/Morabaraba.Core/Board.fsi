module internal Morabaraba.Core.Board

/// Occupies a junction on the board with a cow of the given shade
val occupy : 
    Junction -> Shade -> Occupations -> Result<Occupations, Error>

/// Empties a junction
val empty : Junction -> Occupations -> Result<Occupations, Error>

/// Checks whether two junctions are neighbours
val areNeighbours : Junction -> Junction -> bool

/// Validates a junction
val validateJunction : Junction -> unit

/// Checks to observe if a shot is possible
val canShoot : Junction -> list<Event> -> bool
