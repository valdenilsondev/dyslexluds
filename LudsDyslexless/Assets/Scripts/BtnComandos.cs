using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnComandos : MonoBehaviour
{
    //ok
    /// <param name="nameScene"></param>
    public void GoToScene(string nameScene) {


        SceneManager.LoadScene(nameScene);

    }


    public void Sair() {

        Application.Quit();
    }

    public void JogarNovamente() {

        int idCena = PlayerPrefs.GetInt("idNivel");
        if (idCena != 0) {
            SceneManager.LoadScene(idCena.ToString());
        }

    }

}
