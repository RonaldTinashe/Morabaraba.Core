/// Module for playing
module Morabaraba.Core.Playing

/// Applies move with historical context
val play : Move -> Event list -> Event list option
