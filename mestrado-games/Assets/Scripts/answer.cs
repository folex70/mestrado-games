using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class answer : MonoBehaviour {

	private int idTema;

	public Text pergunta;
	public Text Resp1;
	public Text Resp2;
	public Text Resp3;
	public Text Resp4;
	public Text info;

	public string[] perguntas;
	public string[] alternativaA;
	public string[] alternativaB;
	public string[] alternativaC;
	public string[] alternativaD;
	public string[] corretas;

	private int idPergunta;

	private float acertos;
	private float questoes;
	private float media;
	private int notaFinal;

	// Use this for initialization
	void Start () {
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "quiz-tema1") {
			//carregar dados no arry com os da lista que veio do banco
			for(int i = 0; i < DBManager.perguntasLista.Count; i++){
				perguntas[i] 	= DBManager.perguntasLista [i].Pergunta1;
				alternativaA[i] = DBManager.perguntasLista [i].Alternativa1;
				alternativaB[i] = DBManager.perguntasLista [i].Alternativa2;
				alternativaC[i] = DBManager.perguntasLista [i].Alternativa3;
				alternativaD[i] = DBManager.perguntasLista [i].Alternativa4;
				corretas[i] 	= DBManager.perguntasLista [i].Resposta1;
			}

			idTema = PlayerPrefs.GetInt ("idTema");
			idPergunta = 0;
			questoes = perguntas.Length;
			pergunta.text = perguntas [idPergunta];
			Resp1.text = alternativaA [idPergunta];
			Resp2.text = alternativaB [idPergunta];
			Resp3.text = alternativaC [idPergunta];
			Resp4.text = alternativaD [idPergunta];

			info.text = "Respondendo "+(idPergunta +1).ToString()
				+ " de "+questoes.ToString()+" perguntas.";	
		}
		else if(sceneName == "vouf-tema1") {
			for(int i = 0; i < DBManager.voufPerguntasLista.Count; i++){
				perguntas[i] 	= DBManager.voufPerguntasLista [i].Pergunta1;
				corretas[i] 	= DBManager.voufPerguntasLista [i].Resposta1;
			}

			idTema = PlayerPrefs.GetInt ("voufidTema");
			idPergunta = 0;
			questoes = perguntas.Length;
			pergunta.text = perguntas [idPergunta];

			info.text = "Respondendo "+(idPergunta +1).ToString()
				+ " de "+questoes.ToString()+" perguntas.";	
		}

	}

	public void resposta(string alternativa){
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "quiz-tema1") {
			switch (alternativa) {
			case "A":
				if(alternativaA[idPergunta] == corretas[idPergunta])
				{
					acertos += 1;
				}
				break;
			case "B":
				if(alternativaB[idPergunta] == corretas[idPergunta])
				{
					acertos +=1;
				}
				break;
			case "C":
				if(alternativaC[idPergunta] == corretas[idPergunta])
				{
					acertos +=1;
				}
				break;
			case "D":
				if(alternativaD[idPergunta] == corretas[idPergunta])
				{
					acertos +=1;
				}
				break;
			}	
		}
		else if(sceneName == "vouf-tema1") {
			switch (alternativa) {
				case "V":
					if (alternativa == corretas [idPergunta]) {
						acertos += 1;
					}
				break;
				case "F":
					if (alternativa == corretas [idPergunta]) {
						acertos += 1;
					}
				break;
			}
		}

		Debug.Log ("notafinal "+notaFinal+" acertos "+acertos);
		proximaPergunta ();
		Debug.Log ("notafinal "+notaFinal+" acertos "+acertos);
	}

	void proximaPergunta()
	{
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "quiz-tema1") {
			idPergunta += 1;

			if (idPergunta <= (questoes - 1)) {
				pergunta.text = perguntas [idPergunta];
				Resp1.text = alternativaA [idPergunta];
				Resp2.text = alternativaB [idPergunta];
				Resp3.text = alternativaC [idPergunta];
				Resp4.text = alternativaD [idPergunta];

				info.text = "Respondendo "+(idPergunta +1).ToString()
					+ " de "+questoes.ToString()+" perguntas.";
			} else {
				media = 10 * (acertos/questoes);
				notaFinal = Mathf.RoundToInt(media);

				if(notaFinal > PlayerPrefs.GetInt("notaFinal"+idTema.ToString ()) )//so grava aqui se bater o recorde
				{
					PlayerPrefs.SetInt ("notaFinal"+idTema.ToString (),notaFinal);
					PlayerPrefs.SetInt ("acertos"+idTema.ToString (), (int) acertos);
				}

				PlayerPrefs.SetInt ("notaFinalTemp"+idTema.ToString (),notaFinal);
				PlayerPrefs.SetInt ("acertosTemp"+idTema.ToString (), (int) acertos);

				//Application.LoadLevel("quiz-nota");
				SceneManager.LoadScene ("quiz-nota");
			}
		}
		else if(sceneName == "vouf-tema1") {
			idPergunta += 1;

			if (idPergunta <= (questoes - 1)) {
				pergunta.text = perguntas [idPergunta];

				info.text = "Respondendo "+(idPergunta +1).ToString()
					+ " de "+questoes.ToString()+" perguntas.";
			} else {
				media = 10 * (acertos/questoes);
				notaFinal = Mathf.RoundToInt(media);

				if(notaFinal > PlayerPrefs.GetInt("voufnotaFinal"+idTema.ToString ()) )//so grava aqui se bater o recorde
				{
					PlayerPrefs.SetInt ("voufnotaFinal"+idTema.ToString (),notaFinal);
					PlayerPrefs.SetInt ("voufacertos"+idTema.ToString (), (int) acertos);
				}

				PlayerPrefs.SetInt ("voufnotaFinalTemp"+idTema.ToString (),notaFinal);
				PlayerPrefs.SetInt ("voufacertosTemp"+idTema.ToString (), (int) acertos);

				SceneManager.LoadScene ("vouf-nota");
			}
		}

	}
}
