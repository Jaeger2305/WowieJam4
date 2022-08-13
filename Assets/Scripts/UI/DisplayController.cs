using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    //Will control active canvas/canvas groups
    [SerializeField] UIScreen _customizationScreen;

    [Header("Tutorial Elements")]
    [SerializeField] UIAnimElement _tutorialControls;
    [SerializeField] float _tutorialDisplayDuration;

    bool _customizing;

    private void Start()
    {
        ShowTutorialControls();
    }

    #region Display Toggles
    public void ShowCustomizationScreen()
    {
        //_customizationScreen.enabled = true;
        _customizationScreen.ShowScreen();
        _customizing = true;
    }

    public void HideCustomizationScreen()
    {
        _customizationScreen.HideScreen();
        _customizing = false;
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


    //!!!
    //Placeholder method for testing customization screen
    //Will cause nonfatal errors if the input is spammed while coroutines are running
    //Actual implementation of player input processing should either:
    //      Have cooldown for button press (don't process for x seconds)
    //      Or have _customizing return true if screen has not finished hiding
    public void TestShowTutorial(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.canceled) return;
        if (_customizing) HideCustomizationScreen();
        else ShowCustomizationScreen();
    }
}
