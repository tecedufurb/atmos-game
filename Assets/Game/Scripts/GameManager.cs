using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject gridDePlantas;
    public static GameManager instance = null;//a instancia comeca vazia
    public static GameObject[] botoesCanvas;
    JsonControllerDetalhePlantas jsonControllerDetalhePlantas;
    JsonControllerPlantasErradas jsonControllerPlantasErradas;
    JsonControllerPlantasCorretas jsonControllerPlantasCorretas;
    public static bool podePlantar = true;

    #region UNITY_METHODS
    void Awake() {

        startJson();

        if (instance == null) //Check if instance already exists
        {
            instance = this;  //if not, set instance to this
        }
        else if (instance != this)  //If instance already exists and it's not this:
        {
            Destroy(gameObject); //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        }

        InstantiateButtons(); //carega botoes das plantas 
        botoesCanvas = GameObject.FindGameObjectsWithTag("botaoDoCanvas");//array com os botoes do canvas
    }
    void startJson() { //inicializa jsons
        jsonControllerDetalhePlantas = JsonControllerDetalhePlantas.transformaJson(); //cria e inicializa jasoncontroller
        jsonControllerPlantasCorretas = JsonControllerPlantasCorretas.transformaJson(); //cria e inicializa jasoncontroller
        jsonControllerPlantasErradas = JsonControllerPlantasErradas.transformaJson(); //cria e inicializa jasoncontroller
    }

    void criaListasJsons() {
        foreach (PlantaIncorreta planta in jsonControllerPlantasErradas.plantasIncorretas) {
            PontuacaoPlantas.nomesPlantasErradas.Add(planta.nomePlanta);
        }
    }

    private void Start() {
        podePlantar = true; //quando jogo comeca pode plantar
        criaListasJsons();
    }

    #endregion

    private void InstantiateButtons() {//metodo que adiciona botoes de plantas ao canvas
        GameObject botaoPrefab = null; //botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
            botaoPrefab = (Resources.Load("Prefabs/Button") as GameObject);

        foreach (var p in PlantsSingleton.Instance.SelectedPlants) {//para cada planta no json
            botaoPrefab = Instantiate(botaoPrefab) as GameObject; //instancia o botao
            botaoPrefab.transform.SetParent(gridDePlantas.transform, false); //coloca como pai o gridDePlantas
            botaoPrefab.name = p.NomePopular; //nome do botao é nome da planta 
            botaoPrefab.tag = "botaoDoCanvas"; //adiciona tag aos botoes

            botaoPrefab.GetComponent<InformacoesBotao>().NomePopular = p.NomePopular;
            botaoPrefab.GetComponent<InformacoesBotao>().NomeCientifico = p.NomeCientifico;
            botaoPrefab.GetComponent<InformacoesBotao>().Informacoes = p.Informacoes;
            botaoPrefab.GetComponent<InformacoesBotao>().Imagem = p.Imagem;

            Sprite sprite; //sprite usado para popular iamgens no canvas
            sprite = Resources.Load("Imagens/" + p.Imagem, typeof(Sprite)) as Sprite;  //carrega a imagem de acordo com o nome que consta no json

            var textoNomePlanta = botaoPrefab.transform.GetChild(1).GetChild(0).gameObject;
            //valida tamnha do nome da planta para caber no botao
            if (!(p.NomePopular.Length > 12)) { //se é menor que 12 chars
                textoNomePlanta.GetComponent<Text>().text = p.NomePopular;
            }
            else {
                textoNomePlanta.GetComponent<Text>().text = p.NomePopular.Substring(0, 13) + "..";
            }

            foreach (Transform child in botaoPrefab.transform) {//percorre os transforms do botaoPrefab
                if (child != botaoPrefab.transform) {//pega o filho do botao
                    Image image = child.GetComponent<Image>();//pega a imagem do filho do botao
                    image.overrideSprite = sprite; //seta a imagem no filho
                    break;
                }
            }
        }
    }

    public static bool IsPointerOverUIObject() { //metodo que verifica se colidiu com UI
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}