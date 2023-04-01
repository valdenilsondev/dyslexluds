using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerguntasVF : MonoBehaviour
{
    [Header("Configuração dos Textos")]
    public TextMeshProUGUI infoPergunta;
    //public GameObject perguntasImagem; Mudar modo de jogos depois para imagem
    public TextMeshProUGUI perguntasTxt;

    public TextMeshProUGUI notaFinalTxt;

    public TextMeshProUGUI Mensagem01Text;
    public TextMeshProUGUI Mensagem02Text;
    


    [Header("Configuração Barras")]
    public GameObject barraProgresso;
    public TextMeshProUGUI tempoJogo;
    public GameObject barraTempo;

    [Header("Configuração Botões")]
    public Button[] botoes;
    public Color corAcerto, corErro;

    [Header("Configuração Modo Jogo")]
    public bool perguntasAleat;
    public bool jogarComTempo;
    public float tempoResponder;
    public bool mostarCorreta;
    public int quantPiscar;



    [Header("Configuração Perguntas")]
    public string[] perguntas;
    public string[] correta;
    public List<int> listaPerguntas;


    [Header("Configuração dos Paineis")]
    public GameObject[] paineis;
    public GameObject[] estrela;



    [Header("Configuração dos Mensagens")]
    public string[] mensagem1;
    public string[] mensagem2;
    public Color[] corMensagem;



    private int idResponder;

    private float quantRespondida;

    private float percProgresso;

    private float notaFinal;

    private float valorQuestoes;

    private int quantAcertos;

    private float percTempo;

    private float tempoTime;

    public int notaMin1Star;
    public int notaMin2Star;

    private int nEstrelas;

    private int idNivel;

    private int idBtnCorreta;

    private bool exibindoCorreta;




    // Start is called before the first frame update
    void Start()
    {
        idNivel = PlayerPrefs.GetInt("idNivel");


        
       notaMin1Star =  PlayerPrefs.GetInt("nomeMin1Estrela");
       notaMin2Star =  PlayerPrefs.GetInt("nomeMin2Estrela");

        
        barraTempo.SetActive(false);
        montarListPertuntas();

        valorQuestoes = 10 / (float )perguntas.Length;

        progressaoBarra();
        controlleBarraTempo();

        paineis[0].SetActive(true);
        paineis[1].SetActive(false);



      

    }

    // Update is called once per frame
    void Update()
    {
        if(tempoJogo == true && exibindoCorreta == false) {
            tempoTime += Time.deltaTime;
            controlleBarraTempo();

            if(tempoTime >=tempoResponder) {
                proximaPergunta();
            }
        }
    }


    public void montarListPertuntas() {

        if(perguntasAleat == true) {

            int rand = Random.Range(0, perguntas.Length);






        }else {
            for(int i =0; i < perguntas.Length; i++) {
                listaPerguntas.Add(i);
            }
        }

        perguntasTxt.text = perguntas[idResponder];



    }


    public void reponderQuestao(string alternativa)
    {
        if(exibindoCorreta == true) {
          
            return;

        }


        if (correta[idResponder] == alternativa) {
            quantAcertos += 1;

         
        }
        switch (correta[idResponder]) {
            case "A":
            idBtnCorreta = 0;
            break;

            case "B":
            idBtnCorreta = 1;
            break;
       
        }


        if (mostarCorreta == true) {

            foreach(Button b in botoes) {
                b.image.color = corErro;
            }

            exibindoCorreta = true;
            botoes[idBtnCorreta].image.color = corAcerto;

            StartCoroutine("mostrarAlternativaCorreta");

        }
        else {
            proximaPergunta();
        }
       
    }


    public void proximaPergunta() {
       
        idResponder += 1;

        tempoTime = 0;

        quantRespondida += 1;

        progressaoBarra();

        if (idResponder < perguntas.Length) 
         {

            perguntasTxt.text = perguntas[idResponder];
        }else {

            calcularNotaFinal();
            


        }

        




    }

    private void progressaoBarra() {

        infoPergunta.text = "Respondeu " + quantRespondida  +" de "+ perguntas.Length + " perguntas";
        percProgresso = quantRespondida / perguntas.Length;
        barraProgresso.transform.localScale = new Vector3(percProgresso, 1, 1);
    }

    void controlleBarraTempo() {

        if (jogarComTempo == true) {

            barraTempo.SetActive(true);

        }
        else {

            barraTempo.SetActive(false);
        }

        percTempo = ((tempoTime - tempoResponder) / tempoResponder) * -1;

        if(percTempo < 0) {
            percTempo = 0;
        }
        barraTempo.transform.localScale = new Vector3(percTempo, 1, 1);

    }

    void calcularNotaFinal() {


        notaFinal = Mathf.RoundToInt(valorQuestoes * quantAcertos);

        if(notaFinal > PlayerPrefs.GetInt("notaFinal_" + idNivel.ToString())) {

            PlayerPrefs.SetInt("notaFinal_" + idNivel.ToString(), (int)notaFinal);

        }



        if (notaFinal == 10) {
            nEstrelas = 3;
        }
        else if (notaFinal >= notaMin1Star) {
            nEstrelas = 2;
        }
        else if (notaFinal >= notaMin2Star) {
            nEstrelas = 1;
        }
        notaFinalTxt.text = notaFinal.ToString();


      

        



       

        foreach (GameObject e in estrela) {
            e.SetActive(false);
        }

        for (int i = 0; i < nEstrelas; i++) {
            estrela[i].SetActive(true);
        }

        paineis[0].SetActive(false);
        paineis[1].SetActive(true);


    }

    IEnumerator mostrarAlternativaCorreta() 
        
    {
        for(int i = 0; i<quantPiscar; i++) {
            botoes[idBtnCorreta].image.color = corAcerto;
            yield return new WaitForSeconds(0.2f);
            botoes[idBtnCorreta].image.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }



        foreach (Button b in botoes) {
            b.image.color = Color.white;
        }

        exibindoCorreta = false;
        proximaPergunta();
    }

}
