﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameThemes : MonoBehaviour {

	public Button btnDelete;

	public Button btnPlay;
	public Text textNomeTema;

	public GameObject infoTemas;
	public Text textInfoTema;
	public GameObject medalhaBronze;
	public GameObject medalhaPrata;
	public GameObject medalhaOuro;

	public string[] nomeTema;

	private int idTema;
	private int notaF;
	//private int numeroQuestoes =10;
	private int acertos = 0;
	private int notaFinal;

	// Use this for initialization
	void Start () {
		idTema = 0;
		textNomeTema.text = nomeTema [idTema];
		textInfoTema.text = "Voce Acertou X de X questoes";
		infoTemas.SetActive (false);
		medalhaBronze.SetActive (false);
		medalhaPrata.SetActive (false);
		medalhaOuro.SetActive (false);
		btnPlay.interactable = false;
		btnDelete.interactable = false;
		//Debug.Log(DBManager.temasLista[0].IdTema);

		for(int i = 0; i < DBManager.temasLista.Count; i++){
			nomeTema [i+1] = DBManager.temasLista [i].Tema;
		}
	}

	//usada quando clica em algum tema
	public void selectTheme(int i)
	{
		//Debug.Log(PlayerPrefs.GetInt("idTema"));
		if (nomeTema [i] != "Vazio") {
			//carregar perguntas desse tema 
			//DBManager.GetPerguntaByTema(nomeTema[i]);

			idTema = i;
			PlayerPrefs.SetInt ("idTema", idTema);
			textNomeTema.text = nomeTema [i];
			infoTemas.SetActive (true);	
			btnPlay.interactable = true;
			btnDelete.interactable = true;

			textInfoTema.text = "Voce Acertou " + acertos + " de 10";//+numeroQuestoes;

			notaF = PlayerPrefs.GetInt ("notaFinal" + idTema.ToString ());
			acertos = PlayerPrefs.GetInt ("acertos" + idTema.ToString ());

			if (notaF == 10) {
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (true);
				medalhaOuro.SetActive (true);
			} else if (notaF >= 7) {
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (true);
				medalhaOuro.SetActive (false);
			} else if (notaF >= 3) {
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (false);
				medalhaOuro.SetActive (false);
			} else {
				medalhaBronze.SetActive (false);
				medalhaPrata.SetActive (false);
				medalhaOuro.SetActive (false);
			}
		} else {
			idTema = i;
			PlayerPrefs.SetInt ("idTema", idTema);
			textNomeTema.text = nomeTema [i];
			infoTemas.SetActive (false);
			medalhaBronze.SetActive (false);
			medalhaPrata.SetActive (false);
			medalhaOuro.SetActive (false);
			btnPlay.interactable = false;
			btnDelete.interactable = false;
		}
	}

	//usada quando clica no play
	public void play()
	{
		//Application.LoadLevel ("quiz-tema1");	
		SceneManager.LoadScene ("quiz-tema1");
	}

}
