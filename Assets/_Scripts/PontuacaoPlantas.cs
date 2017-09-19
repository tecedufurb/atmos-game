using System.Collections.Generic;
using UnityEngine;

public class PontuacaoPlantas : MonoBehaviour {

    private static int valorPontPlantaErrada = -10;
    private static int valorPontPlantaCorreta = 10;
    public static int qntTotalPlantas = 300;
    private static int qntForaAreaAppAceitavel = 30;
    public static int qntTotalPlantasReal = qntTotalPlantas - qntForaAreaAppAceitavel;
    public static int qntPlantaPorGrupo = qntTotalPlantasReal / 6 + 1;

    private static int pontuacaoAtual;
    private static int qntPlantasCorretas;
    private static int qntPlantasIncorretas;

    public static List<string> nomesPlantasErradas = new List<string>();
    public static Dictionary<string, string> nomePlantaE_Grupo = new Dictionary<string, string>();
    private static Dictionary<string, int> boxsQntPlantas = new Dictionary<string, int>() { { "Box1", 0 }, { "Box2", 0 }, { "Box3", 0 }, { "Box4", 0 }, { "Box5", 0 }, { "Box6", 0 } };
    private static Dictionary<string, bool> boxsIsFull = new Dictionary<string, bool>() { { "Box1", false }, { "Box2", false }, { "Box3", false }, { "Box4", false }, { "Box5", false }, { "Box6", false } };

    private static bool isAllBoxesFull() {
        foreach (var i in boxsIsFull.Values) {
            if (i == false) {
                return false;
            }
        }
        return true;
    }

    public static void incrementaBox(string box, string nomePlanta) {
        if (!boxsIsFull[box]) { //se ainda nao chegou no maximo
            boxsQntPlantas[box] = boxsQntPlantas[box] + 1;
            ControllerPontuacao.incrementaQntPlantas(); //Atualiza termometro 
            atualizaQuantidadePlantasPontuacao(nomePlanta); //atualiza estatisticas
            if (boxsQntPlantas[box] >= qntPlantaPorGrupo) {
                boxsIsFull[box] = true;
                if (ControllerPontuacao.isQntAtualPlantasGreaterOrEqualQntTotalPlantas()) {
                    reachedMaxQuantity();
                }
            }
        }
    }

    public static string colidiuComBox(string tagColisao) {
        if (boxsQntPlantas.ContainsKey(tagColisao)) {
            return tagColisao;
        }
        return null;
    }

    public static void reachedMaxQuantity() {
        GameManager.podePlantar = false;
        SetValoresPanelEstatisticas();
        DialogPlayerController.instance.showEstatisticas();
        resetaValores();
    }

    private static void SetValoresPanelEstatisticas() {
        generateValuePontuacao();
        DialogPlayerController.instance.setPontuacao(pontuacaoAtual);
        DialogPlayerController.instance.setPlantasCorretas(qntPlantasCorretas);
        DialogPlayerController.instance.setPlantasIncorretas(qntPlantasIncorretas);
    }

    private static void generateValuePontuacao() {
        if (qntPlantasIncorretas == 0) {
            pontuacaoAtual = ((qntPlantasCorretas * 10)) / 6;
        }
        else {
            if ((((qntPlantasCorretas * 10) - (qntPlantasIncorretas * 2)) / 6) > 0)
                pontuacaoAtual = ((qntPlantasCorretas * 10) - (qntPlantasIncorretas * 2)) / 6;
            else
                pontuacaoAtual = 0;
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

    private static void resetaValores() {
        pontuacaoAtual = 0;
        qntPlantasIncorretas = 0;
        qntPlantasCorretas = 0;
        nomesPlantasErradas = new List<string>();
        nomePlantaE_Grupo = new Dictionary<string, string>();
        boxsIsFull = new Dictionary<string, bool>() { { "Box1", false }, { "Box2", false }, { "Box3", false }, { "Box4", false }, { "Box5", false }, { "Box6", false } };
        boxsQntPlantas = new Dictionary<string, int>() { { "Box1", 0 }, { "Box2", 0 }, { "Box3", 0 }, { "Box4", 0 }, { "Box5", 0 }, { "Box6", 0 } };
    }
}
