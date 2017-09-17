using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChanger : MonoBehaviour {

    public GameObject textObject;
    private Text texto;
    private byte r;
    private byte g;
    private byte b;
    private bool fluxoParaEsquerda = true;//var aux

    void Start() {
        texto = textObject.GetComponent<Text>();
        r = ((Color32)texto.color).r;
        g = ((Color32)texto.color).g;
        b = ((Color32)texto.color).b;
    }

    void updateGreenValue() {
        if (g >= 33 && fluxoParaEsquerda) {
            g--;
        }
        else {
            fluxoParaEsquerda = false;
        }
        if (g <= 150 && !fluxoParaEsquerda) {
            g++;
        }
        else {
            fluxoParaEsquerda = true;
        }
    }
    void Update() {
        updateGreenValue();
        texto.color = new Color32(r, g, b, 255);
    }
}
