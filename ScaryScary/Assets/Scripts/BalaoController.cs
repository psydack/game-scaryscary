using UnityEngine;
using System.Collections;

public class BalaoController : MonoBehaviour {
	
	GameObject currentBalao;
	
//	public GameObject balaoDead;
	public GameObject balaoExiting;
	public GameObject balaoLooking;
	public GameObject balaoLookingDeep;
	public GameObject balaoSearching;
	
	void SetBalao(GameObject newBalao)
	{
		if( newBalao == currentBalao ) return;
		if( currentBalao ) currentBalao.SetActive( false );
		newBalao.SetActive( true );
		currentBalao = newBalao;
	}
	
	public void SetBalaoDead() { ResetBaloes(); }
	public void SetBalaoExiting() { SetBalao( balaoExiting ); }
	public void SetBalaoLooking() { SetBalao( balaoLooking ); }
	public void SetBalaoLookingDeep() { SetBalao( balaoLookingDeep ); }
	public void SetBalaoSearching() { SetBalao( balaoSearching ); }
	public void ResetBaloes() 
	{
		if( currentBalao != null )
		{
			currentBalao.SetActive( false );
			currentBalao = null;
		}
	}
}
