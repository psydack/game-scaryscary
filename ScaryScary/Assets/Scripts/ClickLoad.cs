using UnityEngine;
using System.Collections;

public class ClickLoad : MonoBehaviour {

	public string loadSceneName;
	
	void OnMouseDown()
	{
		Application.LoadLevel( loadSceneName );
	}
}
