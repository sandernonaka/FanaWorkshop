using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ConstrintInvertPosition : MonoBehaviour
{
    [field: SerializeField] private Transform InvertingTransform { get; set; }
    
    void Update()
    {
        InvertingTransform.localPosition = transform.localPosition * -1;
    }
}
