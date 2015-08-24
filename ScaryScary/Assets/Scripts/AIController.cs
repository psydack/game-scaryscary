using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	
	int lastSide = 0;
	int side = 0;
	float walkingSpeed = 3f;
	
	public enum ACTION
	{
		ENTERING,
		SEARCHING,
		LOOKING,
		LOOKING_DEEP,
		LEFT_LOOKING,
		DYING,
		EXITING,
		WAITING
		
	}
	
	//what we are doing
	public ACTION currentAction = ACTION.ENTERING;
	
	//bounds to control
	public Transform rightBound;
	public Transform leftBound;
	
	//current objeto (if is stop)
	public GameObject currentObjeto;
	
	//just a sohrtcut
	BalaoController bc;
	SpriteRenderer spr;
	
	//cooldown to generate new action
	float cooldownAction = 0;
	
	//action Chance Looking
	float actionChanceLooking = 0.6f;
	float actionChanceLookingDefault = 0.6f;
	
	float multAction = 0;
	float multActionTimer = 0;
	
	
	bool isEntered = false;
	
	public Sprite[] walkMan;
	public Sprite backMan;
	float animationTimer = 0;
	int indAnimatino = 1;
	
	public AudioClip walkSound;
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		bc = GetComponent<BalaoController>();
		spr = GetComponent<SpriteRenderer>();
	}
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () 
	{
		//random position to move initially
		if( UnityEngine.Random.Range (0f, 1f) > .5f ) side = 1;
		else side = -1;
		
		lastSide = side;
		transform.localScale = new Vector3( side * 1.2f, 1.2f, 1.2f);
		
		//its not entered yet
		isEntered = false;		
		//is entereing
		currentAction = ACTION.ENTERING;
		//reset baloons
		bc.ResetBaloes();
		//reset color
		GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
		//invert position
		Vector3 boundPosition = side > 0 ? leftBound.position : rightBound.position; 
		boundPosition.y = -2f;
		//set initial position
		transform.position = boundPosition;
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () 
	{
		
		switch( currentAction )
		{
			case ACTION.ENTERING:
				ActionEntering();
				break;
			case ACTION.SEARCHING:
				ActionSearching();
				break;
			case ACTION.LOOKING:
				ActionLooking();
				break;
			case ACTION.LOOKING_DEEP:
				ActionLookingDeep();
				break;
			case ACTION.LEFT_LOOKING:
				ActionLeftDeep();
				break;
			case ACTION.DYING:
				ActionDying();
				break;
			case ACTION.EXITING:
				ActionExiting();
				break;
			case ACTION.WAITING:
				ActionWaiting();
				break;
		}
		
		CheckMultipliers();
	}
	
	#region ACTIONS
	
	/// <summary>
	/// Actions the entering.
	/// </summary>
	void ActionEntering()
	{
		transform.Translate( Vector3.right * side * Time.deltaTime * walkingSpeed, Space.World );
		WalkAnimation();
	}
	
	/// <summary>
	/// Actions the searching.
	/// </summary>
	void ActionSearching()
	{
		bc.SetBalaoSearching();
		transform.Translate( Vector3.right * side * Time.deltaTime * walkingSpeed, Space.World );
		WalkAnimation();
		
		if( cooldownAction > .5f )
		{
			cooldownAction = 0;
			if( UnityEngine.Random.Range(0f, 1f) < .2f )
			{
				side *= -1;
				currentAction = ACTION.WAITING;
			}
			
		}
		else
		{
			cooldownAction += Time.deltaTime;
		}
	}
	
	void ActionWaiting()
	{
		BackAnimation();
		if( cooldownAction > .5f )
		{
			cooldownAction = 0;
			if( UnityEngine.Random.Range(0f, 1f) < .6f )
			{
				currentAction = ACTION.SEARCHING;
			}
			
		}
		else
		{
			cooldownAction += Time.deltaTime;
		}
	}
	
	/// <summary>
	/// Actions the looking.
	/// </summary>
	void ActionLooking()
	{
		BackAnimation();
		bc.SetBalaoLooking();
		if( cooldownAction > .5f )
		{
			float rnd = UnityEngine.Random.Range(0f, 1f);
			
			if( rnd < .3f - multAction)
			{
				currentAction = ACTION.LOOKING;
			}
			else if( rnd <= .4f - multAction)
			{
				currentAction = ACTION.LEFT_LOOKING;
			}
			else if ( rnd < .45 - multAction)
			{	
				currentAction = ACTION.SEARCHING;
			}
			else if ( rnd < .6 - multAction)
			{	
				currentAction = ACTION.LOOKING_DEEP;
			}
			
			cooldownAction = 0;
		}
		else
		{
			cooldownAction += Time.deltaTime;
		}
	}
	
	/// <summary>
	/// Actions the looking deep.
	/// </summary>
	void ActionLookingDeep()
	{
		BackAnimation();
		if( cooldownAction > .5f + multActionTimer) 
		{
			cooldownAction = 0;
			currentAction = ACTION.SEARCHING;
		}
		else 
		{
			cooldownAction += Time.deltaTime;
		}
		
		bc.SetBalaoLookingDeep();
	}
	
	/// <summary>
	/// Actions the left deep looking.
	/// </summary>
	void ActionLeftDeep()
	{
		BackAnimation();
		bc.SetBalaoLooking();
		if( cooldownAction > .5f + multActionTimer) 
		{
			cooldownAction = 0;
			currentAction = ACTION.SEARCHING;
		}
		else 
		{
			cooldownAction += Time.deltaTime;
		}
	}
	
	/// <summary>
	/// Actions the dying.
	/// </summary>
	void ActionDying()
	{
		BackAnimation();
		bc.SetBalaoDead();
		transform.Translate( Vector3.up * Time.deltaTime * 4f, Space.World );
		GetComponent<SpriteRenderer>().color = new Color(1,1,1, .5f);
		
		Invoke("Start", 1.5f);
	}
	
	/// <summary>
	/// Actions the exiting.
	/// </summary>
	void ActionExiting()
	{
		bc.SetBalaoExiting();
		transform.Translate( Vector3.right * side * Time.deltaTime * walkingSpeed * 3, Space.World );
		WalkAnimation(.1f);
	}
	
	#endregion
	
	#region SCARE
	/// <summary>
	/// Die this instance when scares right.
	/// </summary>
	public void Die()
	{
		currentAction = ACTION.DYING;
	}
	
	/// <summary>
	/// Scares goes wrong.
	/// </summary>
	public void ScareWrong()
	{
		currentAction = ACTION.EXITING;
	}
	#endregion
	
	/// <summary>
	/// Checks the multipliers.
	/// </summary>
	void CheckMultipliers()
	{
		if( GameController.instance )
		{
			if( GameController.instance.currentDificulty == GameController.DIFICULTY.EASY )
			{
				multAction = 0;
				multActionTimer = 1f;
			}
			else if( GameController.instance.currentDificulty == GameController.DIFICULTY.NORMAL )
			{
				multAction = .1f;
				multActionTimer = 0;
			}
			else if( GameController.instance.currentDificulty == GameController.DIFICULTY.HARD )
			{
				multAction = .2f;
				multActionTimer = - .2f;
			}
		}
	}
	
	void WalkAnimation(float speedWalk = .3f)
	{	
		//update side
		if( lastSide != side )
		{
			transform.localScale = new Vector3( side * 1.2f, 1.2f, 1.2f);
			lastSide = side;
		}
		
		if( animationTimer > speedWalk )
		{
			animationTimer = 0;
			indAnimatino++;
			spr.sprite = walkMan[ indAnimatino % walkMan.Length ];
			GetComponent<AudioSource>().PlayOneShot( walkSound );
		}
		else
		{
			animationTimer += Time.deltaTime;
		}
	}
	
	void BackAnimation()
	{
		if( spr.sprite != backMan )
			spr.sprite = backMan;
	}
	
	
	
	#region COLLIDERS
	
	/// <summary>
	/// Raises the trigger enter2 d event. 
	/// Check if searchomg
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter2D(Collider2D col)
	{
		if( currentAction == ACTION.SEARCHING )
		{
			if( col.CompareTag("objeto") )
			{
				if( UnityEngine.Random.Range(0f, 1f) <= actionChanceLooking )
				{
					currentAction = ACTION.LOOKING;
					actionChanceLooking = actionChanceLookingDefault;
					currentObjeto = col.gameObject;
					
					PlayerController.instance.SetCurrentAI( this );
					
					cooldownAction = 0;
				}
				else
				{
					actionChanceLooking += .1f;
				}
			}
		}
	}
	
	/// <summary>
	/// Raises the trigger exit2 d event.
	/// When enter in scene
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerExit2D( Collider2D col )
	{
		if( col.CompareTag("bound") )
		{
			if( !isEntered )
			{
				currentAction = ACTION.SEARCHING;
				isEntered = true;
			}
			else
			{
				Start();
			}
		}
	}
	
	
	#endregion
}
