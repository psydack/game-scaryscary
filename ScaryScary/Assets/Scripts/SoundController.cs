using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	
	public static SoundController instance;
	
	void Awake()
	{
		if( !instance )
		{
			instance = this;
			DontDestroyOnLoad( gameObject );
		}
		else
		{
			DestroyImmediate( gameObject );
		}
	}
	
	
}
