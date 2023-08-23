using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NovaTela1 : MonoBehaviour
{
    /*public static string instance1;
    public static string instance2;
    public static string instance3;*/

    public string inputGlicemia;
    public string inputData; 
    public string inputHora;
    
    /*void start()
    {
        instance1= inputGlicemia;
        instance2= inputData;
        instance3= inputHora;
    }*/
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void MudarCena(string Tela2)
    {
        PlayerPrefs.SetString("Glicemia", inputGlicemia);
        PlayerPrefs.SetString("Data", inputData);
        PlayerPrefs.SetString("Hora", inputHora);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(Tela2);
    }

    public void Glicemia(string g)
    {
        inputGlicemia=g;
    
    }

    public void Data(string d)
    {
        inputData=d;

    }

    public void Hora(string h)
    {
        inputHora=h;
        
    }
}
