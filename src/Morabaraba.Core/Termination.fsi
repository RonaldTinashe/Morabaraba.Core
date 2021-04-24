module internal Morabaraba.Core.Termination

/// Attempts to call the game a draw
val draw : (Result<History, Error> -> Result<History, Error>)
