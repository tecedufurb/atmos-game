using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenHandle : MonoBehaviour {

    public string[] message = new string[3];

    private int index;

    [SerializeField] private Text messageText;
    [SerializeField] private Button nextMessageButton;
    [SerializeField] private Button previousMessageButton;
    [SerializeField] private GameObject choosePlantsButton;
    [SerializeField] private GameObject introductionPanel;
    [SerializeField] private GameObject choosePlantsPanel;

    void Start() {
        index = 0;
        messageText.text = message[index];
        StartCoroutine(TypeText(message[index], messageText));
    }

    public void previousMessege() {
        StopAllCoroutines();
        index--;
        messageText.text = message[index];

        if (index == 0)
            previousMessageButton.gameObject.SetActive(false);

        nextMessageButton.gameObject.SetActive(true);
        choosePlantsButton.GetComponent<ResizeObject>().m_Active = false;
    }

    public void nextMessege() {
        index++;
        messageText.text = message[index];

        StartCoroutine(TypeText(message[index], messageText));

        if (index == message.Length - 1) {
            nextMessageButton.gameObject.SetActive(false);
            choosePlantsButton.GetComponent<ResizeObject>().m_Active = true;
        }
            

        previousMessageButton.gameObject.SetActive(true);
    }

    public void EnableChoosePlants(bool active) {
        introductionPanel.SetActive(!active);
        choosePlantsPanel.SetActive(active);
    }
    
    private IEnumerator TypeText(string message, Text messageText) {
        messageText.text = "";
        foreach (char letter in message.ToCharArray()) {
            messageText.text += letter;
            //if (typeSound1 && typeSound2)
                //SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            yield return 0;
            yield return new WaitForSeconds(.05f);
        }
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
