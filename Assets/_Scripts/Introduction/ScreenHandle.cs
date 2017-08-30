using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenHandle : MonoBehaviour {
    
    public static bool m_Clicked = false;
    
    private JasonController mJsonController;
    [SerializeField] private Button InformationButton;
    [SerializeField] private GameObject ChoosePlantsButton;
    [SerializeField] private GameObject IntroductionPanel;
    [SerializeField] private GameObject ChoosePlantsPanel;
    [SerializeField] private GameObject InformationPanel;
    [SerializeField] private GameObject GridDePlantas;

    void Start() {
        mJsonController = JasonController.transformaJson(); //cria e inicializa jasoncontroller
        insereBotoes();//carrega os botoes de plantas no canvas
    }

    public void EnableChoosePlants(bool active) {
        IntroductionPanel.SetActive(!active);
        ChoosePlantsPanel.SetActive(active);
    }

    public void EnableInformationPanel(bool active) {
        ChoosePlantsPanel.SetActive(!active);
        InformationPanel.SetActive(active);

        if (!active) {
            InformationButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ShowInfirmationIcon(0);
            m_Clicked = false;
        }        
    }

    public void InformationPlantsButton() {
        if (!m_Clicked) {
            InformationButton.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            ShowInfirmationIcon(140);
            m_Clicked = true;
        } else {
            InformationButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ShowInfirmationIcon(0);
            m_Clicked = false;
        }
    }

    private void ShowInfirmationIcon(byte alpha) {
        GameObject[] plantsButtons = GameObject.FindGameObjectsWithTag("botaoDoCanvas");
        foreach (GameObject obj in plantsButtons) {
            foreach (Transform child in obj.transform) {//percorre os transforms do botaoPrefab
                if (child.name == "InformationIcon")
                    child.GetComponent<Image>().color = new Color32(255, 255, 255, alpha);
            }
        }        
    }

    public void PlayButton(string scene) {
        ButtonInformations[] plantsButtons = FindObjectsOfType<ButtonInformations>();
        foreach (ButtonInformations button in plantsButtons) {
            if (button.estadoDoBotao)
                PlantsSingleton.Instance.SelectedPlants.Add(button);
        }
        LoadScene(scene);
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    private void insereBotoes() {//metodo que adiciona botoes de plantas ao canvas
        GameObject botaoPrefab = null;//botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
            botaoPrefab = (Resources.Load("Prefabs/ButtonPlants") as GameObject);

        foreach (Planta p in mJsonController.plantas) {//para cada planta no json
            botaoPrefab = Instantiate(botaoPrefab) as GameObject; //instancia o botao
            botaoPrefab.transform.SetParent(GridDePlantas.transform, false); //coloca como pai o gridDePlantas
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
    }
}
