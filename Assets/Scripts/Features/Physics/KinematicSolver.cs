using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Features.Movement.Abstractions;
using Movement.Features.Movement.Services;
using Physics.Core.Abstractions;
using Primitives.Physics;
using Physics.Core.DataStructures;
using Features.Movement.Services;

namespace Physics.Features.Movement
{
    public class KinematicSolver : ISolveKinematics
    {

        private ICalcAction _calcJump;
        private ICalcAction _calcFall;
        private ICalcAction _calcRun;
        private ICalcAction _calcRunStop;
        private ICalcAction _calcLanding;
        private ICalcAction _calcJumpCancel;
        private ICalcAction _calcQuickStep;
        private ICalcAction _calcExternalImpulse;
        private ICalcAction _calcExternalContinuous;
        private ICalcAction _calcKnockBack;
        private ICalcAction _calcTeleport;
        private ICalcAction _calcFly;
        private ICalcAction _calcQuickStepStop;
        private ICalcAction _calcQuickStepUpdate;
        private ICalcAction _calcRotate;
        private ICalcAction _calcDoggoDash;
        private ICalcAction _calcDoggoDashUpdate;
        private ICalcAction _calcFriction;
        private ICalcAction _calcLunge;
        private ICalcAction _calcClimb;
        private ICalcAction _calcClimbStop;
        public KinematicSolver()
        {
            // Set up your calculation actions
            _calcJump = new CalcJump();
            _calcFall = new CalcFall();
            _calcRun = new CalcRun();
            _calcRunStop = new CalcRunStop();
            _calcLanding = new CalcLanding();
            _calcJumpCancel = new CalcJumpCancel();
            _calcQuickStep = new CalcQuickStep();
            _calcExternalImpulse = new CalcExternalImpulse();
            _calcExternalContinuous = new CalcExternalContinuous();
            // _calcKnockBack = new CalcKnockBack();
            _calcTeleport = new CalcTeleport();
            _calcFly = new CalcFly();
            _calcQuickStepStop = new CalcQuickStepStop();
            _calcQuickStepUpdate = new CalcQuickStepUpdate();
            _calcRotate = new CalcRotate();
            _calcDoggoDash = new CalcDodge();
            _calcDoggoDashUpdate = new CalcDodgeUpdate();
            _calcFriction = new CalcFriction();
            _calcLunge = new CalcLunge();
            _calcClimb = new CalcClimb();
            _calcClimbStop = new CalcClimbStop();
        }

