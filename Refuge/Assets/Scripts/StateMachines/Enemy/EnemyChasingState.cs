using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeDureation = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
      stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDureation);
    }

    public override void Tick(float deltaTime)
    {
      if (!IsInChaseRange())
      {
        // Transition to chase state
        stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        return;
      }
      else if (IsInAttackRange())
      {
        // Transition to attack state
        stateMachine.SwitchState(new EnemyAttackState(stateMachine));
        return;
      }

      MoveToPlayer(deltaTime);
      FacePlayer();
      stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
      if (stateMachine.Agent != null) {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
      }
    }

    private void MoveToPlayer(float deltaTime)
    {
      if (stateMachine.Agent.isOnNavMesh)
      {
        stateMachine.Agent.destination = stateMachine.Player.transform.position;
        Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
      }

      stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    private bool IsInAttackRange()
    {
      if (stateMachine.Player.isDead) { return false; }
      float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
      return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }
}
