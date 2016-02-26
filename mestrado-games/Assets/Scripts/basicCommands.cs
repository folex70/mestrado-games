using UnityEngine;
using System.Collections;

public class basicCommands : MonoBehaviour {

	public void loadScene(string sceneName)
	{
		Application.LoadLevel (sceneName);
	}
}