        public KinematicResult Solve(ActorFrameData frameData)
        {
            frameData.CurrentState.ExternalVelocity = Vector2.zero;

            // Stop increasing fall speed when grounded.

            // NOTE Shouldn't this belong in a result some where? I don't think I should be looking at the physics context at this point...
            if ((frameData.PhysicsContext.IsGrounded || frameData.PhysicsContext.IsOnPlatform) && frameData.CurrentState.Velocity.y < 0)
            {
                // frameData.CurrentState.Velocity.y = 0;
                frameData.CurrentState.Gravity = 0;
            }

            foreach (IActionResult result in frameData.Results)
            {
                // Debug.Log($"Got result: {result.ResultType}; Approval: {result.Approved}");
                if (!result.Approved) continue;

                switch (result)
                {
                    case JumpResult jump:
                        {
                            // Debug.Log("Jump case");
                            frameData.CurrentState = _calcJump.Calculate(frameData.ActorStats, jump, ref frameData.CurrentState);
                            break;
                        }
                    case JumpCancelResult jumpCancel:
                        {
                            // Debug.Log("Jump cancel case");
                            frameData.CurrentState = _calcJumpCancel.Calculate(frameData.ActorStats, jumpCancel, ref frameData.CurrentState);
                            break;
                        }
                    case FallResult fall:
                        {
                            // Debug.Log($"Fall case for {frameData.DebugName}");
                            frameData.CurrentState = _calcFall.Calculate(frameData.ActorStats, fall, ref frameData.CurrentState);
                            break;
                        }
                    case RunResult run:
                        {
                            // Debug.Log($"Run case input: {run.Value}");
                            // Debug.Log($"Before speed: {frameData.CurrentState.Velocity}");
                            frameData.CurrentState = _calcRun.Calculate(frameData.ActorStats, run, ref frameData.CurrentState);
                            // Debug.Log($"After speed: {frameData.CurrentState.Velocity}");
                            break;
                        }
                    case RunStopResult runStop:
                        {
                            // Debug.Log("Run stop case");
                            frameData.CurrentState = _calcRunStop.Calculate(frameData.ActorStats, runStop, ref frameData.CurrentState);
                            break;
                        }
                    case LandingResult landing:
                        {
                            frameData.CurrentState = _calcLanding.Calculate(frameData.ActorStats, landing, ref frameData.CurrentState);
                            break;
                        }
                    case QuickStepResult quickStep:
                        {
                            // Debug.Log("Quick step case");
                            frameData.CurrentState = _calcQuickStep.Calculate(frameData.ActorStats, quickStep, ref frameData.CurrentState);
                            break;
                        }
                    case QuickStepUpdateResult quickStepUpdate:
                        {
                            // Debug.Log("Quick step update case");
                            frameData.CurrentState = _calcQuickStepUpdate.Calculate(frameData.ActorStats, quickStepUpdate, ref frameData.CurrentState);
                            break;
                        }
                    case QuickStepStopResult quickStepStop:
                        {
                            // Debug.Log("Quick step stop case");
                            frameData.CurrentState = _calcQuickStepStop.Calculate(frameData.ActorStats, quickStepStop, ref frameData.CurrentState);
                            break;
                        }
                    case ExternalImpulseResult extImp:
                        {
                            // Debug.Log("External impulse case");
                            frameData.CurrentState = _calcExternalImpulse.Calculate(frameData.ActorStats, extImp, ref frameData.CurrentState);
                            break;
                        }
                    case ExternalContinuousResult extCont:
                        {
                            // Debug.Log("External continuous case");
                            frameData.CurrentState = _calcExternalContinuous.Calculate(frameData.ActorStats, extCont, ref frameData.CurrentState);
                            break;
                        }
                    // case KnockBackResult knockBack:
                    //     {
                    //         // Debug.Log("Knock back case");
                    //         frameData.CurrentState = _calcKnockBack.Calculate(frameData.ActorStats, knockBack, ref frameData.CurrentState);
                    //         break;
                    //     }
                    case TeleportResult teleport:
                        {
                            // Debug.Log("Teleport case");
                            frameData.CurrentState = _calcTeleport.Calculate(frameData.ActorStats, teleport, ref frameData.CurrentState);
                            break;
                        }
                    case FlyResult fly:
                        {
                            // Debug.Log("Flight case");
                            frameData.CurrentState = _calcFly.Calculate(frameData.ActorStats, fly, ref frameData.CurrentState);
                            break;
                        }
                    case RotateResult rotate:
                        {
                            // Debug.Log("Rotate case");
                            frameData.CurrentState = _calcRotate.Calculate(frameData.ActorStats, rotate, ref frameData.CurrentState);
                            break;
                        }
                    case DodgeResult dodge:
                        {
                            // Debug.Log($"Dodge case");
                            frameData.CurrentState = _calcDoggoDash.Calculate(frameData.ActorStats, dodge, ref frameData.CurrentState);
                            break;
                        }
                    case DodgeUpdateResult dodgeUpdate:
                        {
                            // Debug.Log($"Dodge update case");
                            frameData.CurrentState = _calcDoggoDashUpdate.Calculate(frameData.ActorStats, dodgeUpdate, ref frameData.CurrentState);
                            break;
                        }
                    case FrictionResult friction:
                        {
                            // Debug.Log($"Friction update case");
                            frameData.CurrentState = _calcFriction.Calculate(frameData.ActorStats, friction, ref frameData.CurrentState);
                            break;
                        }
                    case LungeResult lunge:
                        {
                            // Debug.Log($"Friction update case");
                            frameData.CurrentState = _calcLunge.Calculate(frameData.ActorStats, lunge, ref frameData.CurrentState);
                            break;
                        }
                    case ClimbResult climb:
                        {
                            Debug.Log($"Calc Climb case");
                            frameData.CurrentState = _calcClimb.Calculate(frameData.ActorStats, climb, ref frameData.CurrentState);
                            break;
                        }
                    case ClimbStopResult climbStop:
                        {
                            Debug.Log($"Calc Climb case");
                            frameData.CurrentState = _calcClimbStop.Calculate(frameData.ActorStats, climbStop, ref frameData.CurrentState);
                            break;
                        }

                }
                // Debug.Log($"vel: {frameData.CurrentState.Velocity} - Frame: {Time.frameCount}");
                // Debug.Log($"Returning new kinematic state from action {result.ResultType}: vel: {frameData.CurrentState.Velocity}, accel: {frameData.CurrentState.Acceleration}, gravity: {frameData.CurrentState.Gravity}");
            }

            return frameData.CurrentState;
        }
    }
}