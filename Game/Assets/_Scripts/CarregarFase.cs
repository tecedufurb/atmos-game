using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CarregarFase : MonoBehaviour {

    public GameObject loadingScreenPanel;
    public Slider slider;//barra de carregar
    public string fase; //fase que sera carregada
    public GameObject loadingScreenCanvas;

    AsyncOperation async;

    public void LoadScreen()
    {
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        loadingScreenCanvas.active = true;
        loadingScreenPanel.SetActive(true);
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
