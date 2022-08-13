using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] bool _activeOnAwake;
    [SerializeField] GameObject _activeRoot;

    UIAnimElement[] _animChildren;

    public bool ScreenActive { get; private set; }

    private void Awake()
    {
        _animChildren = GetComponentsInChildren<UIAnimElement>(true);
        ScreenActive = _activeOnAwake;
        _activeRoot.SetActive(_activeOnAwake);
    }

    public void ShowScreen()
    {
        ScreenActive = true;
        _activeRoot.SetActive(true);
        foreach (var element in _animChildren) {
            element.ShowElement();
        }
    }

    public void HideScreen(float hideDelay = 0f, bool hideImmediate = false)
    {
        foreach (var element in _animChildren) {
            if (hideImmediate) element.HideElementImmediate();
            else if (hideDelay > 0f) element.HideElementAfterDelay(hideDelay);
            else element.HideElement();
        }

        if (hideImmediate) {
            DeactivateScreen();
            return;
        }
        StartCoroutine(DeactivateAfterAnims());
    }

    void DeactivateScreen()
    {
        ScreenActive = false;
        _activeRoot.SetActive(false);
    }

    IEnumerator DeactivateAfterAnims()
    {
        foreach (var element in _animChildren) {
            while (element.IsAnimating) { 
                yield return null; 
            }
        }

        //All children idle
        DeactivateScreen();

    }
}
