using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ConstraintInvertRotation : MonoBehaviour
{
    [field: SerializeField] private Transform InvertingTransform { get; set; }

    void Update()
    {
        InvertingTransform.localEulerAngles = transform.localEulerAngles * -1;
    }
}