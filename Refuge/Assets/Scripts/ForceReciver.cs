using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReciver : MonoBehaviour
{
  [SerializeField] private CharacterController controller;
  [SerializeField] private NavMeshAgent agent;
  [SerializeField] private float drag = 0.3f;
  [SerializeField] private float JumpHeight = 1.3f;

  private Vector3 dampingVelocity; // ref value for SmoothDamp()
  private Vector3 impact;
  private float verticalVelocity;

  // Impact + gravity
  public Vector3 Movement => impact + Vector3.up * verticalVelocity;

  private void Update()
  {
    if (verticalVelocity < 0f && controller.isGrounded)
    {
      verticalVelocity = Physics.gravity.y * Time.deltaTime;
    }
    else
    {
      verticalVelocity += Physics.gravity.y * Time.deltaTime;
    }

    impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

    if (agent != null)
    {
      if (impact.sqrMagnitude < 0.2f * 0.2f) // Use small value so agent don't run in place
      {
        impact = Vector3.zero;
        agent.enabled = true;
      }
    }
  }

  public void AddForce(Vector3 force)
  {
    impact += force;
    if (agent != null) {
        agent.enabled = false;
    }
  }

  public void Jump(float jumpForce)
  {
    verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);;
  }

  public void Reset()
  {
    verticalVelocity = 0f;
    impact = Vector3.zero;
  }
}
