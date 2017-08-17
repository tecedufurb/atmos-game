using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject gridDePlantas;//isso estava declarado como variavel global
    public static GameManager instance = null;//a instancia comeca vazia
    GameObject prefabProblema; //armazena o prefab com a mensagem do problema
    public static GameObject[] botoesCanvas; //contem
    JasonController jasonController;

    #region UNITY_METHODS
    void Awake() {

        jasonController = JasonController.transformaJson(); //cria e inicializa jasoncontroller

        if (instance == null) //Check if instance already exists
        {
            instance = this;  //if not, set instance to this
        } else if (instance != this)  //If instance already exists and it's not this:
          {
            Destroy(gameObject); //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        insereBotoes();//carrega os botoes de plantas no canvas
        botoesCanvas = GameObject.FindGameObjectsWithTag("botaoDoCanvas");//array com os botoes do canvas
    }

    private void Start() {

    }

    void Update() {

    }
    #endregion


    void exibeProblema(int fase) //exibe o probleme a ser resolvido na fase
    {
        if (fase == 1) {
            prefabProblema = Resources.Load("Prefabs/Problema1") as GameObject;
            prefabProblema = Instantiate(prefabProblema) as GameObject;
            prefabProblema.transform.SetParent(FindObjectOfType<Canvas>().transform, false);//pega o canvas tela. TESTAR
        }
    }

    //esse plantar nao esta sendo utilizado
    void plantar() {
        Vector3 raycastVector; //isso estava declarado como variavel global
        GameObject plantaPrefab;//planta que ira ser instanciada, isso estava declarado como variavel global
        if (Input.GetButtonDown("Fire1")) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Ray ray2 = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x+10, Input.mousePosition.y, Input.mousePosition.z+10));

            RaycastHit hit = new RaycastHit();
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);
            //como mostro o raio do hit na tela?
            bool terreno = false, agua = false, app = false, outraArvore = false, botaoMovimentacao = false, canvasDePlantas = false;




            foreach (RaycastHit ht in hits) {
                if ((ht.transform.tag == "areaApp")) {
                    app = true;
                } else if ((ht.transform.tag == "terrain")) {
                    terreno = true;
                    hit = ht;
                } else if ((ht.transform.tag == "agua")) {
                    agua = true;
                } else if (ht.transform.tag == "ColisaoComBotaoMovimentacao") {
                    botaoMovimentacao = true;
                } else if (ht.transform.tag == "ColisaoComCanvasDePlantas") {
                    canvasDePlantas = true;
                } else {
                    outraArvore = true;
                }

                Debug.Log("hit:" + ht.collider);

            }


            if (terreno && app && !agua && !outraArvore && !botaoMovimentacao && !canvasDePlantas) {
                raycastVector = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                //importar a planta usada de acordo com o canvas
                string[] nomePlantas = new string[botoesCanvas.Length]; //armazena o nome das plantas que devem ser instanciadas
                InformacoesBotao informacao; //armazena o componente informacao botao do canvas

                for (int i = 0; i < botoesCanvas.Length; i++)  //seleciona o nome das plantas que estao selecionadas
                {
                    informacao = botoesCanvas[i].GetComponent<InformacoesBotao>();
                    //  if (botoesCanvas.estadoDoBotao)
                    //  {
                    //     nomePlantas[i] = informacao.NomePopular;
                    //  }
                }

                foreach (string nome in nomePlantas)//INSTANCIA A PLANTA NO MUNDO
                {//METODO QUE CRIA DISTANCIA ENTRE AS PLANTAS




                    plantaPrefab = Resources.Load("Prefabs/" + nome) as GameObject;
                    if (plantaPrefab != null) {
                        plantaPrefab = Instantiate(plantaPrefab, raycastVector, Quaternion.identity) as GameObject; //instancia o  
                    }
                }
            }
        }
    }


    #region metodos que nao precisao ser mexidos
    void insereBotoes() //metodo que adiciona botoes de plantas ao canvas
    {
        GameObject botaoPrefab = null; ;//botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
        {
            botaoPrefab = (Resources.Load("Prefabs/Button") as GameObject);
            Debug.Log("botaoPrefab carregado" + botaoPrefab);
        }

        foreach (Planta p in jasonController.plantas) //para cada planta no json
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
            //aqui adicionar detalhe planta


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
    #endregion
}
