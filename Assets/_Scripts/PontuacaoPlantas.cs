using System.Collections.Generic;
using UnityEngine;

public class PontuacaoPlantas : MonoBehaviour {

    private static int pontuacaoAtual;
    private static int qntPlantasCorretas;//qualquer 1 dos 3 grupos
    private static int qntPlantasIncorretas;//todos as plantas erradas
    public static List<string> nomesPlantasErradas = new List<string>();
    public static Dictionary<string, string> nomePlantaE_Grupo = new Dictionary<string, string>();

    private static int valorPontPlantaErrada = -10;
    private static int valorPontPlantaCorreta = 10;
    private static int qntTotalPlantas = ControllerPontuacao.qntTotalPlantas;

    public static void reachedMaxQuantity() {
        GameManager.podePlantar = false;

        SetValoresPanelEstatisticas();
        DialogPlayerController.instance.showEstatisticas();
    }

    private static void SetValoresPanelEstatisticas() {
        generateValuePontuacao();
        DialogPlayerController.instance.setPontuacao(pontuacaoAtual);
        DialogPlayerController.instance.setPlantasCorretas(qntPlantasCorretas);
        DialogPlayerController.instance.setPlantasIncorretas(qntPlantasIncorretas);
    }

    private static void generateValuePontuacao() {
        if (qntPlantasIncorretas == 0) {
            pontuacaoAtual = ((qntPlantasCorretas / 1) * qntTotalPlantas)/10;
        }
        else {
            pontuacaoAtual = ((qntPlantasCorretas / qntPlantasIncorretas) * qntTotalPlantas);
        }
    }

    public static int setValorPlanta(string nomePlanta) {
        foreach (var plantaErrada in nomesPlantasErradas) {
            if (plantaErrada == nomePlanta) {
                return valorPontPlantaErrada;
            }
        }
        return valorPontPlantaCorreta;
    }

    public static void atualizaQuantidadePlantasPontuacao(string nomePlanta) {
        foreach (var plantaErrada in nomesPlantasErradas) {
            if (plantaErrada == nomePlanta) {
                qntPlantasIncorretas++;
                return;
            }
        }
        qntPlantasCorretas++;
    }

    void OnDestroy() {
        pontuacaoAtual = 0;
        qntPlantasIncorretas = 0;
        qntPlantasCorretas = 0;
        nomesPlantasErradas.Clear();
        nomePlantaE_Grupo.Clear();
    }
}
