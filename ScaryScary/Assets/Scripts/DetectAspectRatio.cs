using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class DetectAspectRatio : MonoBehaviour {

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		GetComponent<CanvasScaler>().scaleFactor = ((float)Screen.width/1080f) ;//scaleheight;	
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () 
	{
		GetComponent<CanvasScaler>().scaleFactor = ((float)Screen.width/1080f) ;//scaleheight;	
	}
}
