using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New UI Style Data",menuName = "Data/UI/UI Style Data")]
public class UIStyleData : ScriptableObject
{
    [Tooltip("")]
    public bool AnimOnStart;

    [Tooltip("Play OnStart motion OnShow")]
    public bool AnimOnShow;

    [Tooltip("")]
    public bool AnimOnHide;

    [Header("Start/Show Anim Control")]
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

    [Header("Hide Anim Control")]

    [Tooltip("")]
    public Vector2 HideAnimExitOffset;

    [Tooltip("")]
    public AnimationCurve HideAnimMotionCurve;

    [Tooltip("")]
    public bool FadeOutOnHide;

    [Tooltip("")]
    public AnimationCurve HideAnimAlphaCurve;

    [Tooltip("")]
    public float HideAnimDuration;

    [Header("Misc")]
    [Tooltip("Value to shave off time calculations to prevent last-mile problems")]
    public float TRoundingAmount;
}
