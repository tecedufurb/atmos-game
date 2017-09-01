using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPontuacao : MonoBehaviour {

    public GameObject objetoComImagem;
    private Image imagem;

    public const int qntTotalPlantas = 100;
    private static int qntAtualPlantas;
    private static bool qntPlantasChanged; //armazena se precisa atualizar a imagem

    private void Start() {
        imagem = objetoComImagem.GetComponentInChildren<Image>();//pega a imagem para ser atualizada
        qntAtualPlantas = 0; //quando começa tem 0 plantas
    }

    public static void incrementaQntPlantas() {
        if (qntAtualPlantas >= qntTotalPlantas) {//se qnt > total
            qntAtualPlantas = qntTotalPlantas; //se qntAtual = maximoPlantas
            qntPlantasChanged = true;
            PontuacaoPlantas.reachedMaxQuantity();
        }
        else {
            qntAtualPlantas++;
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
        double porcentagem = (double)qntAtualPlantas / qntTotalPlantas * 100 / 100;
        imagem.fillAmount = (float)porcentagem;
    }

}
