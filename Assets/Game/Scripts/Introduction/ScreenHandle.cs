using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHandle : MonoBehaviour
{

    private JsonControllerDetalhePlantas jsonControllerDetalhePlantas;
    [SerializeField] private GameObject gridDePlantas; //pai dos botoes plantas
    public GameObject DetalhePlantaPanel;
    public Text NomePlantaDetalhePanel;
    public GameObject panelMenosDeUmaPlantaSelecionada;

    [Tooltip("Prefab that is model to the button beign displayed at the selection of plants")]
    public GameObject ModelOfButtonToPlantSelection;

    void Start()
    {
        jsonControllerDetalhePlantas = JsonControllerDetalhePlantas.transformaJson(); //cria e inicializa jasoncontroller
        InstantiateButtons();//carrega os botoes de plantas no canvas
    }

    public void PlayButton(string scene)
    { //passa plantas selecionadas para prox scena
        InformacoesBotaoIntroductionScene[] plantsButtons = FindObjectsOfType<InformacoesBotaoIntroductionScene>();
        PlantsSingleton.Instance.SelectedPlants = new List<InformacoesBotaoIntroductionScene>();
        foreach (InformacoesBotaoIntroductionScene button in plantsButtons)
            if (button.estadoDoBotao)
                PlantsSingleton.Instance.SelectedPlants.Add(button);
        
        if (PlantsSingleton.Instance.SelectedPlants.Count < 1)
            panelMenosDeUmaPlantaSelecionada.SetActive(true);
        else
            gameObject.GetComponent<SceneLoader>().LoadScene(scene);
    }

    public void SelecionaDeselecionaBotaoPlanta()
    {
        string planta = NomePlantaDetalhePanel.GetComponent<Text>().text;
        foreach (Transform p in gridDePlantas.gameObject.transform)
        {
            if (planta == p.name)
            {
                if (p.GetComponent<InformacoesBotaoIntroductionScene>().estadoDoBotao != SelecionarPlanta.Instance.getEstado())
                {
                    p.GetComponent<InformacoesBotaoIntroductionScene>().clicarBotao();
                }
                SelecionarPlanta.Instance.setEstado(p.GetComponent<InformacoesBotaoIntroductionScene>().estadoDoBotao);
            }
        }
    }

    private void InstantiateButtons()
    {//metodo que adiciona botoes de plantas ao canvas
        GameObject button;
        foreach (Planta p in jsonControllerDetalhePlantas.plantas)
        {//para cada planta no json
            button = Instantiate(ModelOfButtonToPlantSelection) as GameObject; //instancia o botao
            button.transform.SetParent(gridDePlantas.transform, false); //coloca como pai o gridDePlantas
            button.name = p.nomePopular; //nome do botao é nome da planta 
            button.tag = "botaoDoCanvas"; //adiciona tag aos botoes

            button.GetComponent<InformacoesBotaoIntroductionScene>().NomePopular = p.nomePopular;
            button.GetComponent<InformacoesBotaoIntroductionScene>().NomeCientifico = p.nomeCientifico;
            button.GetComponent<InformacoesBotaoIntroductionScene>().Informacoes = p.informacoes;
            button.GetComponent<InformacoesBotaoIntroductionScene>().Imagem = p.imagem;

            Sprite sprite; //sprite usado para popular iamgens no canvas
            sprite = Resources.Load("Imagens/" + p.imagem, typeof(Sprite)) as Sprite;  //carrega a imagem de acordo com o nome que consta no json

            foreach (Transform child in button.transform)
            {//percorre os transforms do botaoPrefab
                if (child != button.transform)
                {//pega o filho do botao
                    Image image = child.GetComponent<Image>();//pega a imagem do filho do botao
                    image.overrideSprite = sprite; //seta a imagem no filho
                    break;
                }
            }
        }
    }

}
