using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPontuacao : MonoBehaviour {

    public GameObject objetoComImagem;
    public GameObject gameObjectAnimationScoreModel;
    private Image imagem;

    public static int qntTotalPlantas = PontuacaoPlantas.qntTotalPlantasReal;
    public static int qntAtualPlantas = 0;  //qnt que o controller esta exibindo atualmente
    private static bool qntPlantasChanged = false; //armazena se precisa atualizar a imagem
    private static List<GameObject> listOfGameObjectAnimations = new List<GameObject>();

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
            addNewAnimation();
            updateImage();
            qntPlantasChanged = false;
        }
        //Update das animacoes
        for (int i = 0; i < listOfGameObjectAnimations.Count; i++) {
            if (listOfGameObjectAnimations[i].GetComponent<Text>().color.a < 0.30 || listOfGameObjectAnimations[i].GetComponent<RectTransform>().localPosition.y > 100) {
                Destroy(listOfGameObjectAnimations[i]);
                listOfGameObjectAnimations.RemoveAt(i);
            }
            else {
                listOfGameObjectAnimations[i].GetComponent<Text>().color = new Color(0.1960784f, 1, 0.1960784f, listOfGameObjectAnimations[i].GetComponent<Text>().color.a-0.01f);
                listOfGameObjectAnimations[i].GetComponent<RectTransform>().localPosition = new Vector3(43.0f, listOfGameObjectAnimations[i].GetComponent<RectTransform>().localPosition.y + 2.5f, 0.0f);
            }
        }
    }

    private void updateImage() {
        double porcentagem = (double)qntAtualPlantas / qntTotalPlantas * 100 / 100;
        imagem.fillAmount = (float)porcentagem;
    }

    private void addNewAnimation() {
        var a = Instantiate(gameObjectAnimationScoreModel, gameObjectAnimationScoreModel.GetComponentInParent<Transform>(), true);
        a.active = true;
        listOfGameObjectAnimations.Add(a);
    }

    private void OnDestroy() {
        qntAtualPlantas = 0;
        qntPlantasChanged = false;
    }
}
