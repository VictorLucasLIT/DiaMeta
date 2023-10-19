using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;
using UnityEngine.SceneManagement;

public class testes : MonoBehaviour
{

        public string Cena;
        public string DataBaseNome;

        public InputField EmailInputL;
        public InputField SenhaInputL;
    
        public Text SenhaTxt;
        public Text EmailTxt;

        string Senha;
        int count;

        public void Logar()
        {
            var _EmailInputL = EmailInputL.text.Trim();
            var _SenhaInputL = SenhaInputL.text.Trim(); 
            string conn = SetDataBaseClass.SetDataBase(DataBaseNome + ".db");
            IDbConnection dbcon;
            IDbCommand dbcmd;
            IDataReader reader;

            dbcon = new SqliteConnection(conn);
            dbcon.Open();
            dbcmd = dbcon.CreateCommand();
            string SQlQuery = "Select Count (Email) From Usuarios Where Email= '"+ _EmailInputL +"'";
            dbcmd.CommandText = SQlQuery;
            object result = dbcmd.ExecuteScalar();

            if (result != null)
            {
             int.TryParse(result.ToString(), out count);
            }

            if (count==1)
            {


                string SQlQuery1 = "Select Senha From Usuarios Where Email= '"+ _EmailInputL +"'";
                dbcmd.CommandText = SQlQuery1;
                reader = dbcmd.ExecuteReader();
                while(reader.Read())
                {
                    string quant = reader.GetString(0);
                    Senha= quant;
                }

                EmailTxt.text= " ";
                
                if (_SenhaInputL== Senha)
                {
                    SceneManager.LoadScene(Cena); 
                    PlayerPrefs.SetString("Email_Ativo", _EmailInputL);
                }

                else
                {
                    SenhaTxt.text=("Sua senha está incorreta!");
                }
            }
            else
            {
                EmailTxt.text= "Seu email está incorreto!";
            }
            dbcmd.Dispose();
            dbcmd = null;
            dbcon.Close();
            dbcon = null;
        }
    }
