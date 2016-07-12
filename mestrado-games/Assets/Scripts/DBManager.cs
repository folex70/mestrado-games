using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DBManager : MonoBehaviour {

	private string connectionString;
	public int i;


	// Use this for initialization
	void Start () {
		i=1;
		connectionString = "URI=file:" + Application.dataPath + "/DB/Quizgame.sqlite";
		GetQuestoes();
		//SetQuestoes("aaaaaa","bbbbbb","cccccccc","dddddddddd","aaaaa");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void GetQuestoes(){
		
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){
			
			dbConnection.Open();
			
			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = "SELECT * FROM quizgame_perguntas";
				
				dbCmd.CommandText = sqlQuery;
				
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while(reader.Read()){
						Debug.Log(reader.GetString(1)+" - "
														+reader.GetString(2)+" - "
														+reader.GetString(3)+" - "
														+reader.GetString(4)+" - "
														+reader.GetString(5) );
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}
		
	}
	
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
}
