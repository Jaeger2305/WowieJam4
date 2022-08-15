using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneLoader : MonoBehaviour
{
    public int destinationScene;
    public UnityEvent loadingThisScene;
    public UnityEvent loadingNextScene;

    void Start()
    {
        loadingThisScene.Invoke();
    }

    public void SceneLoadAfterSeconds(int secondsToWaitBeforeReloading)
    {
        Debug.Log(string.Format("Loading scene {0} in {1}", destinationScene, secondsToWaitBeforeReloading));
        Invoke("SceneLoad", secondsToWaitBeforeReloading);
        loadingNextScene.Invoke();
    }


    private void SceneLoad()
    {
        Debug.Log(string.Format("Loading scene {0} now", destinationScene));
        SceneManager.LoadScene(destinationScene);
    }
}
