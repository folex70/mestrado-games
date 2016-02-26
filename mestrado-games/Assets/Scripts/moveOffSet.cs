using UnityEngine;
using System.Collections;

public class moveOffSet : MonoBehaviour {

	private Material material;
	public float vel =0.5f;
	public float offset;

	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		offset += 0.01f;

		material.SetTextureOffset ("_MainTex", new Vector2 (offset*vel,0)); //move a textura em x

	}
}
