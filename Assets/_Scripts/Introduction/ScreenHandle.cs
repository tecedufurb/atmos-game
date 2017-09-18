using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenHandle : MonoBehaviour {

    private JsonControllerDetalhePlantas jsonControllerDetalhePlantas;
    [SerializeField] private GameObject gridDePlantas; //pai dos botoes plantas
    public GameObject DetalhePlantaPanel;
    public Text NomePlantaDetalhePanel;

    void Start() {
        jsonControllerDetalhePlantas = JsonControllerDetalhePlantas.transformaJson(); //cria e inicializa jasoncontroller
        InstantiateButtons();//carrega os botoes de plantas no canvas
    }

    public void PlayButton(string scene) { //passa plantas selecionadas para prox scena
        InformacoesBotaoIntroductionScene[] plantsButtons = FindObjectsOfType<InformacoesBotaoIntroductionScene>();
        PlantsSingleton.Instance.SelectedPlants = new List<InformacoesBotaoIntroductionScene>();
        foreach (InformacoesBotaoIntroductionScene button in plantsButtons) {
            if (button.estadoDoBotao)
                PlantsSingleton.Instance.SelectedPlants.Add(button);
        }
    }

    public void SelecionaDeselecionaBotaoPlanta() {
        string planta = NomePlantaDetalhePanel.GetComponent<Text>().text;
        foreach (Transform p in gridDePlantas.gameObject.transform) {
            if (planta == p.name) {
                if(p.GetComponent<InformacoesBotaoIntroductionScene>().estadoDoBotao != SelecionarPlanta.Instance.getEstado()) {
                    p.GetComponent<InformacoesBotaoIntroductionScene>().clicarBotao();
                }
                SelecionarPlanta.Instance.setEstado(p.GetComponent<InformacoesBotaoIntroductionScene>().estadoDoBotao);
            }
        }
    }


    /*private void insereBotoes() {//metodo que adiciona botoes de plantas ao canvas
        GameObject botaoPrefab = null;//botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
            botaoPrefab = (Resources.Load("Prefabs/ButtonPrefabIntroScene") as GameObject);

        foreach (Planta p in mJsonController.plantas) {//para cada planta no json
            botaoPrefab = Instantiate(botaoPrefab) as GameObject; //instancia o botao
            botaoPrefab.transform.SetParent(gridDePlantas.transform, false); //coloca como pai o gridDePlantas
            botaoPrefab.name = p.nomePopular; //nome do botao é nome da planta 
            botaoPrefab.tag = "botaoDoCanvas"; //adiciona tag aos botoes

            ButtonInformations informacoesBotao = botaoPrefab.GetComponent<ButtonInformations>(); //pega o componente informocoes do botao e preenche
            informacoesBotao.NomePopular = p.nomePopular;
            informacoesBotao.NomeCientifico = p.nomeCientifico;
            informacoesBotao.Informacoes = p.informacoes;
            informacoesBotao.Imagem = p.imagem;

            Sprite sprite; //sprite usado para popular iamgens no canvas
            sprite = Resources.Load("Imagens/" + p.imagem, typeof(Sprite)) as Sprite;  //carrega a imagem de acordo com o nome que consta no json

            foreach (Transform child in botaoPrefab.transform) {//percorre os transforms do botaoPrefab
                if (child.name == "Image") {//pega o filho do botao
                    Image image = child.GetComponent<Image>();//pega a imagem do filho do botao
                    image.overrideSprite = sprite; //seta a imagem no filho
                }
            }
        }
    }*/
    private void InstantiateButtons() {//metodo que adiciona botoes de plantas ao canvas
        GameObject botaoPrefab = null; //botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
            botaoPrefab = (Resources.Load("Prefabs/ButtonPrefabIntroScene") as GameObject);

        foreach (Planta p in jsonControllerDetalhePlantas.plantas) {//para cada planta no json
            botaoPrefab = Instantiate(botaoPrefab) as GameObject; //instancia o botao
            botaoPrefab.transform.SetParent(gridDePlantas.transform, false); //coloca como pai o gridDePlantas
            botaoPrefab.name = p.nomePopular; //nome do botao é nome da planta 
            botaoPrefab.tag = "botaoDoCanvas"; //adiciona tag aos botoes

            botaoPrefab.GetComponent<InformacoesBotaoIntroductionScene>().NomePopular = p.nomePopular;
            botaoPrefab.GetComponent<InformacoesBotaoIntroductionScene>().NomeCientifico = p.nomeCientifico;
            botaoPrefab.GetComponent<InformacoesBotaoIntroductionScene>().Informacoes = p.informacoes;
            botaoPrefab.GetComponent<InformacoesBotaoIntroductionScene>().Imagem = p.imagem;

            Sprite sprite; //sprite usado para popular iamgens no canvas
            sprite = Resources.Load("Imagens/" + p.imagem, typeof(Sprite)) as Sprite;  //carrega a imagem de acordo com o nome que consta no json

            foreach (Transform child in botaoPrefab.transform) {//percorre os transforms do botaoPrefab
                if (child != botaoPrefab.transform) {//pega o filho do botao
                    Image image = child.GetComponent<Image>();//pega a imagem do filho do botao
                    image.overrideSprite = sprite; //seta a imagem no filho
                    break;
                }
            }
        }
    }

}
