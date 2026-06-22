using System;
using UnityEngine;

namespace Tools.DesignTools
{
    public class VisualizeJumpController : MonoBehaviour
    {
        [SerializeField] private VisualizeJump _runJump;
        [SerializeField] private VisualizeJump _sprintJump;

        [Header("Change Start Direction")]
        public bool FlipDirection;

        [Header("Sim Steps")]
        public int Steps = 200;

        [Header("Drawing Controls")]
        public bool DrawRunJump;
        public bool DrawSprintJump;
        public bool DrawWallJump;

        public bool DrawEarlyJump;
        public bool DrawLateJump;



        private void OnValidate()
        {
            _runJump.Steps = Steps;
            _sprintJump.Steps = Steps;


            FlipInitialVelocity();

            ToggleRunJump();
            ToggleSprintJump();
            ToggleWallJump();
        }

        private void FlipInitialVelocity()
        {
            _runJump.Flip = FlipDirection;
            _sprintJump.Flip = FlipDirection;
        }

        private void ToggleRunJump()
        {
            _runJump.DrawEarlyJump = DrawEarlyJump && DrawRunJump;
            _runJump.DrawJump = DrawRunJump;
            _runJump.DrawLateJump = DrawLateJump && DrawRunJump;
        }

        private void ToggleSprintJump()
        {
            _sprintJump.DrawEarlyJump = DrawEarlyJump && DrawSprintJump;
            _sprintJump.DrawJump = DrawSprintJump;
            _sprintJump.DrawLateJump = DrawLateJump && DrawSprintJump;
        }

        private void ToggleWallJump()
        {
            _runJump.DrawWallJump = DrawWallJump && DrawRunJump;

            _sprintJump.DrawWallJump = DrawWallJump && DrawSprintJump;
        }
    }
}