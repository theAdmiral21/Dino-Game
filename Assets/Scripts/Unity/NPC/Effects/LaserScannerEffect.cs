using Game.Core.Effects;
using UnityEngine;

namespace Enemy.Unity
{
    public class LaserScannerEffect : MonoBehaviour, IEffectPlayer
    {
        [SerializeField] private LineRenderer _laser;
        [SerializeField] private LineRenderer _laserSweep;
        [SerializeField] private Material _laserSweepShader;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Vector3 _transformOffset;
        [SerializeField] private Transform _facingTransform;
        public float ScanDist = 5f;
        public float SweepSpeed = 2f;
        public float SweepRange = 60f;

        private bool _isPlaying;
        private float _time;

        private void Awake()
        {
            _laser.positionCount = 2;
            _laserSweep.positionCount = 2;

            // Calculate the thickness to encompass the sweep
            /*
            We're solving for the y component of a right triangle here using x*tan(theta). This gives us HALF the height. Hence why I multiplied by 2.
            */
            float thickness = 2 * ScanDist * Mathf.Tan(SweepRange * Mathf.Deg2Rad);
            _laserSweep.endWidth = thickness;

            _laserSweepShader = new Material(_laserSweepShader);
            _laserSweep.material = _laserSweepShader;
        }
        public void Play(IEffectResult effectResult)
        {
            _isPlaying = true;
        }

        public void Stop()
        {
            _isPlaying = false;
        }

        private void Update()
        {
            // if (_isPlaying)
            // {
            _time += Time.deltaTime;

            float angle = Mathf.Sin(_time * SweepSpeed) * (SweepRange * Mathf.Deg2Rad);
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);

            Vector3 dir = new Vector3(x, y, 0);
            Vector3 worldDir = transform.TransformDirection(dir);
            Vector3 origin = transform.parent.position + _transformOffset;
            Vector3 sweepEnd = new Vector3(ScanDist, 0, 0);
            RaycastHit2D hit = Physics2D.Raycast(origin, worldDir, ScanDist, _layerMask);


            float dist = hit.collider != null ? hit.distance : ScanDist;
            float facing = Mathf.Sign(_facingTransform.localScale.x);
            dist *= facing;
            sweepEnd *= facing;
            // Debug ray
            Debug.DrawRay(origin, worldDir * dist, Color.green);

            _laser.SetPosition(0, origin);
            _laser.SetPosition(1, origin + dist * worldDir);

            _laserSweep.SetPosition(0, origin);
            _laserSweep.SetPosition(1, origin + sweepEnd);
            // }
        }

    }
}
