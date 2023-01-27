using UnityEngine;

[RequireComponent(typeof(Transform))]
public class SquashStretch : MonoBehaviour
{
    [field: Range(-0.9f, 1.5f)]
    [field: SerializeField]
    private float Stretch { get; set; } = 0;

    [field: SerializeField] private AnimationCurve AnimationCurve1 { get; set; }

    [field: SerializeField] private AnimationCurve AnimationCurve2 { get; set; }

    public void SetStretch(float stretch)
    {
        Stretch = stretch;
        UpdateProperties();
    }
    private void OnValidate()
    {
       UpdateProperties();
    }

    private void UpdateProperties()
    {
        float add = 2;
        float mult = 4;
        float squash1 = AnimationCurve1.Evaluate((Stretch + add) / mult) * mult - add;
        float squash2 = AnimationCurve2.Evaluate((Stretch + add) / mult) * mult - add;
        transform.localScale = new Vector3(2f - squash2, 1f + squash1, 2f - squash2);
    }
}