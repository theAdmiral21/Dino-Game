using Primitives.Physics;
using UnityEngine;

namespace Tools.DesignTools
{
    public class VisualizeJump : DesignToolsBase
    {
        [Header("Jump Type")]
        public string JumpLabel;

        [Header("Settings")]
        [SerializeField] protected LayerMask _layerMask;
        [SerializeField] protected Color _color;

        [Header("Direction")]
        public bool Flip = false;

        [Header("Starting Velocity")]
        public Vector2 StartingVelocity;

        [Header("Initial Jump")]
        public float InitialVelocityX;
        public float InitialJumpHeight;
        public float InitialJumpApexTime;
        public float InitialGravityMultiplier;

        [Header("Wall Jump")]
        public float WallJumpVelocityX;
        public float WallJumpHeight;
        public float WallJumpApexTime;
        public float WallJumpGravityMultiplier;

        [Header("Draw Options")]
        public bool DrawJump;
        public bool DrawEarlyJump;
        public bool DrawLateJump;
        public bool DrawWallJump;

        [Header("Simulation")]
        public int Steps { get; set; }

        protected float _dt => Time.fixedDeltaTime;
        protected KinematicResult _kinematicState;

        protected Vector3 _origin => transform.position;
        protected Vector3 _earlyOrigin => _origin + Vector3.right * 0.5f;
        protected Vector3 _lateOrigin => _origin + Vector3.left * 0.5f;

        protected void OnValidate()
        {
            _kinematicState = new();
        }

        protected void OnDrawGizmos()
        {
            if (DrawEarlyJump) DrawTrajectory(_earlyOrigin);
            if (DrawJump) DrawTrajectory(_origin);
            if (DrawLateJump) DrawTrajectory(_lateOrigin);
        }

        protected float CalculateGravity(float jumpHeight, float apexTime)
        {
            return -2f * jumpHeight / Mathf.Pow(apexTime, 2);
        }

        protected Vector3 SampleArc(Vector3 origin, float t, float vx, float gravity, float apexTime, float gravityMultiplier)
        {
            float x = origin.x + vx * t;

            float v0 = Mathf.Abs(gravity) * apexTime;
            float y;

            if (t <= apexTime || gravityMultiplier == 0f)
            {
                y = origin.y + v0 * t + 0.5f * gravity * t * t;
            }
            else
            {
                float apexY = origin.y + v0 * apexTime + 0.5f * gravity * apexTime * apexTime;
                float tDescent = t - apexTime;
                float fallGravity = -9.81f * gravityMultiplier;
                y = apexY + 0.5f * fallGravity * tDescent * tDescent;
            }

            return new Vector3(x, y, origin.z);
        }

        public virtual void DrawTrajectory(Vector3 origin)
        {
            Vector3 segOrigin = origin + new Vector3(StartingVelocity.x * _dt, StartingVelocity.y * _dt, 0f);
            float segVx = !Flip ? InitialVelocityX : -InitialVelocityX;
            float segGravity = CalculateGravity(InitialJumpHeight, InitialJumpApexTime);
            float segApex = InitialJumpApexTime;
            float segGravityMult = InitialGravityMultiplier;
            float timeOffset = 0f;

            Vector3 prev = origin;

            for (int i = 0; i < Steps; i++)
            {
                float t = (i * _dt) - timeOffset;
                Vector3 current = SampleArc(segOrigin, t, segVx, segGravity, segApex, segGravityMult);

                var (point, hitNormal, hit) = CheckForCollision(prev, current);

                Gizmos.color = _color;
                Gizmos.DrawLine(prev, point);
                prev = point;

                if (!hit) continue;

                if (hitNormal == Vector2.up || hitNormal == Vector2.down)
                    break;

                if (!DrawWallJump)
                    break;

                segOrigin = point + new Vector3(hitNormal.x * 0.05f, 0f, 0f);
                timeOffset = i * _dt;
                segVx = WallJumpVelocityX * Mathf.Sign(hitNormal.x);
                segGravity = CalculateGravity(WallJumpHeight, WallJumpApexTime);
                segApex = WallJumpApexTime;
                segGravityMult = WallJumpGravityMultiplier;
            }
        }

        protected (Vector3 contactPoint, Vector2 hitNormal, bool hit) CheckForCollision(Vector3 prev, Vector3 current)
        {
            RaycastHit2D rayHit = Cast(prev, current);
            if (rayHit.collider != null)
                return (new Vector3(rayHit.point.x, rayHit.point.y, 0f), rayHit.normal, true);

            return (current, rayHit.normal, false);
        }

        public RaycastHit2D Cast(Vector2 s0, Vector2 s1)
        {
            Vector2 dir = (s1 - s0).normalized;
            float dist = (s1 - s0).magnitude;
            return Physics2D.Raycast(s0, dir, dist, _layerMask);
        }
    }
}