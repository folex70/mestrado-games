using UnityEngine;
using System.Collections;

public class infoTheme : MonoBehaviour {


	public int idTema;

	private int notaF;

	public GameObject medalhaBronze;
	public GameObject medalhaPrata;
	public GameObject medalhaOuro;

	private int notaFinal;


	// Use this for initialization
	void Start () {
	
		medalhaBronze.SetActive (false);
		medalhaPrata.SetActive (false);
		medalhaOuro.SetActive (false);


		notaF = PlayerPrefs.GetInt ("notaFinal"+idTema.ToString ());
		//Debug.Log (notaF+" nofaF dentro de infoTheme"+idTema);
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
