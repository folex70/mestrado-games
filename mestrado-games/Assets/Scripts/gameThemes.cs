using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameThemes : MonoBehaviour {

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
	private int numeroQuestoes =20;
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




	}

	//usada quando clica em algum tema
	public void selectTheme(int i)
	{
		idTema = i;
		PlayerPrefs.SetInt ("idTema", idTema);
		textNomeTema.text = nomeTema [i];
		infoTemas.SetActive (true);
		btnPlay.interactable = true;
		textInfoTema.text = "Voce Acertou "+acertos+" de x";//+numeroQuestoes;

		notaF = PlayerPrefs.GetInt ("notaFinal"+idTema.ToString ());
		acertos = PlayerPrefs.GetInt ("acertos"+idTema.ToString ());


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
	}

	//usada quando clica no play
	public void play()
	{
		Application.LoadLevel ("quiz-tema"+idTema);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
