using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CarregarFase : MonoBehaviour {

    
    public Slider slider;//barra de carregar
    public string fase; //fase que sera carregada
    public GameObject loadingScreenCanvas;
    private bool podeClicar = true; //validar se ja não hoube click e esta carregando

    AsyncOperation async;

    public void LoadScreen()
    {
        if (podeClicar) {
            podeClicar = false;
            StartCoroutine(LoadingScreen());
        }
        
    }

    public void LoadScreen(string fase) {
        this.fase = fase;
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        loadingScreenCanvas.active = true;
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
