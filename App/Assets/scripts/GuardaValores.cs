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
        
        int ID_Ativo = 0;
        
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        
        string SQlQuery = "SELECT ID_usuario FROM Usuarios WHERE Email= '" + PlayerPrefs.GetString("EmailAtivo") +"'";
            dbcmd.CommandText = SQlQuery;
            reader = dbcmd.ExecuteReader();
            while(reader.Read())
            {
                string ID = reader.GetString(0);
                ID_Ativo= int.Parse("ID");
            }

        reader.Close();
        reader = null;


        int val= 0;
        if(val != 0)
        {
            string SQlQuery2 = "SELECT ID_dados FROM Dados WHERE FK_Usuário= '" + ID_Ativo +"'";
            dbcmd.CommandText = SQlQuery2;
            reader = dbcmd.ExecuteReader();
            while(reader.Read())
            {
                string ID= reader.GetString(0);
                val= int.Parse("ID");
            }
        }
        
        string SQlQuery3 = "Insert Into Dados(Hora, Glicemia, Data, FK_Usuário)" +
                          "Values('" + _HoraInput + "','" + _GlicemiaInput + "','" + _DataInput + "','"  + ID_Ativo + "')";
        dbcmd.CommandText = SQlQuery3;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {


        }



        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    

    }
}
