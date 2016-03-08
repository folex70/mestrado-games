using UnityEngine;
using System.Collections;

public class infoTheme : MonoBehaviour {


	public int idTema;

	public GameObject medalhaBronze;
	public GameObject medalhaPrata;
	public GameObject medalhaOuro;

	private int notaFinal;


	// Use this for initialization
	void Start () {
	
		medalhaBronze.SetActive (false);
		medalhaPrata.SetActive (false);
		medalhaOuro.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
