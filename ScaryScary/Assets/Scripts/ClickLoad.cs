using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickLoad : MonoBehaviour {

	public string loadSceneName;
	
	void OnMouseDown()
	{
		SceneManager.LoadScene( loadSceneName );
	}
}
