using UnityEngine;
using System.Collections.Generic;

public class MusicPlayMulti : MonoBehaviour 
{
	public List<AudioClip> musicClips;

	public bool overrideVolume = false;
	[Range(0,1)]
	public float volume = 1f;

	public bool onDisableStopMusic = false;

	SoundManager soundManager;
	
	void Awake()
	{
		soundManager = SoundManager.Get();	
	}
	
	void Start()
	{
		Random.seed = System.Environment.TickCount;
		if (soundManager == null)
			Awake();
	}

	public void ChooseRandomMusic()
	{
		var numClips = musicClips.Count;
		if (numClips > 0)
		{
			soundManager.playMusic(musicClips[Mathf.FloorToInt(Random.Range(0, numClips - float.Epsilon))],
			                       getVolume());
		}
	}

	public int Count
	{
		get { return musicClips.Count; }
	}

	public bool playMusic(int index)
	{
		var result = false;
		if (index >= 0 && index < musicClips.Count)
		{
			soundManager.playMusic(musicClips[index], getVolume());
			result = true;
		}

		return result;
	}

	public float getVolume()
	{
		if (overrideVolume)
		{
			return volume;
		}
		else
		{
			return soundManager.musicVolume;
		}
	}
	
	void OnEnable ()
	{
		ChooseRandomMusic ();
	}
	
	void OnDisable()
	{
		if ( onDisableStopMusic )
		{
			soundManager.fadeOutMusic( );
		}
	}
}