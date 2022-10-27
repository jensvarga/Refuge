using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
  private readonly int AttackHash = Animator.StringToHash("DownChop");
  private readonly int SpeedHash = Animator.StringToHash("Speed");
  private const float TransitionDuration = 0.1f;
  private const float AnimatorDampTime = 0.1f;
  private readonly string AttackString = "Attack";

  public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine) {}

  public override void Enter()
  {
    FacePlayer();
    stateMachine.Weapon?.SetAttack(stateMachine.AttackDamage, stateMachine.Knockback);
    stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
  }

  public override void Tick(float deltaTime)
  {
    //Move(deltaTime);
    float normalizedTime = GetNormalizedTime(stateMachine.Animator, AttackString);
    if (normalizedTime >= 1f)
    {
      // Switch back to EnemyChasingState if animation is done
      // (then it switches back here again if we are still within attack range)
      stateMachine.SwitchState(new EnemyChasingState(stateMachine));
    }

  }

  public override void Exit() {}
}
