using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class LoginResponsividade : MonoBehaviour {
// Variáveis para armazenar as dimensões da tela
    public int larguraDaTela;
    public int alturaDaTela;
    public float UiWidth;
    public float UiHeight;

    Dictionary<string, float> DicionarioUW = new Dictionary<string, float>()
     {
        {"BackGround", 182.1375f}
     };
     
    Dictionary<string, float> DicionarioUH = new Dictionary<string, float>()
     {
        {"BackGround", 165.2015625f}
     };
    void Start()
    {
        GameObject UiObj = gameObject;
        string NomeGO=UiObj.name;
        RectTransform UiRectTransform = GetComponent<RectTransform>();
        // Obter as dimensões da tela e armazená-las nas variáveis
        larguraDaTela = Screen.width;
        alturaDaTela = Screen.height;
        UiWidth = UiRectTransform.rect.width;
        UiHeight = UiRectTransform.rect.height;
        Debug.Log(UiHeight); 
        //UiRectTransform.sizeDelta = new Vector2(((DicionarioUW[NomeGO]*larguraDaTela)/100), ((DicionarioUH[NomeGO]*alturaDaTela)/100));
        UiRectTransform.sizeDelta = new Vector2(20f, 0f);
        UiHeight = UiRectTransform.rect.height;
        // Exemplo de uso
        

        
    }
 
}
