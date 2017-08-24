using UnityEngine;
using UnityEngine.UI;

public class PlantsSingleton : MonoBehaviour {
    public GameObject gridDePlantas;
    private static PlantsSingleton instance = null;//a instancia comeca vazia
    public static GameObject[] botoesCanvas;
    JasonController jasonController;

    public static PlantsSingleton Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<PlantsSingleton>();

            return instance;
        }
    }

    void Awake() {

        jasonController = JasonController.transformaJson(); //cria e inicializa jasoncontroller
        
        DontDestroyOnLoad(gameObject);

        insereBotoes();//carrega os botoes de plantas no canvas
        botoesCanvas = GameObject.FindGameObjectsWithTag("botaoDoCanvas");//array com os botoes do canvas
    }

    void insereBotoes() //metodo que adiciona botoes de plantas ao canvas
    {
        GameObject botaoPrefab = null;//botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
        {
            botaoPrefab = (Resources.Load("Prefabs/ButtonPlants") as GameObject);
            Debug.Log("Achou o prefab");
        }

        foreach (Planta p in jasonController.plantas) //para cada planta no json
        {
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
}