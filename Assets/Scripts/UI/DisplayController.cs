using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DisplayController : MonoBehaviour
{
    //Will control active canvas/canvas groups
    [SerializeField] Canvas _customizationScreen;
    [SerializeField] UIAnimElement _tutorialControls;


    private void Start()
    {
        ShowTutorialControls();
    }

    #region Display Toggles
    public void ShowCustomizationScreen()
    {

    }

    public void HideCustomizationScreen()
    {

    }
    public void ShowTutorialControls()
    {
        _tutorialControls.gameObject.SetActive(true);
    }
    public void HideTutorialControls()
    {
        _tutorialControls.gameObject.SetActive(false);
    }
    #endregion
}
