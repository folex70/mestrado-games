using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class DBManager : MonoBehaviour {

	private string connectionString;
	public int i;
	
	private List<Pergunta> perguntasLista = new List<Pergunta>();
	
	// Use this for initialization
	void Start () {
		i=1;
		connectionString = "URI=file:" + Application.dataPath + "/DB/Quizgame.sqlite";
		GetQuestoes();
		//SetQuestoes("aaaaaa","bbbbbb","cccccccc","dddddddddd","aaaaa");
		//DeleteQuestoes(2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void GetQuestoes(){
		
		perguntasLista.Clear();
		
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){
			
			dbConnection.Open();
			
			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = "SELECT * FROM quizgame_perguntas";
				
				dbCmd.CommandText = sqlQuery;
				
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while(reader.Read()){
						/*Debug.Log(reader.GetString(1)+" - "
														+reader.GetString(2)+" - "
														+reader.GetString(3)+" - "
														+reader.GetString(4)+" - "
														+reader.GetString(5) );*/
						//@TODO: adicionar o campo pergunta a tabela quizgame_perguntas
						perguntasLista.Add(new Pergunta(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetString(5),reader.GetString(6)));
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}
		
	}
	//insert
	private void SetQuestoes(string alternativa1,string alternativa2,string alternativa3,string alternativa4,string alternativa_correta){
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){
			
			dbConnection.Open();
			
			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = String.Format("insert into quizgame_perguntas(alternativa1,alternativa2,alternativa3,alternativa4,alternativa_correta) values (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\");"
				,alternativa1,alternativa2,alternativa3,alternativa4,alternativa_correta);
				
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close();

			}
		}
	}
	
	private void DeleteQuestoes(int idpergunta){
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){
			
			dbConnection.Open();
			
			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = String.Format("delete from quizgame_perguntas where idpergunta = (\"{0}\");",idpergunta);
				
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close();

			}
		}
	}
	
}
