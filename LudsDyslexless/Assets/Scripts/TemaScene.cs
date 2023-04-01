using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class TemaScene : MonoBehaviour
{

    

    public TextMeshProUGUI nameTemaTXT;

    public Button btnJogar;
    // Start is called before the first frame update
    void Start()
    {
        btnJogar.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Jogar() {

       int idCena = PlayerPrefs.GetInt("idNivel");
        if(idCena != 0) {
            SceneManager.LoadScene(idCena.ToString());
        }

        
    }
}
