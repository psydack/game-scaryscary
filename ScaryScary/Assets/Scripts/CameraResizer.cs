using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class CameraResizer : MonoBehaviour {

	public int targetWidth = 1920;
	public float pixelsToUnits = 100;
	
	void Update() {
		
		int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
		
		GetComponent<Camera>().orthographicSize = height / pixelsToUnits / 2;
	}
}
