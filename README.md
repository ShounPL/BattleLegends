# Battle Legends

A vertical-slice Unity prototype for a mobile action RPG built around a fast arena battle loop, hero progression, rewards, save system, and a menu flow. This repository is intentionally scoped to a stable core so it can be expanded into a larger game without rewriting the foundation.

## Included prototype features
- Playable battle arena with three starter heroes and enemy waves
- Simple touch-friendly menu flow using Unity GUI
- Core combat loop: movement, auto-targeting, basic attacks, ability usage
- XP and gold rewards with progression tracking
- Local save/load system for progress data
- Data-driven hero and ability definitions built with ScriptableObjects
- Modular classes for future expansion

## Project structure

```text
BattleLegends/
├── Assets/
│   ├── Scripts/
│   │   ├── Combat/
│   │   │   ├── BattleManager.cs
│   │   │   ├── CombatActor.cs
│   │   │   ├── EnemyController.cs
│   │   │   └── HeroController.cs
│   │   ├── Data/
│   │   │   ├── AbilityDefinition.cs
│   │   │   └── HeroDefinition.cs
│   │   ├── Progression/
│   │   │   └── ProgressionSystem.cs
│   │   ├── Save/
│   │   │   └── SaveSystem.cs
│   │   └── UI/
│   │       ├── BattleHUD.cs
│   │       └── MainMenuController.cs
│   └── Scenes/
│       └── README.txt
├── Packages/
│   └── manifest.json
├── .gitignore
├── README.md
└── ProjectSettings/
    └── ProjectVersion.txt
```

## Unity setup
1. Open the repository root in Unity Hub as a Unity project.
2. Use a recent LTS version, preferably Unity 2022.3 LTS.
3. Create a new scene named `BattleArena`.
4. Add an empty GameObject named `GameBootstrap`.
5. Attach the following components to `GameBootstrap`:
   - `BattleManager`
   - `ProgressionSystem`
   - `MainMenuController`
6. Press Play in the Editor.
7. The prototype menu appears and you can start the battle from the UI.

## Prototype controls
- WASD / Arrow keys: move the active hero
- Space: basic attack (if implemented via the custom battle loop)
- Q: cast first ability
- E: cast second ability
- In the menu: use buttons to start a battle and check progression

## Notes
- This is a vertical slice, not the full 80-hero MMORPG. It focuses on a stable, expandable core.
- Placeholder 3D models are represented by primitive capsules/cubes, so the project can run immediately without external 3D assets.
- The networking/backend layer is intentionally isolated and ready for future online integration.

## Recommended next milestones
1. Add more heroes and enemy types through scriptable data
2. Add a real canvas-based UI with touch-friendly buttons
3. Add dungeons, clan data, and PvP server-authoritative hooks
4. Add a backend service (Firebase, PlayFab, or custom Node + Photon/Netcode)

## Android build
- Switch platform to Android in Build Settings
- Enable Android IL2CPP and ARM64 if supported by your target device
- Add a keystore for release builds
- Build via Build > Build APK or Build > Build Android App Bundle

## Testing checklist
- Start the menu and launch the battle
- Confirm hero movement and target acquisition
- Confirm enemies attack and die properly
- Confirm XP and gold rewards are applied
- Close and reopen the project to verify local save persistence

## Important disclaimer
This repository contains a functional prototype and a modular foundation for the larger game, but no external art assets, backend credentials, or full online multiplayer implementation are included.
