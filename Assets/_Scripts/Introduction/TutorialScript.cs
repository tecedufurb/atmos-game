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

    public void PreviousMessege() {
        mIndex--;
        messageText.text = m_Message[mIndex];

        if (mIndex == 0)
            previousMessageButton.gameObject.SetActive(false);

        nextMessageButton.gameObject.SetActive(true);
    }

    public void NextMessege() {
        mIndex++;
        messageText.text = m_Message[mIndex];

        if (mIndex == m_Message.Length - 1) {
            nextMessageButton.gameObject.SetActive(false);
        }
        previousMessageButton.gameObject.SetActive(true);
    }

    void OnEnable() {//aways start on message index 0
        mIndex = 0;
        messageText.text = m_Message[mIndex];
        previousMessageButton.gameObject.SetActive(false);
        nextMessageButton.gameObject.SetActive(true);
    }
}
