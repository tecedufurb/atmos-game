using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    private int mIndex;

    [SerializeField] private string[] m_Message = new string[3];
    [SerializeField] private Text messageText;
    [SerializeField] private Button nextMessageButton;
    [SerializeField] private Button previousMessageButton;
    [SerializeField] private Button nextPanelButton;

    void Start() {
        mIndex = 0;
        StartCoroutine(TypeText(m_Message[mIndex], messageText));
    }

    public void PreviousMessege() {
        StopAllCoroutines();
        mIndex--;
        messageText.text = m_Message[mIndex];

        if (mIndex == 0)
            previousMessageButton.gameObject.SetActive(false);

        nextMessageButton.gameObject.SetActive(true);
        nextPanelButton.GetComponent<ResizeObject>().m_Active = false;
    }

    public void NextMessege() {
        StopAllCoroutines();
        mIndex++;
        messageText.text = m_Message[mIndex];

        StartCoroutine(TypeText(m_Message[mIndex], messageText));

        if (mIndex == m_Message.Length - 1) {
            nextMessageButton.gameObject.SetActive(false);
            nextPanelButton.GetComponent<ResizeObject>().m_Active = true;
        }
        previousMessageButton.gameObject.SetActive(true);
    }

    private IEnumerator TypeText(string message, Text messageText) {
        messageText.text = "";
        foreach (char letter in message.ToCharArray()) {
            messageText.text += letter;
            yield return 0;
            yield return new WaitForSeconds(.05f);
        }
    }
}
