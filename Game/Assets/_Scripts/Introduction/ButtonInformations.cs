using UnityEngine;
using UnityEngine.UI;

public class ButtonInformations : MonoBehaviour {
    private string nomePopular;
    private string nomeCientifico;
    private string imagem;
    private string informacoes;
    public GameObject panelDetalhePlanta;
    public GameObject telaPanel;
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

        if (!DetalhePlantaManeger.botaoDetalhePlanta)//deseleciona o botao (coloca cor branca)
        {
            if (estadoDoBotao) {
                gameObject.GetComponent<UnityEngine.UI.Button>().image.color = new Color(255, 255, 255);
                estadoDoBotao = false; //seta o botao como desativo/falso
            } else //seta o botao (colocar a cor amarela)
              {
                gameObject.GetComponent<UnityEngine.UI.Button>().image.color = Color.yellow;
                estadoDoBotao = true; //seta o botao como ativo
            }
        } else //se botao detalhe esta ativado
         {
            instanciarDetalhesPlantas();
        }
    }

    public void Start() {
        telaPanel = GameObject.Find("Canvas");//tem que usar o find pois essa classe é um prefab,e nao pode receber valores do jogo por referencia(jeito atraves do unity)
        panelDetalhePlanta = telaPanel.transform.Find("PanelDetalhePlanta").gameObject;
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
                Sprite sprite = Resources.Load("Imagens/" + nomePopular, typeof(Sprite)) as Sprite;  //carrega a imagem de acordo com o nome
                imagem.overrideSprite = sprite; //seta a imagem
            }
        }

        panelDetalhePlanta.SetActive(true); //ativa o painel

    }
}
