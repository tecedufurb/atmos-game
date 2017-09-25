using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextColorChanger : MonoBehaviour {

    public GameObject textObject;
    private Text texto;
    private byte r;
    private byte g;
    private byte b;
    private bool fluxoParaEsquerda = true;//var aux
    public enum Opcao { TextMenu, TextSuaPontuacao, TextParabens };
    public Opcao opcao;
    void Start() {
        texto = textObject.GetComponent<Text>();
        r = ((Color32)texto.color).r;
        g = ((Color32)texto.color).g;
        b = ((Color32)texto.color).b;
    }

    void changeColorMainMenu() {
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

    void changeColorMuitoBem() {
        if (g > 50 && fluxoParaEsquerda) {
            g--;
        }
        else {
            fluxoParaEsquerda = false;
        }
        if (g < 200 && !fluxoParaEsquerda) {
            g++;
        }
        else {
            fluxoParaEsquerda = true;
        }
    }
    void changeColorSuaPontuacao() {
        if (g > 100 && fluxoParaEsquerda) {
            g--;
        }
        else {
            fluxoParaEsquerda = false;
        }
        if (g < 230 && !fluxoParaEsquerda) {
            g++;
        }
        else {
            fluxoParaEsquerda = true;
        }
    }

    void Update() {
        if (Opcao.TextMenu.ToString() == opcao.ToString()) {
            changeColorMainMenu();
        }
        else if (Opcao.TextParabens.ToString() == opcao.ToString()) {
            changeColorMuitoBem();
        }
        else if (Opcao.TextSuaPontuacao.ToString() == opcao.ToString()) {
            changeColorSuaPontuacao();
        }
        texto.color = new Color32(r, g, b, 255);
    }
}
