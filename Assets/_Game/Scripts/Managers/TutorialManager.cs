using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameManager gameManager;
    public TextUtils textUtils;
    public GameObject instructionsPanel;

    public GameObject outOfSeedsMessagePrefab;

    private string[] texto;
    private bool alreadyShowingOutOfSeedsMessage;

    public void displayInstructions() {
        instructionsPanel.SetActive(true);
        gameManager.pauseGame();
    }

    public void closeInstructions() {
        instructionsPanel.SetActive(false);
        gameManager.resumeGame();
    }

    public void showMessageOutOfSeeds() {
        if (alreadyShowingOutOfSeedsMessage)
            return;

        GameObject message = Instantiate(outOfSeedsMessagePrefab, outOfSeedsMessagePrefab.transform.parent);
        message.SetActive(true);
        StartCoroutine(textUtils.moveTextDown(message, 200, 5));
        StartCoroutine(waitBeforeCanShownNewOutOfSeedsMessage(1.5f));
    }

    private IEnumerator waitBeforeCanShownNewOutOfSeedsMessage(float seconds) {
        alreadyShowingOutOfSeedsMessage = true;
        yield return new WaitForSeconds(seconds);
        alreadyShowingOutOfSeedsMessage = false;
    }
    
}