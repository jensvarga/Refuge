using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    private bool isInvulnerable;
    public bool isDead => health == 0f;
    public event Action OnTakeDamage;
    public event Action OnDie;

    private void Start()
    {
      health = maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
      this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damage) {
      if (health == 0f) { return; }
      if (isInvulnerable) { return; }
      health = Mathf.Max(health - damage, 0);
      OnTakeDamage?.Invoke();
      if (health == 0f)
      {
        OnDie?.Invoke();
      }
    }

}
