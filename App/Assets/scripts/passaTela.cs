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
}
