module internal Morabaraba.Core.Movement

/// Moves a cow from a source to a destination
val move : Junction -> Junction -> History -> Result<History, Error>
