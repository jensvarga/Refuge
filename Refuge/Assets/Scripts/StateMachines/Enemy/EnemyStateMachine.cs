using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
  [field: SerializeField] public Animator Animator { get; private set; }
  [field: SerializeField] public float PlayerChasingRange { get; private set; }
  [field: SerializeField] public float AttackRange { get; private set; }
  [field: SerializeField] public float MovementSpeed { get; private set; }
  [field: SerializeField] public CharacterController Controller { get; private set; }
  [field: SerializeField] public ForceReciver ForceReciver { get; private set; }
  [field: SerializeField] public NavMeshAgent Agent { get; private set; }
  [field: SerializeField] public WeaponDamage Weapon { get; private set; }
  [field: SerializeField] public int AttackDamage { get; private set; }
  [field: SerializeField] public float Knockback { get; private set; }
  [field: SerializeField] public Health health { get; private set; }
  [field: SerializeField] public Target Target { get; private set; }
  [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

  [field: SerializeField] public Health Player { get; private set; }

  private void Start()
  {
    //Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    Agent.updatePosition = false; // Use own movement code to move
    Agent.updateRotation = false;
    SwitchState(new EnemyIdleState(this));
  }

  // Temp to find player should update when a nre player enters the scene
  private void Update()
  {
    if (GameObject.FindGameObjectsWithTag("Player").Length > 0 && Player == null) 
    { 
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); 
    }
  }

  private void OnEnable()
  {
    health.OnTakeDamage += HandleTakeDamage;
    health.OnDie += HandleDie;
  }

  private void OnDisable()
  {
    health.OnTakeDamage -= HandleTakeDamage;
    health.OnDie -= HandleDie;
  }

  private void HandleTakeDamage()
  {
    SwitchState(new EnemyImpactState(this));
  }

  private void HandleDie()
  {
    SwitchState(new EnemyDeadState(this));
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
  }
}
