using System;
using Primitives.Characters;
using Primitives.SaveData;
using UnityEngine;

namespace Infrastructure.Unity.DataStructures
{
    public struct SpawnData
    {
        /// <summary>
        /// The player's game object
        /// </summary>
        public GameObject PlayerObject;
        /// <summary>
        /// The camera object associated with this player
        /// </summary>
        public GameObject CameraObject;
        /// <summary>
        /// The location to spawn the player at
        /// </summary>
        public Vector2 SpawnPoint;
        /// <summary>
        /// Struct containing costume and stat data for the given character
        /// </summary>
        public CharacterConfig CharacterInfo;
        /// <summary>
        /// The id identifying what character the player is playing as
        /// </summary>
        public CharacterID Id;
        /// <summary>
        /// The session id for this player
        /// </summary>
        public Guid PlayerId;
        /// <summary>
        /// The unique integer relating the player to the player's save data
        /// </summary>
        public int ProfileId;

        /// <summary>
        /// The save data for this player
        /// </summary>
        public PlayerSaveData SaveData;
    }
}