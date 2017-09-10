using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPontuacao : MonoBehaviour {

    public GameObject objetoComImagem;
    private Image imagem;

    public const int qntTotalPlantas = 200;
    private static int qntAtualPlantas = 0;  //qnt que o controller esta exibindo atualmente
    private static bool qntPlantasChanged = false; //armazena se precisa atualizar a imagem

    private void Start() {
        imagem = objetoComImagem.GetComponentInChildren<Image>();//pega a imagem para ser atualizada
    }

    public static void incrementaQntPlantas() {
        if (qntAtualPlantas >= qntTotalPlantas) {//se qnt > total
            qntAtualPlantas = qntTotalPlantas; //se qntAtual = maximoPlantas
            qntPlantasChanged = true;
            PontuacaoPlantas.reachedMaxQuantity();
        }
        else {
           // Debug.Log("ELSE");
            qntAtualPlantas++;
            qntPlantasChanged = true;
        }
    }

    void Update() {
        if (qntPlantasChanged) {
            Debug.Log(qntAtualPlantas);
            updateImage();
            qntPlantasChanged = false;
        }
    }

    private void updateImage() {
        double porcentagem = (double)qntAtualPlantas / qntTotalPlantas * 100 / 100;
        imagem.fillAmount = (float)porcentagem;
    }

    private void OnDestroy() {
        qntAtualPlantas = 0;
    }

}
