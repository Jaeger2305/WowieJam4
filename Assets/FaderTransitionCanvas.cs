using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderTransitionCanvas : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1f;
    private float destinationOpacity = 1f;
    private float currentOpacity = 0f;

    void Update()
    {
        // urggghh so hacky - better to call a tween once, but this is just copy paste under time pressure
        if (currentOpacity != destinationOpacity)
        {
            bool isIncreasingOpacity = currentOpacity <= destinationOpacity;
            float opacityDelta = Time.deltaTime * fadeSpeed;
            opacityDelta *= isIncreasingOpacity ? 1 : -1;
            currentOpacity = Mathf.Clamp(currentOpacity + opacityDelta, 0f, 1f);
            ApplyOpacity(currentOpacity);
        }
    }

    public void HideContentInGroup()
    {
        currentOpacity = 0f;
        destinationOpacity = 1f;
    }
    public void RevealContentInGroup()
    {
        currentOpacity = 1f;
        destinationOpacity = 0f;
    }

    private void ApplyOpacity(float opacity)
    {
        canvasGroup.alpha = opacity;
    }
}
