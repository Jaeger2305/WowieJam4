using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimElement : MonoBehaviour
{
    [SerializeField] UIStyleData _style;
    [SerializeField] RectTransform _rootRect;
    [SerializeField] Image _img;
    [SerializeField] CanvasGroup _alphaGroup;

    Vector2 _defaultLocalPosition;

    float _animStartTime;

    bool _animating;

    #region Unity
    private void Awake()
    {
        //Get references

        //Store unanimated state parameters
        _defaultLocalPosition = _rootRect.localPosition;
    }

    private void Start()
    {
        if (_style.AnimOnStart) {
            InitAnimOnStartFlyIn();
            StartCoroutine(OnStartFlyIn());
        }
    }
    #endregion

    #region Public Controls
    public void HideElement()
    {

    }
    public void ShowElement()
    {

    }
    #endregion

    #region Animation
    #region Anim Initialization
    void InitAnimOnStartFlyIn()
    {
        _rootRect.localPosition = _defaultLocalPosition + _style.StartAnimEntryOffset;
        _animStartTime = Time.time;
        _animating = true;
        if (_style.FadeInOnStart) _alphaGroup.alpha = _style.StartAnimAlphaCurve.Evaluate(0f);
    }
    #endregion
    IEnumerator OnStartFlyIn()
    {
        while (Time.time < _animStartTime + _style.StartAnimDuration) {
            //Fly in from offset to default position
            float t = RoundT((Time.time - _animStartTime)/_style.StartAnimDuration);
            Vector2 newPos = Vector2.Lerp(
                _style.StartAnimEntryOffset + _defaultLocalPosition, 
                _defaultLocalPosition, 
                _style.StartAnimMotionCurve.Evaluate(t));
            _rootRect.localPosition = newPos;

            //Handle group alpha
            if (_style.FadeInOnStart) _alphaGroup.alpha = _style.StartAnimAlphaCurve.Evaluate(t);

            yield return null;
        }

        //Animation Complete
        _animating = false;
    }
    #endregion

    #region Element Styling

    #endregion

    #region Utilities
    float RoundT(float t)
    {
        if (t + _style.TRoundingAmount >= 1f) return 1f;
        if (t - _style.TRoundingAmount <= 0f) return 0f;

        return t;
    }
    #endregion
}
