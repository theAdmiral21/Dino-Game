using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using NPC.Core.Inputs;
using Primitives.Physics;
using Unity.Common.Unity;
using UnityEngine;

namespace NPC.Unity.Inputs
{
    public class AiInputReader : MonoBehaviour, IAiInputReader
    {
        [SerializeField] private SerializedInterface<IActionRequestSink> _requestSinkMono;
        private IActionRequestSink _requestSink => _requestSinkMono.Interface;

        public Vector2 Move => _move;
        private Vector2 _move;

        public bool JumpPressed => _jumpPressed;
        private bool _jumpPressed;

        public bool JumpHeld => _jumpHeld;
        private bool _jumpHeld;

        public bool FaceLeftHeld => _faceLeftHeld;
        private bool _faceLeftHeld;

        private void Awake()
        {

        }
        // This will get funky because some npc's fly...
        public void SetMove(Vector2 input)
        {
            _move = input;
            SendRequest(new RunRequest(false, _move, true));
        }
        public void SetBackUp(Vector2 input)
        {
            _move = input;
            SendRequest(new RunRequest(true, _move, true));
        }
        public void SetJumpPressed(bool input)
        {
            _jumpPressed = input;
            if (input)
            {
                SendRequest(new JumpRequest(true, JumpType.Ground));
            }
        }

        // This will probably get funky later
        public void SetJumpHeld(bool input)
        {
            _jumpHeld = input;
            if (input)
            {
                SendRequest(new JumpRequest(true, JumpType.Ground));
            }
        }
        public void FaceLeft(bool input)
        {
            _faceLeftHeld = input;
            SendRequest(new ChangeFacingRequest(_faceLeftHeld));
        }

        private void SendRequest(IActionRequest request)
        {
            // Debug.Log($"Ai sent request: {request}");
            _requestSink.EnqueueActionRequest(request);
        }

    }
}