using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameForca : MonoBehaviour {

	public int idTema;
	public int notaFinal;

	public GameObject dica1;
	public GameObject dica2;
	public GameObject dica3;
	public GameObject dica4;
	public Text textDica1;
	public Text textDica2;
	public Text textDica3;
	public Text textDica4;

	public string palavraCorreta;

	public Text textoTema;
	public Text textoTempo;

	public InputField palavra;

	public float tempoDecorrido;

	public int countErros = 0;

	public Animator forcaAnimator;
	// Use this for initialization
	void Start () {
		idTema = PlayerPrefs.GetInt ("forcaidTema");

		forcaAnimator.Play ("forca_anim");

		dica1.SetActive (false);	
		dica2.SetActive (false);	
		dica3.SetActive (false);	
		dica4.SetActive (false);	
		//Debug.Log (DBManager.forcaPerguntasLista [0].Alternativa1);
		textDica1.text = DBManager.forcaPerguntasLista [0].Alternativa1;
		textDica2.text = DBManager.forcaPerguntasLista [0].Alternativa2;
		textDica3.text = DBManager.forcaPerguntasLista [0].Alternativa3;
		textDica4.text = DBManager.forcaPerguntasLista [0].Alternativa4;
		textoTema.text = "Tema: "+DBManager.forcaPerguntasLista [0].Tema;
		palavraCorreta = DBManager.forcaPerguntasLista [0].Pergunta1;
	}

	void Update(){
		tempoDecorrido += Time.deltaTime;
		textoTempo.text = "" + tempoDecorrido;

		if (tempoDecorrido < 10) {
			textoTempo.color = Color.green;
			notaFinal = 10;
		} else if (tempoDecorrido > 10 && tempoDecorrido < 30) {
			textoTempo.color = Color.yellow;
			notaFinal = 7;
		} else {
			textoTempo.color = Color.red;
			notaFinal = 3;
		}

	}

	public void ProcessaResposta(){


		if (palavra.text != "") {
			if (palavra.text == palavraCorreta) {
				//acertou, mensagem de acerto e sai da cena após 3 segundos
				notaFinal = notaFinal - (2*countErros);
				PlayerPrefs.SetInt ("forcanotaFinal"+idTema.ToString (),notaFinal);
				PlayerPrefs.SetInt ("forcanotaFinalTemp"+idTema.ToString (),notaFinal);
				SceneManager.LoadScene ("forca-nota");
			} else {
				//errou
				forcaAnimator.Play ("forca_anim_erro_1");
				dica1.SetActive (true);	
				if (countErros == 1) {
					forcaAnimator.Play ("forca_anim_erro_2");
					dica2.SetActive (true);	
				}
				if (countErros == 2) {
					forcaAnimator.Play ("forca_anim_erro_3");
					dica3.SetActive (true);	
				}
				if (countErros == 3) {
					dica4.SetActive (true);	
					forcaAnimator.Play ("forca_anim_erro_4");
				}
				if (countErros == 4) {
					notaFinal = 0;
					PlayerPrefs.SetInt ("forcanotaFinal"+idTema.ToString (),notaFinal);
					PlayerPrefs.SetInt ("forcanotaFinalTemp"+idTema.ToString (),notaFinal);
					//gameover da um tempo e vai pra tela de titulo
					SceneManager.LoadScene ("forca-nota");
				}
				countErros++;
			}	
		} else {
			Debug.Log ("resposta vazia");
		}
	}
}

