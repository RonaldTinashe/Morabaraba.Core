module internal Morabaraba.Core.Board

/// Occupies a junction on the board with a cow of the given shade
val occupy : Junction -> Shade -> Occupations -> Result<Occupations, Error>

/// Empties a junction
val empty : Junction -> Occupations -> Result<Occupations, Error>

/// Checks whether two junctions are neighbours
val areNeighbours : Junction -> Junction -> bool

/// Validates a junction
val validateJunction : Junction -> unit

/// Checks to observe if a shot is possible
val canShoot : Junction -> History -> bool

/// Get mills for a given player
val getMills : History -> Shade -> list<list<Junction>>

/// Determines if a junction is in a mill
val isInMill : Junction -> list<list<Junction>> -> bool

/// Determines if a cows of a given shade are blocked
val areBlocked : Shade -> Occupations -> bool

/// Retrieves the occupations of a given shade
val getOccupations : (Shade -> Occupations -> Occupations)
