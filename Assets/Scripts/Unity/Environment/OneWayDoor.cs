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
        [SerializeField] private Vector2 _offset;
        [SerializeField] private LayerMask _layerMask;

        protected override void Awake()
        {
            base.Awake();
        }
        public override bool CanInteract()
        {
            return GetInteractDirection();
        }

        private bool GetInteractDirection()
        {
            Debug.Log($"Checking direction");
            // Overlap in the direction the door should be opened from
            Vector2 castCenter = (Vector2)transform.position + _offset;
            if (_openFromLeft)
            {
                castCenter -= _offset;
            }
            else
            {
                castCenter += _offset;
            }

            Collider2D collider = Physics2D.OverlapBox(castCenter, _castSize, 0f, _layerMask);
            DrawUtil.DrawRectangle(castCenter, _castSize / 2, Color.red);
            if (collider != null)
            {
                return collider.CompareTag("Player");
            }
            return false;
        }
    }
}