using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverPlanta : MonoBehaviour {

    public GameObject cubo;
    public GameObject personagem;
    public GameObject botaoSimENao;
    public GameObject botaoRemover;
    public static RemoverPlanta instance = null;
    private static bool expandeCuboBool = false;
    private GameObject planta;

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

    private void FixedUpdate() {
        expandeCubo();
    }

    public void clickBotaoPrepararRemoverPlanta() {
        expandeCuboBool = true;
    }

    public void recebePlanta(GameObject planta,bool isPlanta) {
        if (isPlanta) {
            instance.planta = planta;
            instance.personagem.GetComponent<SimpleCharacterControl>().canMove(false);
            exibeSimENao(true);
        }
        else {
            exibeErro(0);
        }
    }

    private void exibeSimENao(bool exibirSimENao) {
        if (exibirSimENao) { //exibe sim e nao e esconde botao remover
            botaoRemover.active = false;
            botaoSimENao.active = true;
        }
        else { //exibe botao remover e esconde sim e nao
            botaoRemover.active = true;
            botaoSimENao.active = false;
        }
    }

    private void exibeErro(int codigoErro) {
        this.GetComponent<DialogPlayerController>().exibeErroAoRemoverPlanta(codigoErro);        
        instance.personagem.GetComponent<SimpleCharacterControl>().canMove(true);
    }

    public void sim() {
        Destroy(instance.planta);
        instance.personagem.GetComponent<SimpleCharacterControl>().canMove(true);
        exibeSimENao(false);
    }

    public void nao() {
        instance.personagem.GetComponent<SimpleCharacterControl>().canMove(true);
        exibeSimENao(false);
    }


    public void resetCubo() {
        expandeCuboBool = false;
        cubo.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
        cubo.GetComponent<Transform>().position= cubo.GetComponentInParent<Transform>().position;
    }

    private void expandeCubo() {
        if (expandeCuboBool) {
            float x = cubo.GetComponent<BoxCollider>().center.x;
            float y = cubo.GetComponent<BoxCollider>().center.y;
            float z = cubo.GetComponent<BoxCollider>().center.z;
            float limite = 20.0f;
            if (z > limite) {
                resetCubo();
                exibeErro(1);
                return;
            }
            cubo.GetComponent<BoxCollider>().center = new Vector3(x, y, z + 4);
        }
    }
}