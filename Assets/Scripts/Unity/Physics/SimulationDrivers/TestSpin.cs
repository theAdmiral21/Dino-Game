using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Unity.Common.Unity;
using UnityEngine;

namespace Physics.Unity
{
    public class TestSpin : MonoBehaviour
    {
        [SerializeField] private SerializedInterface<IActionRequestSink> _requestHandlerMono;
        private IActionRequestSink _requestHandler => _requestHandlerMono.Interface;

        [SerializeField] private bool _spin;
        [SerializeField] private float _omega;

        // Update is called once per frame
        void Update()
        {
            if (_spin)
            {
                _requestHandler.EnqueueActionRequest(new RotateRequest(_omega));
            }
        }
    }
}
