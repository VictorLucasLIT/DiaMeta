using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;

public class Grafico2 : MonoBehaviour
{
    [SerializeField] private Sprite CirculoSprite;
    private RectTransform graficoConteiner;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    public string DataBaseNome;

    string Data;
    List<int> valorLista= new List<int>() {0, 0, 0, 0, 0, 0, 0};
    int vez;
    
    private void Awake()
    {
        Data = PlayerPrefs.GetString("DataAtual");

        graficoConteiner = transform.Find("graficoConteiner").GetComponent<RectTransform>();
        labelTemplateX = graficoConteiner.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graficoConteiner.Find("labelTemplateY").GetComponent<RectTransform>();

        
        string valorD= PlayerPrefs.GetString("Data");
        string valorH= PlayerPrefs.GetString("Hora");

        vez= PlayerPrefs.GetInt("Vez");
        AddGlicose();
        MostraGrafico(valorLista);

        PlayerPrefs.Save();
        
    }

    void AddGlicose()
    {
        string conn = SetDataBaseClass.SetDataBase(DataBaseNome + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;
        
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();

        for (int i=0; i<valorLista.Count; i++)
            {
                int IdDados= i+1;
                string SQlQuery2 = "SELECT COUNT(Glicemia) FROM Dados WHERE Data= '" + PlayerPrefs.GetString("DataAtual") +"' AND FK_Usuário= '"+ PlayerPrefs.GetInt("ID_Ativo") +"' AND ID_Dados= '"+ IdDados +"'";
                dbcmd.CommandText = SQlQuery2;
                object result = dbcmd.ExecuteScalar();

                if(result != "0")
                {
                string SQlQuery1 = "SELECT Glicemia FROM Dados WHERE Data= '" + PlayerPrefs.GetString("DataAtual") +"' AND FK_Usuário= '"+ PlayerPrefs.GetInt("ID_Ativo") +"' AND ID_Dados= '"+ IdDados +"'";
                dbcmd.CommandText = SQlQuery1;
                object result1 = dbcmd.ExecuteScalar();
                int ValorDado;
                if (result1 !=null )
                {    
                    int.TryParse(result1.ToString(), out ValorDado);
                    PlayerPrefs.SetInt("Lista"+i, ValorDado);
                    valorLista[i]= ValorDado;
                }
                }
                else
                {
                    PlayerPrefs.SetInt("Lista"+i, 0);
                    valorLista[i]= 0;
                }
        
        /*
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
            PlayerPrefs.Save();*/
        }    
       dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null; 
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
