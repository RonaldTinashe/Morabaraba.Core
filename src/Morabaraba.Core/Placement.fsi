module internal Morabaraba.Core.Placement

/// Place cow on board
val place : Junction -> History -> Result<History, Error>
