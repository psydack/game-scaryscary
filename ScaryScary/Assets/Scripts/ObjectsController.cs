using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsController : MonoBehaviour {
	
	public static ObjectsController instance;
	
	public int indexPossession = 0;
	public List<GameObject> objetos = new List<GameObject>();
	
	float colorPossession = 200f/255f;
	
	void Awake()
	{
		if( !instance ) instance = this;
	}
	
	
	// Use this for initialization
	void Start () {
		objetos[ indexPossession ].GetComponent<SpriteRenderer>().color = new Color(colorPossession,colorPossession,colorPossession,1);
	}
	
	
	public void MoveRight()
	{
		if( indexPossession >= objetos.Count - 1 ) return; 
		else {
			ResetColor();
			indexPossession += 1;
		}
		
		PossessionNewObject();
	}
	
	public void MoveLeft()
	{
		if( indexPossession == 0 ) return; 
		else  {
			ResetColor();
			indexPossession -= 1;
		}
		
		PossessionNewObject();
	}
	
	void PossessionNewObject()
	{
		if( PlayerController.instance )
		{
			objetos[ indexPossession ].GetComponent<SpriteRenderer>().color = new Color(colorPossession,colorPossession,colorPossession,1);
			PlayerController.instance.player.transform.localPosition = objetos[ indexPossession ].transform.localPosition;
			
		}
	}
	
	void ResetColor()
	{
		objetos[ indexPossession ].GetComponent<SpriteRenderer>().color = Color.white;
	}
	
	public GameObject GetCurrentObject()
	{
		return objetos[ indexPossession ] as GameObject;
	}
}
