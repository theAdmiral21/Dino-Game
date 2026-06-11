using System;
using Infrastructure.Unity.DataStructures;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Info;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class SetTransitionView : MonoBehaviour
    {

        public SpawnData SetView(ref SpawnData data)
        {
            if (data.CameraObject == null)
            {
                Debug.LogError($"Camera object is null.");
                throw new NullReferenceException();
            }
            if (data.PlayerObject == null)
            {
                Debug.LogError($"Player object is null.");
                throw new NullReferenceException();
            }

            ITransitionView view = data.CameraObject.GetComponentInChildren<ITransitionView>();
            IPlayerView playerView = data.PlayerObject.GetComponentInChildren<IPlayerView>();
            playerView.SetTransitionView(view);

            return data;
        }
    }
}