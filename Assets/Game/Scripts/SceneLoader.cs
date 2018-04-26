using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour {

    public Slider slider; // loading bat
    private string fase; // fase that will be loaded
    private bool clickable = true; // validate if loading hasn't already started

    private AsyncOperation async;

    public void LoadScene(string fase) {
        gameObject.SetActive(true);
        this.fase = fase;
        if (clickable) {
            clickable = false;
            StartCoroutine(LoadingScreen());
        }
    }

    IEnumerator LoadingScreen()
    {
        async = SceneManager.LoadSceneAsync(fase);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    
}
