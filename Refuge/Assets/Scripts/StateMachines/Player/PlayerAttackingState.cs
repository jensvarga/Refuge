using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private Attack attack;
    private bool alreadyAppliesForce;
    private readonly string AttackString = "Attack";

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
      attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
      stateMachine.Weapon?.SetAttack(attack.Damage, attack.Knockback);
      stateMachine.Kick?.SetAttack(attack.Damage, attack.Knockback);
      stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
      Move(deltaTime);

      FaceTarget();

      float normalizedTime = GetNormalizedTime(stateMachine.Animator, AttackString);

      if (normalizedTime < 1f)
      {
        if (normalizedTime >= attack.ForceTime)
        {
          TryApplyForce();
        }

        if (stateMachine.InputReader.IsAttacking)
        {
          TryComboAttack(normalizedTime);
        }
      }
      else
      {
        // Go back to locomotion
        if (stateMachine.Targeter.CurrentTarget != null)
        {
          stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
          stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
      }

      previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
    }

    private void TryComboAttack(float normalizedTime)
    {
      if (attack.ComboStateIndex == -1) { return; } // Attack can't combo

      if (normalizedTime < attack.ComboAttackTime) {return; } // Far enough throu current animation to combo

      stateMachine.SwitchState
      (
        new PlayerAttackingState
        (
          stateMachine,
          attack.ComboStateIndex
        )
      );
    }

    private void TryApplyForce()
    {
      if (alreadyAppliesForce) { return; }
      stateMachine.ForceReciver.AddForce(stateMachine.transform.forward * attack.Force);
      alreadyAppliesForce = true;
    }

}
