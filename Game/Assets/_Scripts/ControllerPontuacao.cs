﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPontuacao : MonoBehaviour {

    public GameObject objetoComImagem;
    private Image imagem;

    public const int qntTotalPlantas = 200;
    public int qntAtualPlantas;
    private bool qntPlantasChanged; //armazena se precisa atualizar a imagem

    private void Start() {
        imagem = objetoComImagem.GetComponentInChildren<Image>();//pega a imagem para ser atualizada
    }

    public void setQntAtualPlantas(int qnt) { 
        if (qnt >= 0 && qnt <= qntTotalPlantas) { //so qnt >0 e menor que qntTotal
            qntAtualPlantas = qnt;
            qntPlantasChanged = true;
        }
    }

    void Update() {
        if (qntPlantasChanged) {
            updateImage();
            qntPlantasChanged = false;
        }
    }

    private void updateImage() {
        double porcentagem = (double)qntAtualPlantas / qntTotalPlantas * 100 / 100; /// 100;//works
        imagem.fillAmount = (float)porcentagem;
    }

}
