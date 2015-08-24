using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour {
	
	public AudioClip scaring;
	public AudioClip scaringWrong;
	public AudioClip scaringRight;
	
	AudioSource audioSource;
	
	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	void PlayAudio(AudioClip _audio)
	{
		audioSource.PlayOneShot( _audio );
	}
	
	public void PlayAudioScaring() { PlayAudio(scaring); }
	public void PlayAudioScaringWrong() { PlayAudio(scaringWrong); }
	public void PlayAudioScaringRight() { PlayAudio(scaringRight); }
	
}
