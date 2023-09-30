using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public string Cena;
    public string DataBaseNome;

    public InputField EmailInput;
    public InputField SenhaInput;
    
    public Text SenhaTxt;
    public Text EmailTxt;

    string Senha;
    int Quant; 

    public void Logar()
    {
        var _EmailInput = EmailInput.text.Trim();
        var _SenhaInput = SenhaInput.text.Trim();
        string conn = SetDataBaseClass.SetDataBase(DataBaseNome + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string SQlQuery = "Select Count (Email) From Usuarios Where Email= '"+ _EmailInput +"'";
        dbcmd.CommandText = SQlQuery;
        reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {
            string quant = reader.GetString(0);
            Quant = int.Parse(quant);
        }
        
        reader.Close();
        reader = null;

        if (Quant==1)
        {
            

            string SQlQuery1 = "Select Senha From Usuarios Where Email= '"+ _EmailInput +"'";
            dbcmd.CommandText = SQlQuery1;
            reader = dbcmd.ExecuteReader();
            while(reader.Read())
            {
                string quant = reader.GetString(0);
                Senha= quant;
            }
            
            if (_SenhaInput==Senha)
            {
                SceneManager.LoadScene(Cena);
                
            }
            
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }
}
}
