using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

	public void resposta(string alternativa){

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
		Debug.Log ("notafinal "+notaFinal+" acertos "+acertos);
		proximaPergunta ();
		Debug.Log ("notafinal "+notaFinal+" acertos "+acertos);
	}

	void proximaPergunta()
	{
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

			Application.LoadLevel("quiz-nota");
		}

	


	}


}
