using UnityEngine;
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

        //insereBotoes();//carrega os botoes de plantas no canvas
        InstantiateButtons();
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

        foreach (PlantaCorreta planta in jsonControllerPlantasCorretas.plantasCorretas) {
            PontuacaoPlantas.nomePlantaE_Grupo.Add(planta.nomePlanta,planta.grupo);
        }
    }

    private void Start() {
        podePlantar = true; //quando jogo comeca pode plantar
        criaListasJsons();
    }

    void Update() {

    }
    #endregion

    void insereBotoes() //metodo que adiciona botoes de plantas ao canvas
    {
        GameObject botaoPrefab = null; //botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
        {
            botaoPrefab = (Resources.Load("Prefabs/Button") as GameObject);
        }

        foreach (Planta p in jsonControllerDetalhePlantas.plantas) //para cada planta no json
        {
            botaoPrefab = Instantiate(botaoPrefab) as GameObject; //instancia o botao
            botaoPrefab.transform.SetParent(gridDePlantas.transform, false); //coloca como pai o gridDePlantas
            botaoPrefab.name = p.nomePopular; //nome do botao é nome da planta 
            botaoPrefab.tag = "botaoDoCanvas"; //adiciona tag aos botoes

            InformacoesBotao informacoesBotao = botaoPrefab.GetComponent<InformacoesBotao>(); //pega o componente informocoes do botao e preenche
            informacoesBotao.NomePopular = p.nomePopular;
            informacoesBotao.NomeCientifico = p.nomeCientifico;
            informacoesBotao.Informacoes = p.informacoes;
            informacoesBotao.Imagem = p.imagem;

            Sprite sprite; //sprite usado para popular iamgens no canvas
            sprite = Resources.Load("Imagens/" + p.imagem, typeof(Sprite)) as Sprite;  //carrega a imagem de acordo com o nome que consta no json

            foreach (Transform child in botaoPrefab.transform)//percorre os transforms do botaoPrefab
            {
                if (child != botaoPrefab.transform) //pega o filho do botao
                {
                    Image image = child.GetComponent<Image>();//pega a imagem do filho do botao
                    image.overrideSprite = sprite; //seta a imagem no filho
                }
            }
        }
    }

    private void InstantiateButtons() {//metodo que adiciona botoes de plantas ao canvas
        GameObject botaoPrefab = null; //botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
            botaoPrefab = (Resources.Load("Prefabs/Button") as GameObject);

        foreach (ButtonInformations p in PlantsSingleton.Instance.SelectedPlants) {//para cada planta no json
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

            foreach (Transform child in botaoPrefab.transform) {//percorre os transforms do botaoPrefab
                if (child != botaoPrefab.transform) {//pega o filho do botao
                    Image image = child.GetComponent<Image>();//pega a imagem do filho do botao
                    image.overrideSprite = sprite; //seta a imagem no filho
                }
            }
        }
    }
}