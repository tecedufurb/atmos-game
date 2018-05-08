using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPlayerController : MonoBehaviour {

    public GameObject gameObjectComMensagensDeErro;
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
        GameObject msgCodigoErro;
        GameObject cloneMensagemDeErro;
        if (codigoErro == 0) {
            msgCodigoErro = gameObjectComMensagensDeErro.transform.GetChild(0).gameObject;
            cloneMensagemDeErro = Instantiate(msgCodigoErro, msgCodigoErro.GetComponentInParent<Transform>(), true);
            StartCoroutine(printaMsgErroNaTela(cloneMensagemDeErro.GetComponent<Transform>()));
        }
        else if (codigoErro == 1) {
            msgCodigoErro = gameObjectComMensagensDeErro.transform.GetChild(1).gameObject;
            cloneMensagemDeErro = Instantiate(msgCodigoErro, msgCodigoErro.GetComponentInParent<Transform>(), true);
            StartCoroutine(printaMsgErroNaTela(cloneMensagemDeErro.GetComponent<Transform>()));
        }
        else {
            Debug.LogError("Erro ao processar codigo do erro ao remover planta:" + codigoErro);
        }
    }

    IEnumerator printaMsgErroNaTela(Transform msgErroTransform) {
        msgErroTransform.gameObject.active = true;
        while (msgErroTransform.position.y > 46.0) {
            msgErroTransform.position = new Vector3(msgErroTransform.position.x, msgErroTransform.position.y - 0.01f, msgErroTransform.position.z);
            yield return null;
        }
        Destroy(msgErroTransform.gameObject);
    }
}


