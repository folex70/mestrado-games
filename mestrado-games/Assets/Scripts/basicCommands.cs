using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class basicCommands : MonoBehaviour {

	public void loadScene(string sceneName)
	{
		//Application.LoadLevel (sceneName);

		SceneManager.LoadScene (sceneName);
	}
}
