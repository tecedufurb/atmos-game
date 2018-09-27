using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilsUI : MonoBehaviour {
    private AsyncOperation async;

    public void activateCanvas(GameObject canvasOrPanel) {
        canvasOrPanel.SetActive(true);
    }

    public void deactivateCanvas(GameObject canvasOrPanel) {
        canvasOrPanel.SetActive(false);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void LoadScene(string sceneName) {
        StartCoroutine(LoadingScreen(sceneName));
    }

    IEnumerator LoadingScreen(string sceneName) {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.isDone == false) {

            if (async.progress == 0.9f) {
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}