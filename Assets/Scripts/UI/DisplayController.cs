using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    //Will control active canvas/canvas groups
    [SerializeField] Canvas _customizationScreen;

    [Header("Tutorial Elements")]
    [SerializeField] UIAnimElement _tutorialControls;
    [SerializeField] float _tutorialDisplayDuration;


    private void Start()
    {
        ShowTutorialControls();
    }

    #region Display Toggles
    public void ShowCustomizationScreen()
    {
        _customizationScreen.enabled = true;
        //TODO: Show all UIAnimElement children
    }

    public void HideCustomizationScreen()
    {
        //TODO: Disable canvas after UIAnimElement children are done animating
    }
    public void ShowTutorialControls()
    {
        _tutorialControls.ShowElement();
        _tutorialControls.HideElementAfterDelay(_tutorialDisplayDuration);
    }
    public void HideTutorialControls()
    {
        _tutorialControls.HideElement();
    }
    #endregion
}
