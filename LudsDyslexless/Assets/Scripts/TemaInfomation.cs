using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TemaInfomation : MonoBehaviour
{

    private TemaScene temaScene;

    [Header("Configura Nível")]
    public int idNivel;
    public string nomeNivel;
    public Color corTema;

    [Header("Condfiguração Stars")]
    public int notaMin1Star;
    public int notaMin2Star;


    [Header("Condfiguração Botão")]
    public TextMeshProUGUI idNivelTxt;
    public GameObject[] stars;

    public int notaFinal;

    private void Awake() {
        stars[0].SetActive(false);
        stars[1].SetActive(false);
        stars[2].SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        idNivelTxt.text = idNivel.ToString();


      

         notaFinal = PlayerPrefs.GetInt("notaFinal_" + idNivel.ToString());
        

        starsControl(); //Invoca o método que define quantas estrelas ganhamos

        temaScene = FindAnyObjectByType(typeof(TemaScene)) as TemaScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectTema() {

        temaScene.nameTemaTXT.text = nomeNivel;
        temaScene.nameTemaTXT.color = corTema;
        
        PlayerPrefs.SetInt("idNivel", idNivel);
        PlayerPrefs.SetString("nomeNivel", nomeNivel);
        PlayerPrefs.SetInt("nomeMin1Estrela", notaMin1Star);
        PlayerPrefs.SetInt("nomeMin2Estrela", notaMin2Star);
        
        temaScene.btnJogar.interactable = true;

    }

    public void starsControl() {

       foreach (GameObject e in stars) {
            e.SetActive(false);
       }

        int nStars = 0;

        if(notaFinal == 10) {
            nStars = 3;
        }else if(notaFinal >= notaMin1Star) {
            nStars = 2;
        }else if(notaFinal >= notaMin2Star) {
            nStars = 1;
        }


        for(int i =0; i<nStars; i++) {
            stars[i].SetActive(true);
        }
    }
}
