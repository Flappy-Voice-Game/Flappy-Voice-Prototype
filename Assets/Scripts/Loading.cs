using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private static bool shouldPlayOpeningAnimation = false;

    private Animator anim;
    private AsyncOperation loadingSceneOperation;

    public void SwitchScene(string sceneName)
    {
        anim.SetTrigger("sceneClosing");

        loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingSceneOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation) anim.SetTrigger("sceneOpening");
    }

    public void OnAnimationOver()
    {
        shouldPlayOpeningAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}
