using System;
using UnityEngine;

[ExecuteAlways]
public class BallRigger : MonoBehaviour
{
    [field: SerializeField] private bool ShowHelper { get; set; }

    [field: Range(-0.9f, 1.5f)]
    [field: SerializeField]
    private float Stretch { get; set; }

    [field: Range(-360, 360)]
    [field: SerializeField]
    private float StretchDirection { get; set; }

    [field: Range(-0.5f, 0.5f)]
    [field: SerializeField]
    private float StretchPivotX { get; set; }

    [field: Range(-0.5f, 0.5f)]
    [field: SerializeField]
    private float StretchPivotY { get; set; }

    [field: HideInInspector]
    [field: SerializeField]
    private SquashStretch SquashStretch { get; set; }

    [field: HideInInspector]
    [field: SerializeField]
    private Transform SquashRotationTransform { get; set; }

    [field: HideInInspector]
    [field: SerializeField]
    private Transform SquashPivotTransform { get; set; }


    private void Update()
    {
        UpdateProperties();
    }

    private void OnValidate()
    {
        UpdateProperties();
    }

    private void UpdateProperties()
    {
        SquashStretch.SetStretch(Stretch);
        SquashRotationTransform.localEulerAngles = new Vector3(0, 0, StretchDirection);
        SquashPivotTransform.localPosition = new Vector3(StretchPivotX, StretchPivotY, 0);
    }

    private void OnDrawGizmos()
    {
        if (!ShowHelper)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(SquashPivotTransform.position, 0.1f);
    }
}