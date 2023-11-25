using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;

public class Tabela : MonoBehaviour
{
    public string DataBaseNome;
    
    public Text Hora1;
    public Text Hora2;
    public Text Hora3;
    public Text Hora4;
    public Text Hora5;
    public Text Hora6;
    public Text Hora7;
    
    public Text Glicose1;
    public Text Glicose2;
    public Text Glicose3;
    public Text Glicose4;
    public Text Glicose5;
    public Text Glicose6;
    public Text Glicose7;

    string Data;
    List<int> valorLista= new List<int>() {0, 0, 0, 0, 0, 0, 0};
    List<string> ListaHora= new List<string>() {"00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00"};
    int vez;
    

    private void Start()
    {
        Data = PlayerPrefs.GetString("DataAtual");

        
        string valorD= PlayerPrefs.GetString("Data");
        string valorH= PlayerPrefs.GetString("Hora");

        vez= PlayerPrefs.GetInt("Vez");
        AddGlicose();

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
                    string SQlQuery3 = "SELECT Hora FROM Dados WHERE Data= '" + PlayerPrefs.GetString("DataAtual") +"' AND FK_Usuário= '"+ PlayerPrefs.GetInt("ID_Ativo") +"' AND ID_Dados= '"+ IdDados +"'AND Glicemia= '"+ result1 +"'";
                    dbcmd.CommandText = SQlQuery3;
                    object result2 = dbcmd.ExecuteScalar();
                    
                    int.TryParse(result1.ToString(), out ValorDado);
                    PlayerPrefs.SetInt("Lista"+i, ValorDado);
                    valorLista[i]= ValorDado;
                    ListaHora[i]= result2.ToString();
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
        Hora1.text=ListaHora[0];
        Hora2.text=ListaHora[1];
        Hora3.text=ListaHora[2];
        Hora4.text=ListaHora[3];
        Hora5.text=ListaHora[4];
        Hora6.text=ListaHora[5];
        Hora7.text=ListaHora[6];

        Glicose1.text=valorLista[0].ToString();
        Glicose2.text=valorLista[1].ToString();
        Glicose3.text=valorLista[2].ToString();
        Glicose4.text=valorLista[3].ToString();
        Glicose5.text=valorLista[4].ToString();
        Glicose6.text=valorLista[5].ToString();
        Glicose7.text=valorLista[6].ToString();
        }    
       
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;

    }
}
    