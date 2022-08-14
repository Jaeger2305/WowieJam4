using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderTransition : MonoBehaviour
{
    private SpriteRenderer fadingSprite;
    public float fadeSpeed = 1f;
    private float destinationOpacity = 1f;
    private float currentOpacity = 0f;
    void Start()
    {
        fadingSprite = transform.Find("Fader").GetComponent<SpriteRenderer>();
        ApplyOpacity(currentOpacity);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOpacity != destinationOpacity)
        {
            bool isIncreasingOpacity = currentOpacity <= destinationOpacity;
            float opacityDelta = Time.deltaTime * fadeSpeed;
            opacityDelta *= isIncreasingOpacity ? 1 : -1;
            currentOpacity = Mathf.Clamp(currentOpacity + opacityDelta, 0f, 1f);
            ApplyOpacity(currentOpacity);
        }
    }

    public void HideContentUnderThisSprite()
    {
        currentOpacity = 0f;
        destinationOpacity = 1f;
    }
    public void RevealContentUnderThisSprite()
    {
        currentOpacity = 1f;
        destinationOpacity = 0f;
    }

    private void ApplyOpacity(float opacity)
    {
        fadingSprite.color = new Color(fadingSprite.color.r, fadingSprite.color.g, fadingSprite.color.b, opacity);
    }
}
