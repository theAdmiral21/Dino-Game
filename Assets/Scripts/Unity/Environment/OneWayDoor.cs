using Unity.Hierarchy.Editor;
using Unity.Tools.DrawingTools;
using UnityEngine;

namespace Unity.Environment
{
    public class OneWayDoor : Door
    {
        [SerializeField] private bool _openFromLeft;

        [Header("Overlap check values")]
        [SerializeField] private Vector2 _castSize;
        [SerializeField] private float _centerOffsetX;
        [SerializeField] private LayerMask _layerMask;
        private Vector2 _offset;
        protected override void Awake()
        {
            base.Awake();
            _offset = Mathf.Abs(_centerOffsetX) * Vector2.right;
        }
        public override bool CanInteract()
        {
            return GetInteractDirection();
        }

        private bool GetInteractDirection()
        {
            // Overlap in the direction the door should be opened from
            Vector2 castSize = 2 * Vector2.one;
            Vector2 castCenter = transform.position;
            if (_openFromLeft)
            {
                castCenter -= _offset;
            }
            else
            {
                castCenter += _offset;
            }

            Collider2D collider = Physics2D.OverlapBox(castCenter, castSize, 0f, _layerMask);
            DrawUtil.DrawRectangle(castCenter, castSize / 2, Color.red);
            if (collider != null)
            {
                return collider.CompareTag("Player");
            }
            return false;
        }
    }
}