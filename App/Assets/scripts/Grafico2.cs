using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Grafico2 : MonoBehaviour
{
    [SerializeField] private Sprite CirculoSprite;
    private RectTransform graficoConteiner;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    
    List<int> valorLista= new List<int>() {0, 0, 0, 0, 0, 0, 0};
    int vez;
    
    private void Awake()
    {
        graficoConteiner = transform.Find("graficoConteiner").GetComponent<RectTransform>();
        labelTemplateX = graficoConteiner.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graficoConteiner.Find("labelTemplateY").GetComponent<RectTransform>();

        int valorG= int.Parse(PlayerPrefs.GetString("Glicemia"));
        string valorD= PlayerPrefs.GetString("Data");
        string valorH= PlayerPrefs.GetString("Hora");
        
        for (int i=0; i<7; i++)
            {
                int val= PlayerPrefs.GetInt("Lista" +i);
                valorLista[i]=val;
            }

        vez= PlayerPrefs.GetInt("Vez");
        AddGlicose(valorG);
        MostraGrafico(valorLista);
        
        for (int i=0; i<valorLista.Count; i++)
            {
                PlayerPrefs.SetInt("Lista" +i, (valorLista[i]));
            }
            PlayerPrefs.Save();
    }

    void AddGlicose(int valor)
    {
        if (vez!=7)
        {
            valorLista[vez]=(valor);
            vez+= 1;
            PlayerPrefs.SetInt("Vez", vez);
            PlayerPrefs.Save();
        }
        else
        {
            vez=0;
            for(int i=0; i<7; i++)
            {
                valorLista[i]=0;
            }
            valorLista[vez]=(valor);
            vez+= 1;
            PlayerPrefs.SetInt("Vez", vez);
            PlayerPrefs.Save();
        }    
    
    }   
    
    private GameObject CriarCirculo(Vector2 anchoredPosition)
    {
        GameObject gameObject= new GameObject("circulo", typeof(Image));
        gameObject.transform.SetParent(graficoConteiner, false);
        gameObject.GetComponent<Image>().sprite= CirculoSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition= (anchoredPosition);
        rectTransform.sizeDelta= new Vector2(11, 11);
        rectTransform.anchorMin= new Vector2(0, 0);
        rectTransform.anchorMax= new Vector2(0, 0);
        return gameObject;
    }

    private void MostraGrafico(List<int> valorLista)
    {
        float graficoHeight= graficoConteiner.sizeDelta.y;
        float yMaximum= 100f;
        float xSize= 30f;
        
        GameObject ultimoCirculoGameObject = null;
        for (int i=0; i< valorLista.Count; i++)
        {
            float xPosition= xSize + i * xSize;
            float yPosition= (valorLista[i]/yMaximum) * graficoHeight;
            GameObject circuloGameObject= CriarCirculo(new Vector2(xPosition, yPosition));
            if (ultimoCirculoGameObject!= null)
            {
                CreateDotConnection(ultimoCirculoGameObject.GetComponent<RectTransform>().anchoredPosition, circuloGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            ultimoCirculoGameObject=circuloGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graficoConteiner, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -10f);
            labelX.GetComponent<Text>().text = (i+1).ToString();
        }
        int yValores = 0;
        int separador= 6;
        for (int i=0; i <separador ; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graficoConteiner, false);
            labelY.gameObject.SetActive(true);
            float normalizaValor = i * 1f / separador;
            labelY.anchoredPosition = new Vector2(-8f, normalizaValor* 150);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(yValores).ToString();
            yValores+= 50;
        } 
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject= new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graficoConteiner, false);
        gameObject.GetComponent<Image>().color= new Color(1,1,1, .5f);
        RectTransform rectTransform= gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA,dotPositionB);
        rectTransform.sizeDelta= new Vector2(distance, 3f);
        rectTransform.anchorMin= new Vector2(0, 0);
        rectTransform.anchorMax= new Vector2(0, 0);
        rectTransform.anchoredPosition= dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles =  new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
