using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;
using UnityEngine.SceneManagement;

public class Cadastro : MonoBehaviour
{
    public string DataBaseNome;
    
    public InputField NomeInput;
    public InputField SobrenomeInput;
    public InputField EmailInput;
    public InputField SenhaInput;
    public InputField EmailAtivo;
    public string Cena;
    public Text SenhaTxt;
    public Text EmailTxt;

    public void InserirInfo()
    {
        
        
        var _NomeInput = NomeInput.text.Trim();
        var _SobrenomeInput = SobrenomeInput.text.Trim();
        var _EmailInput = EmailInput.text.Trim();
        var _SenhaInput = SenhaInput.text.Trim();

        string conn = SetDataBaseClass.SetDataBase(DataBaseNome + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string SQlQuery = "Select Count (Nome) From Usuarios Where Email= '"+ _EmailInput +"'";
        dbcmd.CommandText = SQlQuery;
        object result = dbcmd.ExecuteScalar();
        
        if(result.ToString()=="0")
        {
            EmailTxt.text= "";
        if((_EmailInput.ToString()).Length>=7)
        {
            EmailTxt.text= "";
            if((_SenhaInput.ToString()).Length>=8)
        {
            string SQlQuery2 = "Insert Into Usuarios(Nome, Sobrenome, Email, Senha)" +
                              "Values('" + _NomeInput + "','" + _SobrenomeInput + "','" + _EmailInput + "','" + _SenhaInput + "')";
            dbcmd.CommandText = SQlQuery2;
            reader = dbcmd.ExecuteReader();
            SceneManager.LoadScene(Cena);
        }
        else
        {
            SenhaTxt.text= "A senha possui menos de 8 caracteres";
        }
        }
            else
        {
            EmailTxt.text= "O email possui menos de 7 caracteres";
        }
        }
        else
        {
            
            EmailTxt.text= "O email já está cadastrado";
        }
            dbcmd.Dispose();
            dbcmd = null;
            dbcon.Close();
            dbcon = null;
        
        /*dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string SQlQuery2 = "SELECT ID_usuario FROM Usuarios WHERE Email = '"+ _EmailInput + "'";
        dbcmd.CommandText = SQlQuery2;
        reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {
            string ID= reader.GetString(0);
            PlayerPrefs.SetString("ID_Ativo", "2");
            
        }
        

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;*/

        PlayerPrefs.SetString("Email_Ativo", EmailAtivo.text.Trim());
    }
    
}
