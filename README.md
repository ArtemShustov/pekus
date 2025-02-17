# About the Project
**PEKUS** (rus. пекус) – russian slang for "freshman". The game was intended as a cute little game about a freshman student. It was designed to include a story and various side activities.  
Inspired by *Bully* and *Animal Crossing*.

**Planned Activities Included:**
* Mini-games for various activities:
  * Classes to improve skills
  * Daily routines
  * Part-time jobs
* Character and room customization
* Relationships with NPCs

---

# Technologies
* Custom DI (Dependency Injection) via reflection
* EntryPoint and CompositionRoot patterns
* SceneReference
* Locations system

---

# About the Architecture
The game loading process starts by searching for the `Bootstrap` component in the first scene.
```csharp
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
private static void Load() {
    _context = new GameContext();
    var bootstrap = Object.FindFirstObjectByType<Bootstrap>();
    if (bootstrap == null) {
        Debug.LogWarning("Bootstrap not found. Check scene order.");
        return;      
    }
    bootstrap.Run(_context).Forget();
}
```
In `Bootstrap`, core components such as authentication and the loading screen are initialized. After that, the world scene is loaded with its own entry point.
The `WorldEntryPoint` then creates everything needed for the game (Player, LocationController, UI, WorldClock, etc.). Each stage creates its own DI container with a hierarchical structure.
```csharp
public async UniTask Run(GameContext gameContext) {
    // DI
    var container = new DIContainer(gameContext.Container);
    container.RegisterInstance(_popupCanvas);
    container.RegisterInstance(_root);
    container.RegisterInstance(_camera);
    _root.SetContainer(container);
    // Init
    _player = CreatePlayerCharacter(container, new PlayerData(), _camera);
    // Load location
    await _root.ChangeLocationAsync(_locationName);
}
```
Then, a specific location is loaded.

# Why the Project Was Canceled
I couldn't come up with gameplay that felt interesting enough.
I don’t want to make a purely story-driven game, but there weren’t enough engaging activities for a free-play mode. Players had nothing to upgrade or explore.
Maybe I’ll return to this project in the future, but for now, it feels too big for me.