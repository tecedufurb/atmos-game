using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecionarPlanta : MonoBehaviour {

    private static SelecionarPlanta instance;
    private bool estado = false;
    public GameObject gameObjectImagem;
    public GameObject botao;
    public Text texto;


    public static SelecionarPlanta Instance {
        get {
            return instance;
        }

        set {
            instance = value;
        }
    }

    void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void setEstado(bool estado) {
        this.estado = estado;
    }

    public bool getEstado() {
        return estado;
    }

    public void clickBotao() {
        if (estado) {
            estado = false;
            texto.GetComponent<Text>().text = "Selecionar";
            botao.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            gameObjectImagem.active = false;
        }
        else {
            estado = true;
            texto.GetComponent<Text>().text = "Selecionado!!";
            botao.GetComponent<Image>().color = Color.yellow;
            gameObjectImagem.active = true;
        }
    }
}
