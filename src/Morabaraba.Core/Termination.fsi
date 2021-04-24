module internal Morabaraba.Core.Termination

/// Attempts to call the game a draw
val draw : (Result<History, Error> -> Result<History, Error>)

/// Attempts to call the game a win
val win : (Result<History, Error> -> Result<History, Error>)
