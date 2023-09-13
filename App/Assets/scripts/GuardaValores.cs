using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;

public class GuardaValores : MonoBehaviour
{
    public string DataBaseNome;
    public InputField HoraInput;
    public InputField GlicemiaInput;
    public InputField DataInput;

    public void InserirInfo()
    {
        var _HoraInput = HoraInput.text.Trim();
        var _GlicemiaInput = GlicemiaInput.text.Trim();
        var _DataInput = DataInput.text.Trim();

        string conn = SetDataBaseClass.SetDataBase(DataBaseNome + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        if (PlayerPrefs.GetInt("QuantID")==null)
        {    
            int QuantID= 1;
            PlayerPrefs.SetInt("QuantID", QuantID);
        }
        else
        {
            int QuantID= PlayerPrefs.GetInt("QuantID");
            QuantID+= 1;
            PlayerPrefs.SetInt("QuantID", QuantID);
        }
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string SQlQuery = "Insert Into Dados(Hora, Glicemia, Data, ID_Dados)" +
                          "Values('" + _HoraInput + "','" + _GlicemiaInput + "','" + _DataInput + "','" + PlayerPrefs.GetInt("QuantID") + "')";
        dbcmd.CommandText = SQlQuery;
        reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {
            //Essa parte e para pegar itens do banco de dados
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    

    }
}
