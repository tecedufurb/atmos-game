using System.Collections.Generic;
using UnityEngine;

public class PontuacaoPlantas : MonoBehaviour {

    private static int valorPontPlantaErrada = -10;
    private static int valorPontPlantaCorreta = 10;
    public static int qntTotalPlantas = 200;
    private static int qntForaAreaAppAceitavel = 5;
    public static int qntTotalPlantasReal = qntTotalPlantas - qntForaAreaAppAceitavel;
    public static int qntPlantaPorGrupo = qntTotalPlantasReal / BoxesTerrainGenerator.qntBoxes / 2;

    private static int pontuacaoAtual;
    private static int qntPlantasCorretas;
    private static int qntPlantasIncorretas;

    public static List<string> nomesPlantasErradas = new List<string>();
    public static Dictionary<string, string> nomePlantaE_Grupo = new Dictionary<string, string>();
    private static Dictionary<string, int> boxsQntPlantas = new Dictionary<string, int>() { { "Box1", 0 }, { "Box2", 0 }, { "Box3", 0 }, { "Box4", 0 }, { "Box5", 0 }, { "Box6", 0 } };

    private static List<Container> box1;//lado esquerdo do rio
    private static List<Container> box2;//lado direito do rio

    public static void incrementaBox(Vector3 coord, string nomePlanta, int areaApp) {
        int cont = 0;
        if (areaApp == 1) {
            foreach (Container i in box1) {
                if ((coord.x >= i.start) && (coord.x <= i.end)) {
                    if (!i.isFull) { //se nao esta cheio
                        box1[cont] = new Container(box1[cont].isFull, box1[cont].start, box1[cont].end, box1[cont].qnt + 1);
                        Debug.Log("1 box: " + cont + " val: " + i.qnt);
                        ControllerPontuacao.incrementaQntPlantas(); //Atualiza termometro 
                        atualizaQuantidadePlantasPontuacao(nomePlanta); //atualiza estatisticas
                        if (i.qnt >= qntPlantaPorGrupo) {
                            Debug.Log("full box " + cont);
                            box1[cont] = new Container(true, box1[cont].start, box1[cont].end, box1[cont].qnt);
                            if (ControllerPontuacao.isQntAtualPlantasGreaterOrEqualQntTotalPlantas()) {
                                reachedMaxQuantity();
                            }
                        }
                    }
                    break;
                }
                cont++;
            }
        }
        else {
            foreach (Container i in box2) {
                if ((coord.x >= i.start) && (coord.x <= i.end)) {
                    if (!i.isFull) { //se nao esta cheio
                        box2[cont] = new Container(box2[cont].isFull, box2[cont].start, box2[cont].end, box2[cont].qnt + 1);
                        Debug.Log("2 box: " + cont + " val: " + i.qnt);
                        ControllerPontuacao.incrementaQntPlantas(); //Atualiza termometro 
                        atualizaQuantidadePlantasPontuacao(nomePlanta); //atualiza estatisticas
                        if (i.qnt >= qntPlantaPorGrupo) {
                            Debug.Log("full box " + cont);
                            box2[cont] = new Container(true, box2[cont].start, box2[cont].end, box2[cont].qnt);
                            if (ControllerPontuacao.isQntAtualPlantasGreaterOrEqualQntTotalPlantas()) {
                                reachedMaxQuantity();
                            }
                        }
                    }
                    break;
                }
                cont++;
            }
        }
    }


    void Start() {
        box1 = BoxesTerrainGenerator.instance.generateListBoxes();
        box2 = new List<Container>(box1);
        string s = "1 - ";
        foreach (Container item in box1) {
            s = s + " full " + item.isFull + " start: " + item.start + " end: " + item.end + " qnt " + item.qnt;
        }
       // Debug.Log(s);

        string d = "2 - ";
        foreach (Container item in box1) {
            d = d + " full " + item.isFull + " start: " + item.start + " end: " + item.end + " qnt " + item.qnt;
        }
       // Debug.Log(d);
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
            pontuacaoAtual = ((qntPlantasCorretas * 10)) / 7;
        }
        else {
            if ((((qntPlantasCorretas * 10) - (qntPlantasIncorretas * 2)) / 67 > 0))
                pontuacaoAtual = ((qntPlantasCorretas * 10) - (qntPlantasIncorretas * 2)) / 7;
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
        box1 = null;
        box2 = null;
    }
}
