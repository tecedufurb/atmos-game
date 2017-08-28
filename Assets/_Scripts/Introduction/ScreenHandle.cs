using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenHandle : MonoBehaviour {

    public string[] m_Message = new string[3];
    public static bool m_Clicked = false;

    private int index;
    private JasonController mJsonController;

    [SerializeField] private Text messageText;
    [SerializeField] private Button nextMessageButton;
    [SerializeField] private Button previousMessageButton;
    [SerializeField] private Button InformationButton;
    [SerializeField] private GameObject choosePlantsButton;
    [SerializeField] private GameObject introductionPanel;
    [SerializeField] private GameObject choosePlantsPanel;
    [SerializeField] private GameObject informationPanel;
    [SerializeField] private GameObject gridDePlantas;

    void Start() {
        index = 0;
        m_Message[0] = "Olá, essa fase irá testar seus conhecimentos em relação a área  de preservação permanente nos arredores dos rios...";
        m_Message[1] = "O objetivo é plantar as plantas corretas nos arredores do rio para formar a área de preservação permanente...";
        m_Message[2] = "Antes de começarmos, clique no botão Escolher plantas para selecionar as plantas com as quais você irá trabalhar.";
        messageText.text = m_Message[index];
        StartCoroutine(TypeText(m_Message[index], messageText));

        mJsonController = JasonController.transformaJson(); //cria e inicializa jasoncontroller
        insereBotoes();//carrega os botoes de plantas no canvas
    }

    public void previousMessege() {
        StopAllCoroutines();
        index--;
        messageText.text = m_Message[index];

        if (index == 0)
            previousMessageButton.gameObject.SetActive(false);

        nextMessageButton.gameObject.SetActive(true);
        choosePlantsButton.GetComponent<ResizeObject>().m_Active = false;
    }

    public void nextMessege() {
        StopAllCoroutines();
        index++;
        messageText.text = m_Message[index];

        StartCoroutine(TypeText(m_Message[index], messageText));

        if (index == m_Message.Length - 1) {
            nextMessageButton.gameObject.SetActive(false);
            choosePlantsButton.GetComponent<ResizeObject>().m_Active = true;
        }
        previousMessageButton.gameObject.SetActive(true);
    }

    public void EnableChoosePlants(bool active) {
        introductionPanel.SetActive(!active);
        choosePlantsPanel.SetActive(active);
    }

    public void EnableInformationPanel(bool active) {
        choosePlantsPanel.SetActive(!active);
        informationPanel.SetActive(active);
        InformationButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        m_Clicked = false;
    }

    public void InformationPlantsButton() {
        if (!m_Clicked) {
            InformationButton.GetComponent<Image>().color = new Color32(50, 97, 143, 255);
            m_Clicked = true;
        } else {
            InformationButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            m_Clicked = false;
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

    private IEnumerator TypeText(string message, Text messageText) {
        messageText.text = "";
        foreach (char letter in message.ToCharArray()) {
            messageText.text += letter;
            yield return 0;
            yield return new WaitForSeconds(.05f);
        }
    }

    private void insereBotoes() {//metodo que adiciona botoes de plantas ao canvas
        GameObject botaoPrefab = null;//botao que vai ser instanciado   
        if (botaoPrefab == null) //carrega prefab se esta vazio
            botaoPrefab = (Resources.Load("Prefabs/ButtonPlants") as GameObject);

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
                if (child != botaoPrefab.transform) {//pega o filho do botao
                    Image image = child.GetComponent<Image>();//pega a imagem do filho do botao
                    image.overrideSprite = sprite; //seta a imagem no filho
                }
            }
        }
    }
}
