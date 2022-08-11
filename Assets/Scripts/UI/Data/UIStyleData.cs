using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New UI Style Data",menuName = "Data/UI/UI Style Data")]
public class UIStyleData : ScriptableObject
{
    [Tooltip("")]
    public bool AnimOnStart;

    [Tooltip("")]
    public bool AnimOnHide;

    [Tooltip("")]
    public Vector2 StartAnimEntryOffset;

    [Tooltip("")]
    public AnimationCurve StartAnimMotionCurve;

    [Tooltip("")]
    public bool FadeInOnStart;

    [Tooltip("")]
    public AnimationCurve StartAnimAlphaCurve;

    [Tooltip("")]
    public float StartAnimDuration;

    [Tooltip("Value to shave off time calculations to prevent last-mile problems")]
    public float TRoundingAmount;
}
