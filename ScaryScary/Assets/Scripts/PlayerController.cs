using UnityEngine;
using System.Collections;

[RequireComponent( typeof( InputController ) )]
public class PlayerController : MonoBehaviour {

	public static PlayerController instance;
	
	bool canDoAction = true;
	
	public GameObject player;
	AIController currentAI;
	
	
	
	void Awake()
	{
		if( !instance ) instance = this;
	}
	

	// Use this for initialization
	void Start () 
	{
		InputController.keyLeftDown += new InputController.KeyLeftDownEventHandler( MoveLeft );
		InputController.keyRightDown += new InputController.KeyRightDownEventHandler( MoveRight );
		InputController.keyScareDown += new InputController.KeyScareDownEventHandler( Scare );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void MoveLeft()
	{
		if( !canDoAction ) return;
		
		if( ObjectsController.instance )
		{
			ObjectsController.instance.MoveLeft();
		}
	}
	
	void MoveRight()
	{
		if( !canDoAction ) return;
		
		if( ObjectsController.instance )
		{
			ObjectsController.instance.MoveRight();
		}
	}
	
	void Scare()
	{
		if( !canDoAction ) return;
		GetComponent<PlayerSoundController>().PlayAudioScaring();
		
		if( currentAI )
		{
			if( currentAI.currentObjeto.gameObject == ObjectsController.instance.GetCurrentObject() )
			{
				if( currentAI.currentAction == AIController.ACTION.LOOKING_DEEP )
				{
					GetComponent<PlayerSoundController>().PlayAudioScaringRight();
					GameController.instance.AddScore( true );
					currentAI.Die();
					currentAI = null;
				}
				else if( currentAI.currentAction == AIController.ACTION.LEFT_LOOKING ||
				        currentAI.currentAction == AIController.ACTION.LOOKING )
				{
					GetComponent<PlayerSoundController>().PlayAudioScaringWrong();
					GameController.instance.AddScore( false );
					currentAI.ScareWrong();
					currentAI = null;
				}
			}
			
		}
		
		StartCoroutine( ScaleUp() );
	}
	
	
	
	IEnumerator ScaleUp()
	{
		canDoAction = false;
		player.transform.localScale = Vector3.one * 1.2f;
		player.GetComponent<SpriteRenderer>().color = Color.white;
		
		yield return new WaitForSeconds(.1f);
		
		player.transform.localEulerAngles = Vector3.forward * 33f;
		
		yield return new WaitForSeconds(.1f);
		
		player.transform.localEulerAngles = Vector3.forward * -33f;
		
		yield return new WaitForSeconds(.2f);
		
		ScaleDown ();
	}
	
	void ScaleDown()
	{
		player.transform.localScale = Vector3.one * .15f;
		canDoAction = true;
		player.GetComponent<SpriteRenderer>().color = new Color(1,1,1,.45f);
		player.transform.localEulerAngles = Vector3.zero;
	}
	
	public void SetCurrentAI( AIController _ai)
	{
		currentAI = _ai;
	}
}
