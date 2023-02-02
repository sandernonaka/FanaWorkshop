using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class AnimGhost : MonoBehaviour
{
    private enum TransformPosition
    {
        X,
        Y,
        Z
    }
    
    [field: SerializeField] private bool Hide { get; set; }

    [field: SerializeField] private int AnimationClipIndex { get; set; }

    [field: SerializeField] private AnimationClip[] AnimationClipList { get; set; }

    [field: SerializeField] private Animator Animator { get; set; }

    [field: SerializeField] private int FPS { get; set; }

    [field: SerializeField] private float DummySize { get; set; }

    private int TotalFrames { get; set; }

    private float TotalSecs { get; set; }

    private Dictionary<string, Vector3[]> CurvesDictionary { get; set; }

    // Start is called before the first frame update
    void Update()
    {
        if (Animator == null)
            return;

        if (AnimationClipIndex >= AnimationClipList.Length)
            return;

        CurvesDictionary = new Dictionary<string, Vector3[]>();
        ShowClipCurve(AnimationClipList[AnimationClipIndex]);
    }

    private void ShowClipCurve(AnimationClip clip)
    {
        TotalSecs = clip.length;

        TotalFrames = Mathf.FloorToInt(FPS * TotalSecs);

        foreach (var binding in AnimationUtility.GetCurveBindings(clip))
        {
            if (binding.type != typeof(Transform))
                continue;

            if (binding.propertyName == "m_LocalPosition.x")
            {
                AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                CurvesDictionary[binding.path] = GetPositionList(binding.path, curve, TransformPosition.X);
            }

            if (binding.propertyName == "m_LocalPosition.y")
            {
                AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                CurvesDictionary[binding.path] = GetPositionList(binding.path, curve, TransformPosition.Y);
            }

            if (binding.propertyName == "m_LocalPosition.z")
            {
                AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                CurvesDictionary[binding.path] = GetPositionList(binding.path, curve, TransformPosition.Z);
            }
        }
    }

    private Vector3[] GetPositionList(string path, AnimationCurve curve, TransformPosition transformPosition)
    {
        Vector3[] positionsList;

        if (CurvesDictionary.ContainsKey(path))
            positionsList = CurvesDictionary[path];
        else
            positionsList = new Vector3[TotalFrames];

        for (int i = 1; i < TotalFrames; i++)
        {
            float percentage = i / (float) TotalFrames;

            float time = TotalSecs * percentage;

            float value = curve.Evaluate(time);

            if (transformPosition == TransformPosition.X)
                positionsList[i].x = value;
            if (transformPosition == TransformPosition.Y)
                positionsList[i].y = value;
            if (transformPosition == TransformPosition.Z)
                positionsList[i].z = value;
        }

        return positionsList;
    }

    private void OnDrawGizmos()
    {
        if (CurvesDictionary == null || Hide)
            return;

        foreach (var curvesInfo in CurvesDictionary)
        {
            foreach (var position in curvesInfo.Value)
            {
                Gizmos.DrawSphere(position, DummySize);
            }
        }
    }
}