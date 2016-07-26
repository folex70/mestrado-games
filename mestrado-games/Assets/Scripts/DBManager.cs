using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DBManager : MonoBehaviour {

	private string connectionString;
	//public int i;
	
	static public List<Pergunta> perguntasLista = new List<Pergunta>();
	static public List<Temas>temasLista = new List<Temas>();

	static public List<Pergunta> voufPerguntasLista = new List<Pergunta>();
	static public List<Temas> voufTemasLista = new List<Temas>();

	public Text textTema;
	public InputField enterTema;
	public InputField enterPergunta1;
	public InputField enterAlternativa1;
	public InputField enterAlternativa2;
	public InputField enterAlternativa3;
	public InputField enterAlternativa4;
	public Dropdown enterAlternativaCorreta;
	public string alternativaCorreta;

	public int countPerguntasCadastradas;
		
	// Use this for initialization
	void Start () {
		connectionString = "URI=file:" + Application.dataPath + "/mestradodb.sqlite";
		CreateTable ();
		voufCreateTable ();
		GetTemas ();
		voufGetTemas ();
		countPerguntasCadastradas = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateTable(){
		//Debug.Log ("entrou na createtable");
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = String.Format ("CREATE  TABLE  IF NOT EXISTS main.quizGame_table (idpergunta INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL  UNIQUE , pergunta TEXT NOT NULL , resposta1 TEXT NOT NULL , resposta2 TEXT NOT NULL , resposta3 TEXT NOT NULL , resposta4 TEXT NOT NULL , resposta_correta TEXT NOT NULL , pontos INTEGER NOT NULL ,idtema INTEGER NOT NULL , tema TEXT NOT NULL , data DATETIME NOT NULL  DEFAULT CURRENT_TIME)");
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close();
			}
		}
	}

	private void voufCreateTable(){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = String.Format ("CREATE  TABLE  IF NOT EXISTS \"main\".\"voufGame_table\" (\"idpergunta\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL  UNIQUE , \"pergunta\" TEXT NOT NULL ,\"verdadeiro_ou_falso\" TEXT NOT NULL , \"idtema\" INTEGER NOT NULL , \"tema\" TEXT NOT NULL , \"data\" DATETIME NOT NULL  DEFAULT CURRENT_TIME)");
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close();
			}
		}
	}
	
	private void GetPerguntas(){
		
		perguntasLista.Clear();
		
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){
			
			dbConnection.Open();
			
			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = "SELECT * FROM quizGame_table";
				
				dbCmd.CommandText = sqlQuery;
				
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while(reader.Read()){
						Debug.Log(reader.GetString(1)+" - "
														+reader.GetString(2)+" - "
														+reader.GetString(3)+" - "
														+reader.GetString(4)+" - "
														+reader.GetString(5)+" - "
														+reader.GetString(6)+" - "
														+reader.GetInt32(7)+" - "							
														+reader.GetInt32(8)+" - "
														+reader.GetString(9));
						//@TODO: adicionar o campo pergunta a tabela quizgame_perguntas
						perguntasLista.Add(new Pergunta(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetString(5),reader.GetString(6),reader.GetInt32(7),reader.GetInt32(8),reader.GetString(9)));
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}
		
	}

	public void GetPerguntaByTema(int idtema){

		perguntasLista.Clear();

		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){

			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = String.Format("SELECT * FROM quizGame_table where idtema = (\"{0}\");",idtema);
				//string sqlQuery = "SELECT * FROM quizGame_table";
				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader()){
					while(reader.Read()){
						Debug.Log(reader.GetString(1)+" - "
							+reader.GetString(2)+" - "
							+reader.GetString(3)+" - "
							+reader.GetString(4)+" - "
							+reader.GetString(5)+" - "
							+reader.GetString(6)+" - "
							+reader.GetInt32(7)+" - "							
							+reader.GetInt32(8)+" - "
							+reader.GetString(9));
						//@TODO: adicionar o campo pergunta a tabela quizgame_perguntas
						perguntasLista.Add(new Pergunta(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetString(5),reader.GetString(6),reader.GetInt32(7),reader.GetInt32(8),reader.GetString(9)));
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}

	}

	public void voufGetPerguntaByTema(int idtema){

		voufPerguntasLista.Clear();

		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){

			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = String.Format("SELECT * FROM voufGame_table where idtema = (\"{0}\");",idtema);
				dbCmd.CommandText = sqlQuery;
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while(reader.Read()){
						voufPerguntasLista.Add(new Pergunta(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetInt32(3),reader.GetString(4)));
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}
	}

	private void GetTemas(){

		temasLista.Clear();

		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){

			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = "select distinct idtema,tema from quizgame_table";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader()){
					while(reader.Read()){
						//Debug.Log(reader.GetInt32(0)+" - "+reader.GetString(1));
						//@TODO: adicionar o campo pergunta a tabela quizgame_perguntas
						temasLista.Add(new Temas(reader.GetInt32(0),reader.GetString(1)));
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}
	}

	private void voufGetTemas(){
		voufTemasLista.Clear();
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){
			dbConnection.Open();
			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = "select distinct idtema,tema from voufgame_table";
				dbCmd.CommandText = sqlQuery;
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while(reader.Read()){
						voufTemasLista.Add(new Temas(reader.GetInt32(0),reader.GetString(1)));
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}
	}


	//carregar e colocar na tela os temas
	private void ShowPerguntas()
	{
		GetPerguntas ();

		//limpar caso tenha outro score na tela para não ter repetição
		//foreach(GameObject score in GameObject.FindGameObjectsWithTag("score")){
		//	Destroy(score);
		//}

		for (int i = 0; i < temasLista.Count; i++) {

			if (i <= temasLista.Count - 1) {

				//GameObject tmpObject = Instantiate (temaPrefab);

				//Temas tmpScore = temasLista [i];

				//tmpObject.GetComponent<HighScoreScript> ().SetScore (tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString ());

				//tmpObject.transform.SetParent (temaParent);		

				//tmpObject.GetComponent<RectTransform> ().localScale = new Vector3 (0.004f,0.004f,0.02f);			
				//tmpObject.GetComponent<RectTransform> ().position = new Vector3 (-1.8f,temaParent.position.y,0);			
			}						
		}
	}

	//insert
	private void SetQuestoes(string pergunta,string alternativa1,string alternativa2,string alternativa3,string alternativa4,string alternativa_correta,int pontos, int idtema, string tema ){
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){
			
			dbConnection.Open();
			//insert into quizGame_table(pergunta,resposta1,resposta2,resposta3,resposta4,resposta_correta,pontos,idtema,tema)  values('quanto é 2+2','1','2','3','4','4','0','1','matematica') ;
			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = String.Format("insert into  quizGame_table(pergunta,resposta1,resposta2,resposta3,resposta4,resposta_correta,pontos,idtema,tema) values (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\");"
					,pergunta,alternativa1,alternativa2,alternativa3,alternativa4,alternativa_correta,pontos,idtema,tema);
				
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close();

			}
		}
	}

	//insert
	private void voufInsertQuestoes(string pergunta,string alternativa_correta, int idtema, string tema ){
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){
			dbConnection.Open();
			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = String.Format("insert into  voufGame_table(pergunta,verdadeiro_ou_falso,idtema,tema) values (\"{0}\",\"{1}\",\"{2}\",\"{3}\");"
					,pergunta,alternativa_correta,idtema,tema);
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
				string sqlQuery = String.Format("delete from  quizGame_table where idpergunta = (\"{0}\");",idpergunta);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close();
			}
		}
	}

	public void DeleteTema(){
		int idTema = PlayerPrefs.GetInt ("idTema");
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){

			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = String.Format("delete from  quizGame_table where idtema = (\"{0}\");",idTema);
				Debug.Log (sqlQuery);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close();

				PlayerPrefs.SetInt ("notaFinal"+idTema.ToString (),0);
				PlayerPrefs.SetInt ("acertos"+idTema.ToString (), (int) 0);

				string sceneName = SceneManager.GetActiveScene().name;
				// load the same scene
				SceneManager.LoadScene(sceneName,LoadSceneMode.Single);

			}
		}
	}

	//delete
	public void voufDeleteTema(){
		int idTema = PlayerPrefs.GetInt ("voufidTema");
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){

			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand()){
				string sqlQuery = String.Format("delete from  voufGame_table where idtema = (\"{0}\");",idTema);
				Debug.Log (sqlQuery);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close();

				PlayerPrefs.SetInt ("voufnotaFinal"+idTema.ToString (),0);
				PlayerPrefs.SetInt ("voufacertos"+idTema.ToString (), (int) 0);

				string sceneName = SceneManager.GetActiveScene().name;
				// load the same scene
				SceneManager.LoadScene(sceneName,LoadSceneMode.Single);

			}
		}
	}

	public void EnterPergunta(){
		Debug.Log (countPerguntasCadastradas);
		if (enterAlternativaCorreta.value == 0) {
			alternativaCorreta = enterAlternativa1.text;
		}
		else if(enterAlternativaCorreta.value == 1){
			alternativaCorreta = enterAlternativa2.text;
		}
		else if(enterAlternativaCorreta.value == 2){
			alternativaCorreta = enterAlternativa3.text;
		}
		else if(enterAlternativaCorreta.value == 3){
			alternativaCorreta = enterAlternativa4.text;
		}

		int idtema = temasLista.Count + 1;

		if (idtema <= 10 && countPerguntasCadastradas < 10) {
			//Debug.Log (enterPergunta1.text+" - "+ enterAlternativa1.text+" - "+enterAlternativa2.text+" - "+ enterAlternativa3.text+" - "+ enterAlternativa4.text+" - "+ alternativaCorreta+" - "+ 1+" - "+ idtema+" - "+ enterTema.text);
			SetQuestoes (enterPergunta1.text, enterAlternativa1.text, enterAlternativa2.text, enterAlternativa3.text, enterAlternativa4.text, alternativaCorreta, 0, idtema, enterTema.text);
			countPerguntasCadastradas++;
			textTema.text = enterTema.text;
			enterTema.gameObject.SetActive(false);
		}else{
			Debug.Log ("idtema: "+idtema+"-"+"Perguntas cadastradas: "+countPerguntasCadastradas);
			SceneManager.LoadScene("quiz-temas");
		}	

		enterPergunta1.text 	= string.Empty;
		enterAlternativa1.text	= string.Empty;
		enterAlternativa2.text	= string.Empty;
		enterAlternativa3.text	= string.Empty;
		enterAlternativa4.text	= string.Empty;
	}

	public void voufEnterPergunta(){

		if (enterAlternativaCorreta.value == 0) {
			alternativaCorreta = "V";
		}
		else if(enterAlternativaCorreta.value == 1){
			alternativaCorreta = "F";
		}

		int idtema = voufTemasLista.Count + 1;

		if (idtema <= 10 && countPerguntasCadastradas < 10) {
			voufInsertQuestoes (enterPergunta1.text,alternativaCorreta, idtema, enterTema.text);
			countPerguntasCadastradas++;
			textTema.text = enterTema.text;
			enterTema.gameObject.SetActive(false);
		}else{
			Debug.Log ("idtema: "+idtema+"-"+"Perguntas cadastradas: "+countPerguntasCadastradas);
			SceneManager.LoadScene ("vouf-temas");

		}	
		enterPergunta1.text 	= string.Empty;
	}

	//CREATE  TABLE  IF NOT EXISTS "main"."quizGame_table" ("idpergunta" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL  UNIQUE , "pergunta" TEXT NOT NULL , "resposta1" TEXT NOT NULL , "resposta2" TEXT NOT NULL , "resposta3" TEXT NOT NULL , "resposta4" TEXT NOT NULL , "resposta_correta" TEXT NOT NULL , "idtema" INTEGER NOT NULL , "tema" TEXT NOT NULL , "data" DATETIME NOT NULL  DEFAULT CURRENT_TIME)
	//select distinct idtema from quizgame_table ;
	//--insert into quizGame_table(pergunta,resposta1,resposta2,resposta3,resposta4,resposta_correta,pontos,idtema,tema)  values('quanto é 2+2','1','2','3','4','4','0','1','matematica') ;
	//CREATE  TABLE  IF NOT EXISTS "main"."voufGame_table" ("idpergunta" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL  UNIQUE , "pergunta" TEXT NOT NULL ,"verdadeiro_ou_falso" TEXT NOT NULL , "idtema" INTEGER NOT NULL , "tema" TEXT NOT NULL , "data" DATETIME NOT NULL  DEFAULT CURRENT_TIME)
	//--insert into voufGame_table(pergunta,verdadeiro_ou_falso,pontos,idtema,tema)  values('2+2 e 4?','V','0','1','matematica') ;
	//	INSERT INTO "main"."voufGame_table" ("pergunta","verdadeiro_ou_falso","idtema","tema") VALUES (?1,?2,?3,?4)	Parameters:	param 1 (text): 1+1 e igual a 2?	param 2 (text): V	param 3 (integer): 1	param 4 (text): matematica

}
