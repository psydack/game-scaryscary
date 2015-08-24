using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	public KeyCode right1;
	public KeyCode right2;
	
	public KeyCode left1;
	public KeyCode left2;
	
	public KeyCode scare;
	
	public delegate void KeyLeftDownEventHandler();
	public static event KeyLeftDownEventHandler keyLeftDown;
	
	public delegate void KeyRightDownEventHandler();
	public static event KeyRightDownEventHandler keyRightDown;
	
	public delegate void KeyScareDownEventHandler();
	public static event KeyScareDownEventHandler keyScareDown;
	
	
	// Update is called once per frame
	void Update () {
		
		if( Input.GetKeyDown(right1) || Input.GetKeyDown(right2) )
		{
			if( keyRightDown != null ) keyRightDown();
		}
		else if( Input.GetKeyDown(left1) || Input.GetKeyDown(left2) )
		{
			if( keyLeftDown != null ) keyLeftDown();
		}
		else if( Input.GetKeyDown(scare) )
		{
			if( keyScareDown != null ) keyScareDown();
		}
		
	}
}
