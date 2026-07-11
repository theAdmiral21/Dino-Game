using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Info;
using UnityEngine;

namespace PlayerController.Unity.Info
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        // public IPlayerInfoProvider PlayerInfo => _playerInfo.Interface;
        // [SerializeField] SerializedInterface<IPlayerInfoProvider> _playerInfo;

        public ITransitionView TransitionView { get; private set; }

        public void SetTransitionView(ITransitionView transitionView)
        {
            TransitionView = transitionView;
            Debug.Log($"Set player transition view to: {transitionView}");
        }
    }
}