using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;
using UnityEngine.SceneManagement;

public class GuardaValores : MonoBehaviour
{
    public string DataBaseNome;
    public InputField HoraInput;
    public InputField GlicemiaInput;
    public InputField DataInput;
    public Text ErroTxt;
    int count;
    public void Start()
    {
        Debug.Log(PlayerPrefs.GetString("Email_Ativo"));
    }

    public void InserirInfo()
    {
        var _HoraInput = HoraInput.text.Trim();
        var _GlicemiaInput = GlicemiaInput.text.Trim();
        var _DataInput = DataInput.text.Trim();

        string conn = SetDataBaseClass.SetDataBase(DataBaseNome + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;
        
        int ID_Ativo=0;
        
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();

        

            string SQlQuery = "SELECT ID_usuario FROM Usuarios WHERE Email= '" + PlayerPrefs.GetString("Email_Ativo") +"'";
            dbcmd.CommandText = SQlQuery;
            object result = dbcmd.ExecuteScalar();

            if (result != null)
            {
              int.TryParse(result.ToString(), out ID_Ativo);
            }

            else
            {
                // Trate o caso em que nenhum resultado foi retornado
                Debug.Log("Nenhum email encontrado.");
            }


        int val= 0;

        string SQlQuery2 = "SELECT COUNT(ID_Dados) FROM Dados WHERE FK_Usuário= '" + ID_Ativo +"'AND Data= '"+ _DataInput +"'";
        dbcmd.CommandText = SQlQuery2;
        object result1 = dbcmd.ExecuteScalar();
        
        if (result1 != null)
        {
            int.TryParse(result1.ToString(), out val);
        }

        else
        {
            // Trate o caso em que nenhum resultado foi retornado
            Debug.Log("Nenhum email encontrado.");
        }

        int ValFinal= val + 1;

        if(val<7){
            string SQlQuery3 = "Insert Into Dados(Hora, Glicemia, Data, FK_Usuário, ID_Dados)" +
                            "Values('" + _HoraInput + "','" + _GlicemiaInput + "','" + _DataInput + "','"  + ID_Ativo + "','" + ValFinal +"')";
            dbcmd.CommandText = SQlQuery3;
            reader = dbcmd.ExecuteReader();
            SceneManager.LoadScene("GRAFICO");
        }
        else{
            ErroTxt.text= "Já foram armazenados outros 7 dados neste dia. tente em outro, por favor.";
        }
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    
        PlayerPrefs.SetString("DataAtual", _DataInput);
        PlayerPrefs.SetInt("ID_Ativo", ID_Ativo);
    }
}
