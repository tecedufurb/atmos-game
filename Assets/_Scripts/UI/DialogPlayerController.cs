﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPlayerController : MonoBehaviour {

    public GameObject panelMsgErroRemoverPlanta;
    public GameObject panelEstatisticas;
    public GameObject pontuacao;
    public GameObject plantasCorretas;
    public GameObject plantasIncorretas;
    public GameObject botaoVoltarAoMenu;
    public static DialogPlayerController instance = null;//a instancia comeca vazia

    void Awake() {

        if (instance == null) //Check if instance already exists
        {
            instance = this;  //if not, set instance to this
        }
        else if (instance != this)  //If instance already exists and it's not this:
        {
            Destroy(gameObject); //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        }
    }

    public void setPontuacao(int newQntPontuacao) {
        pontuacao.GetComponent<Text>().text = newQntPontuacao + " PTS";
    }

    public void setPlantasCorretas(int qntPlantasCorretas) {
        plantasCorretas.GetComponent<Text>().text = qntPlantasCorretas + " plantas";
    }

    public void setPlantasIncorretas(int qntPlantasIncorretas) {
        plantasIncorretas.GetComponent<Text>().text = qntPlantasIncorretas + " plantas";
    }

    public void showEstatisticas() {
        panelEstatisticas.active = true;
    }

    public void acaoVoltarAoMenu() {
        panelEstatisticas.active = false;
    }

    public void exibeErroAoRemoverPlanta(int codigoErro) {
        if (codigoErro == 0) {
            StartCoroutine(printaMsgErroNaTela(panelMsgErroRemoverPlanta.transform.GetChild(0).gameObject));
        }
        else if (codigoErro == 1) {
            StartCoroutine(printaMsgErroNaTela(panelMsgErroRemoverPlanta.transform.GetChild(1).gameObject));
        }
        else {
            Debug.LogError("CODIGO ERRO NAO ENCONTRADO:" + codigoErro);
        }
    }

  
    IEnumerator printaMsgErroNaTela(GameObject msgErro) {
        var t = msgErro.GetComponent<RectTransform>();
        //for () {

        //}
        yield return null;
    }
}
