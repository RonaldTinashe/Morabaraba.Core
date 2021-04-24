module internal Morabaraba.Core.Player

/// Takes a cow from a player's hand
val decrementHand : Player -> Player

/// Retrieves the phase the current player is in
val getPhase : History -> Phase
