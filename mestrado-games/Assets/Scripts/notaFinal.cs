using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class notaFinal : MonoBehaviour {

	private int idTema;

	public Text txtTema;
	public Text txtNota;

	public GameObject medalhaBronze;
	public GameObject medalhaPrata;
	public GameObject medalhaOuro;

	private int notaF;
	private int acertos;


	// Use this for initialization
	void Start () {

		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "quiz-nota") {

			medalhaBronze.SetActive (false);
			medalhaPrata.SetActive (false);
			medalhaOuro.SetActive (false);

			idTema = PlayerPrefs.GetInt("idTema");
			Debug.Log (idTema);
			notaF = PlayerPrefs.GetInt ("notaFinalTemp"+idTema.ToString ());
			acertos = PlayerPrefs.GetInt ("acertosTemp"+idTema.ToString ());

			txtNota.text = notaF.ToString ();

			txtTema.text = "Voce acertou "+acertos.ToString()+" de 10 pergutnas"; 

			if (notaF == 10) {
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (true);
				medalhaOuro.SetActive (true);
			} else if (notaF >= 7) {
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (true);
				medalhaOuro.SetActive (false);
			} else if(notaF >=3){
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (false);
				medalhaOuro.SetActive (false);
			}	
		}
		else if (sceneName == "vouf-nota") {

			medalhaBronze.SetActive (false);
			medalhaPrata.SetActive (false);
			medalhaOuro.SetActive (false);

			idTema = PlayerPrefs.GetInt("voufidTema");
			Debug.Log (idTema);
			notaF = PlayerPrefs.GetInt ("voufnotaFinalTemp"+idTema.ToString ());
			acertos = PlayerPrefs.GetInt ("voufacertosTemp"+idTema.ToString ());

			txtNota.text = notaF.ToString ();

			txtTema.text = "Voce acertou "+acertos.ToString()+" de 10 pergutnas"; 

			if (notaF == 10) {
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (true);
				medalhaOuro.SetActive (true);
			} else if (notaF >= 7) {
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (true);
				medalhaOuro.SetActive (false);
			} else if(notaF >=3){
				medalhaBronze.SetActive (true);
				medalhaPrata.SetActive (false);
				medalhaOuro.SetActive (false);
			}	
		}
	}

	public void jogarNovamente(){

		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "quiz-nota") {
			SceneManager.LoadScene ("quiz-tema1");		
		}
		else if (sceneName == "vouf-nota") {
			SceneManager.LoadScene ("vouf-tema1");		
		}
	}
}
