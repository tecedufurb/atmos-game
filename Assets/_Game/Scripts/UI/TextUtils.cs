using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextUtils : MonoBehaviour {

    public void changeColorOfText(Text text, Color color) {
        text.color = color;
    }

    private IEnumerator changeSizeOfText(Text text, int scaleOfChangeInFont) {
        int maxFontSize = text.fontSize + scaleOfChangeInFont;
        int minFontSize = text.fontSize - 2;
        int currentSize = text.fontSize;
        bool scaleSizeUp = true;
        while (true) {
            if (scaleSizeUp) {
                currentSize += 3;
                text.fontSize = currentSize;
                if (currentSize >= maxFontSize)
                    scaleSizeUp = false;
            } else {
                currentSize -= 3;
                text.fontSize = currentSize;
                if (currentSize <= minFontSize)
                    scaleSizeUp = true;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }


    public IEnumerator moveTextDown(GameObject text, float distance, float velocity) {
        float destination = text.transform.position.y - distance;

        while (text.transform.position.y > destination) {
            text.transform.position += Vector3.down * velocity;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(text);
    }

}