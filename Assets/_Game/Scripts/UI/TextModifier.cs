using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextModifier : MonoBehaviour { //TODO use this class

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
                currentSize+=3;
                text.fontSize = currentSize;
                if (currentSize >= maxFontSize)
                    scaleSizeUp = false;
            } else {
                currentSize-=3;
                text.fontSize = currentSize;
                if (currentSize <= minFontSize)
                    scaleSizeUp = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}