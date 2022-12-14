using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3> OnLedgeDetect;

    private void OnTriggerEnter(Collider other)
    {
      OnLedgeDetect?.Invoke(other.ClosestPointOnBounds(transform.position), other.transform.forward);
    }
}
