using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int destinationScene;

    void Start()
    {
        GetComponent<FaderTransition>().RevealContentUnderThisSprite();
    }

    public void SceneLoadAfterSeconds(int secondsToWaitBeforeReloading)
    {
        Debug.Log(string.Format("Loading scene {0} in {1}", destinationScene, secondsToWaitBeforeReloading));
        Invoke("SceneLoad", secondsToWaitBeforeReloading);
        FadeOut();
    }
    private void SceneLoad()
    {
        Debug.Log(string.Format("Loading scene {0} now", destinationScene));
        SceneManager.LoadScene(destinationScene);
    }

    void FadeOut()
    {
        GetComponent<FaderTransition>().HideContentUnderThisSprite();
    }
}
