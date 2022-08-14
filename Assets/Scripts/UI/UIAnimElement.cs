using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIAnimElement : MonoBehaviour
{
    [SerializeField] UIStyleData _style;
    [SerializeField] RectTransform _rootRect;
    [SerializeField] CanvasGroup _alphaGroup;

    public UnityEvent OnShowEnd;

    public bool IsAnimating { get { return _animating; } }

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
        if (!_style.AnimOnHide) {
            HideElementImmediate();
            return;
        }

        InitAnimOnHideFlyOut();
        StartCoroutine(OnHideFlyOut());
        StartCoroutine(HideAfterAnim());
    }
    public void HideElementImmediate()
    {
        _rootRect.gameObject.SetActive(false);
    }
    public void HideElementAfterDelay(float seconds)
    {
        StartCoroutine(HideAfterDelay(seconds));
    }
    public void ShowElement()
    {
        _rootRect.gameObject.SetActive(true);
        if (!_style.AnimOnShow) return;
        InitAnimOnStartFlyIn();
        StartCoroutine(OnStartFlyIn());
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
    void InitAnimOnHideFlyOut()
    {
        _animStartTime = Time.time;
        _animating = true;
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

        OnShowEnd?.Invoke();
    }
    IEnumerator OnHideFlyOut()
    {
        while (Time.time < _animStartTime + _style.HideAnimDuration) {
            //Fly to offset from default position
            float t = RoundT((Time.time - _animStartTime) / _style.StartAnimDuration);
            Vector2 newPos = Vector2.Lerp(
                _defaultLocalPosition,
                _style.HideAnimExitOffset + _defaultLocalPosition,
                _style.HideAnimMotionCurve.Evaluate(t));
            _rootRect.localPosition = newPos;

            //Handle group alpha
            if (_style.FadeOutOnHide) _alphaGroup.alpha = _style.HideAnimAlphaCurve.Evaluate(t);

            yield return null;
        }

        //Animation Complete
        _animating = false;
    }
    #endregion

    #region Element Styling

    #endregion

    #region Utilities
    IEnumerator HideAfterAnim()
    {
        while (_animating) {
            yield return null;
        }

        //Anim complete, hide
        HideElementImmediate();
    }
    IEnumerator HideAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        HideElement();
    }
    float RoundT(float t)
    {
        if (t + _style.TRoundingAmount >= 1f) return 1f;
        if (t - _style.TRoundingAmount <= 0f) return 0f;

        return t;
    }
    #endregion
}
