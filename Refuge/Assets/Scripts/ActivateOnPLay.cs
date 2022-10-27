using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnPLay : MonoBehaviour
{
    [field: SerializeField] public Renderer Renderer { get; private set; }

    private void Start()
    {
        Renderer.enabled = true;
    }
}
