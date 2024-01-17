using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class passaTela : MonoBehaviour
{
    public InputField DataInput;
    
    public void LoadScenes(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void NovaData()
    {
        var _DataInput = DataInput.text.Trim();
        PlayerPrefs.SetString("DataAtual", _DataInput);
    }

    public void PassarIfComeca()
    {
        string EmailAtivo= PlayerPrefs.GetString("Email_Ativo");
        if(EmailAtivo!="")
        {
            SceneManager.LoadScene("TELA1");
        }
        else
        {
            SceneManager.LoadScene("LOGIN");
        }
    }
    public void PassarIfGraph(Text ErroTxt)
    {
        string EmailAtivo= PlayerPrefs.GetString("Email_Ativo");
        if(EmailAtivo!="")
        {
            SceneManager.LoadScene("DIAS");
        }
        else
        {
            ErroTxt.text="Nenhuma conta logada.";
        }
    }

    public void Deslogar()
    {
        PlayerPrefs.SetString("Email_Ativo", null);
        SceneManager.LoadScene("LOGIN");
    }
}
