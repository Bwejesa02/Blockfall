Sound Effects: https://pixabay.com/sound-effects/
Backgroung Image
Google Images
Effects: https://assetstore.unity.com/packages/vfx/particles/war-fx-5669

A Tetris style game 

Design Patterns
- Flyweight/Factory Pattern (ParticleFactory):
  - Caches particle prefabs to reduce memory usage.
  - Demonstrated in FlyweightTest with a toggle for testing

- Singleton Pattern (AudioManager):
  - Manages sound effects globally for the game

- Command 
- Controls adding menu actions

- Plugin (InputMapper):
  - Static plugin to map inputs for movement, rotation, and drops


Plugins/DLL
- InputMapper Plugin:
  - Provides modular input handling.

Performance Profiling
- Flyweight optimization tested using Unity Profiler
- Cached particles should reduce memory usage from instantiating over again

Diagrams
Flyweight
- ParticleFactory
  - → Caches → Particle prefab
  - → Spawns → Multiple particles
Command
-GameOverMenu
Command Pattern

→ Invokes → PlayAgian

→ Invokes → MainMenu

→ Invokes → Quit

InputMapper Plugin
- InputMapper
  - → Returns → Input values for movement, rotation, and drop actions

AudioManager
- AudioManager (Singleton)
  - → Plays → Sound effects

Known Issues
- Game not set up to best utilize flyweight
- GhostShape occasionally lags behind active shapes during rapid movement
- Overlap when swapping saved shapes
- Controller Input transitions more that 1 space left and right

