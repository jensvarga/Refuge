using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetNameCanvasCamera : MonoBehaviour
{
    [field: SerializeField] Canvas Canvas;

    void Start()
    {
        Canvas.worldCamera = Camera.main;
    }
}
