using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
  private readonly int JumpHash = Animator.StringToHash("Jump");

  private const float CrossFadeDuration = 0.1f;
  private Vector3 momentum;
  private float colliderHeight;

  public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

  public override void Enter()
  {
    stateMachine.ForceReciver.Jump(stateMachine.JumpForce);
    momentum = stateMachine.Controller.velocity;
    momentum.y = 0f;
    stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
    colliderHeight = stateMachine.Controller.height;
    stateMachine.Controller.height = colliderHeight * 0.50f;
    stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
  }

  public override void Tick(float deltaTime)
  {
    Vector3 movement = CalculateMovement();

      Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
      //Move(momentum, deltaTime);

    if (stateMachine.Controller.velocity.y <= 0f)
    {
      stateMachine.SwitchState(new PlayerFallingState(stateMachine));
      return;
    }
    FaceTarget();
  }

  private void HandleLedgeDetect(Vector3 closestPoint, Vector3 ledgeForward)
  {
    stateMachine.SwitchState(new PlayerHangingState(stateMachine, closestPoint, ledgeForward));
  }

  public override void Exit()
  {
    stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetect;
    stateMachine.Controller.height = colliderHeight;;
  }

  private Vector3 CalculateMovement()
  {
    Vector3 forward = stateMachine.MainCameraTransform.forward;
    Vector3 right = stateMachine.MainCameraTransform.right;

    forward.y = 0;
    right.y = 0;

    forward.Normalize();
    right.Normalize();

    return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
  }
}
