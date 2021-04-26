module internal Morabaraba.Core.PlayingHelpers

/// Binder for playing results
val occupationBinder : History -> Player -> Occupations -> Result<History, Error>
