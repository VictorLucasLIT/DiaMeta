using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;

public class tela1 : MonoBehaviour
{
    

    public Text valorGtxt;
    public Text valorDtxt;
    public Text valorHtxt;

    public string inputGlicemia;
    public string inputData;
    public string inputHora;


    
    void Start()
    {
    
    }
    private void Update()
    {
       Load();
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
    
    public void Save()
    {
        PlayerPrefs.SetString("Glicemia", inputGlicemia);
        PlayerPrefs.SetString("Data", inputData);
        PlayerPrefs.SetString("Hora", inputHora);
    }
    
    public void Load()
    {
        PlayerPrefs.GetString("Glicemia");
        PlayerPrefs.GetString("Data");
        PlayerPrefs.GetString("Hora");
    }
    
    
}
