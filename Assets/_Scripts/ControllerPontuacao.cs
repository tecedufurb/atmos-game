using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPontuacao : MonoBehaviour {

    public GameObject objetoComImagem;
    private Image imagem;

    public static int qntTotalPlantas = PontuacaoPlantas.qntTotalPlantasReal;
    public static int qntAtualPlantas = 0;  //qnt que o controller esta exibindo atualmente
    private static bool qntPlantasChanged = false; //armazena se precisa atualizar a imagem

    private void Start() {
        imagem = objetoComImagem.GetComponentInChildren<Image>();//pega a imagem para ser atualizada
    }

    public static bool isQntAtualPlantasGreaterOrEqualQntTotalPlantas() {
        if (qntAtualPlantas >= qntTotalPlantas) {
            return true;
        }
        return false;
    }

    public static void incrementaQntPlantas() {
        if (qntAtualPlantas >= qntTotalPlantas) {
            qntAtualPlantas = qntTotalPlantas; 
            qntPlantasChanged = true;
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

    private void OnDestroy() {
        qntAtualPlantas = 0;
        qntPlantasChanged = false;
    }
}
