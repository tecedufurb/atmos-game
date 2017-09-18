using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InformacoesBotaoIntroductionScene : MonoBehaviour {
    private string nomePopular;
    private string nomeCientifico;
    private string imagem;
    private string informacoes;
    private GameObject panelDetalhePlanta;
    private GameObject sceneCanvas;
    public bool estadoDoBotao = false;

    public string NomePopular {
        get {
            return nomePopular;
        }

        set {
            nomePopular = value;
        }
    }

    public string NomeCientifico {
        get {
            return nomeCientifico;
        }

        set {
            nomeCientifico = value;
        }
    }

    public string Imagem {
        get {
            return imagem;
        }

        set {
            imagem = value;
        }
    }

    public string Informacoes {
        get {
            return informacoes;
        }

        set {
            informacoes = value;
        }
    }

    public void clicarBotao() {
        if (estadoDoBotao) {
            gameObject.GetComponent<Button>().image.color = new Color(255, 255, 255);
            if (!(SceneManager.GetActiveScene().name == "Introduction")) {
                var botaoNomePlanta = gameObject.transform.GetChild(1).gameObject;
                botaoNomePlanta.GetComponent<Button>().image.color = new Color(255, 255, 255);
            }
            estadoDoBotao = false; //seta o botao como desativo/falso
        }
        else //seta a cor botao selecionado
        {
            gameObject.GetComponent<Button>().image.color = new Color32(0, 174, 0, 255);
            if (!(SceneManager.GetActiveScene().name == "Introduction")) {
                var botaoNomePlanta = gameObject.transform.GetChild(1).gameObject;
                botaoNomePlanta.GetComponent<Button>().image.color = new Color32(0, 174, 0, 255);
            }
            estadoDoBotao = true; //seta o botao como ativo
        }
    }

    public void Start() {
        if (SceneManager.GetActiveScene().name == "Introduction") {
            sceneCanvas = GameObject.Find("Canvas"); //busca durante execucao pois é um prefab
        }
        else {
            sceneCanvas = GameObject.Find("VisaoDeCimaCanvas"); //busca durante execucao pois é um prefab
        }
        panelDetalhePlanta = sceneCanvas.transform.Find("PanelDetalhePlanta").gameObject;
    }

    public void instanciarDetalhesPlantas() {
        //seta os textos
        Text[] arrayTextoPainelDetalhePlanta = panelDetalhePlanta.GetComponentsInChildren<Text>(); //pega os campos de texto
        foreach (Text texto in arrayTextoPainelDetalhePlanta) { //define as informãções de texto
            if (texto.name == "Detalhes") {
                texto.text = informacoes;
            }
            if (texto.name == "NomeEspecie") {
                texto.text = nomePopular;
            }
        }
        //seta a imagem
        Image[] arrayImagemsPainelDetalhePlanta = panelDetalhePlanta.GetComponentsInChildren<Image>(); //pega os campos de texto
        foreach (Image imagem in arrayImagemsPainelDetalhePlanta) { //percorre o array de imagens
            if (imagem.name == "Image") {  //se é a imagem do painel detalhe planta
                Sprite sprite = Resources.Load("Imagens/" + Imagem, typeof(Sprite)) as Sprite;  //carrega a imagem de acordo com o nome
                imagem.overrideSprite = sprite; //seta a imagem
            }
        }
        if(SelecionarPlanta.Instance.getEstado() != estadoDoBotao) {
            SelecionarPlanta.Instance.clickBotao();
        }
        panelDetalhePlanta.SetActive(true); //ativa o painel
    }
}
