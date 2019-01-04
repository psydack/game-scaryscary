using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public static GameController instance;
	
	public enum DIFICULTY
	{
		EASY,
		NORMAL,
		HARD
	}
	public DIFICULTY currentDificulty = DIFICULTY.EASY;
	
	//current score
	public float score = 0;
	//total score we want reach
	float totalScore = 3000;
	
	//UI
	public Image imgScore;
	
	// Use this for initialization
	void Awake () 
	{
		if( !instance ) instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( score > 0 )
			imgScore.fillAmount = score/totalScore;
			
	}
	
	public void AddScore(bool isDead)
	{
		//update score
		int mult = currentDificulty == DIFICULTY.EASY ? 1 : currentDificulty == DIFICULTY.NORMAL ? 2 : 3;
		score += (isDead) ? 200 * mult : 100 * mult;
		print ( score );
		//update dificult
		if( score > totalScore/2 && score < totalScore-(totalScore*.2f) ) 
		{
			currentDificulty = DIFICULTY.NORMAL;
		}
		else if( score >= totalScore-(totalScore*.2f)  )
		{
			currentDificulty = DIFICULTY.HARD;
		}
		
		
		if ( score >= totalScore ) 
			SceneManager.LoadScene("GameOver");
		
		
	}
}
