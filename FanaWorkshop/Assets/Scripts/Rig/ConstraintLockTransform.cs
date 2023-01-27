using UnityEngine;

[ExecuteAlways]
public class ConstraintLockTransform : MonoBehaviour
{
    [System.Serializable]
    private class LockingTransform
    {
        [field: SerializeField] public bool X { get; set; }
        [field: SerializeField] public float XValue { get; set; }
        [field: SerializeField] public bool Y { get; set; }
        [field: SerializeField] public float YValue { get; set; }
        [field: SerializeField] public bool Z { get; set; }
        [field: SerializeField] public float ZValue { get; set; }
    }

    [field: SerializeField] private LockingTransform LockingTransformPos { get; set; }
    [field: SerializeField] private LockingTransform LockingTransformRot { get; set; }
    [field: SerializeField] private LockingTransform LockingTransformScale { get; set; }

    void Update()
    {
        Transform thisTransform = transform;
        
        Vector3 position = thisTransform.position;

        if (LockingTransformPos.X)
            position.x = LockingTransformPos.XValue;
        if (LockingTransformPos.Y)
            position.y = LockingTransformPos.YValue;
        if (LockingTransformPos.Z)
            position.z = LockingTransformPos.ZValue;

        transform.position = position;
        
        Vector3 localScale = thisTransform.localScale;

        if (LockingTransformScale.X)
            localScale.x = LockingTransformScale.XValue;
        if (LockingTransformScale.Y)
            localScale.y = LockingTransformScale.YValue;
        if (LockingTransformScale.Z)
            localScale.z = LockingTransformScale.ZValue;

        transform.localScale = localScale;
        
        Vector3 localEulerAngles = thisTransform.eulerAngles;

        if (LockingTransformRot.X)
            localEulerAngles.x = LockingTransformRot.XValue;
        if (LockingTransformRot.Y)
            localEulerAngles.y = LockingTransformRot.YValue;
        if (LockingTransformRot.Z)
            localEulerAngles.z = LockingTransformRot.ZValue;

        transform.eulerAngles = localEulerAngles;
    }
}