# Audio Notes

I am overhauling the audio system. It was getting out of control. I had 4 or 5 different audio request types and 4 different audio entity enums. Almost every entity also had its own form of an audio bridge. This has all changed.

- There is now only **one** entity key: `EntityKey` and it can refer to ANYTHING that makes a sound.
- All of the audio bridges have been collapsed down into **one** `AudioBridge` component. This component implement `IAudioBridge` which is just a contract for creating a `SoundRequest` with different levels of ease.
- `SoundRequest` has replaced all of the previous `IAudioRequest` objects for playing sounds. Note that ambient sounds and music still need to get sorted.
- The `LevelObjectSoundSet` will remain unchanged on the outside. On the inside it has been updated to use the new sound request system.
- There will no longer be an `EnemySoundSet`, `PlayerSoundSet`, or `BossSoundSet`. These will be reduced down to an `EntitySoundSet` class.
- The `AudioClipLibrary` is also being reworked. Instead of holding 8 different scriptable object references there will be a list of EntitySoundSets for enemies and the player, a LevelObjectSoundSet for items and level objects, a sound set for ambiance, and a sound set for music. 