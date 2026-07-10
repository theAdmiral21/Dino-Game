using Core.Movement.Abstractions;
using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace Unity.NPC.Inputs
{
    public class RaptorInputReader : MonoBehaviour, IRaptorInput
    {
        private IActionRequestSink _requestSink;
        [SerializeField] private bool _printDebug;
        public Vector2 Move => _move;
        private Vector2 _move;

        public bool JumpPressed => _jumpPressed;
        private bool _jumpPressed;

        public bool JumpHeld => _jumpHeld;
        private bool _jumpHeld;

        public bool FaceLeftHeld => _faceLeftHeld;

        public bool LungePressed { get; private set; }

        public bool BitePressed { get; private set; }

        private bool _faceLeftHeld;

        private void Awake()
        {
            var sinkProvider = GetComponentInParent<IActionRequestSinkProvider>();
            _requestSink = sinkProvider.RequestSink;
        }

        public void Lunge(Vector2 direction)
        {
            SendRequest(new LungeRequest(direction));
        }

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
            if (_printDebug) Debug.Log($"Ai sent request: {request}");

            _requestSink.EnqueueActionRequest(request);
        }

        public void Bite()
        {
            SendRequest(new BiteRequest());
        }
    }
}