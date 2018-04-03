using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;
	public AudioMixerGroup audioMixer;
	public static AudioManager instance;

	// Use this for initialization
	void Awake () {

		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.outputAudioMixerGroup = audioMixer;

			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
		}
	}

	void Start() {
		Play("BackgroundMusic");
	}

	public bool IsPlaying(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found");
			return true;
		}
		if (s.source.isPlaying) {
			return true;
		}
		else {
			return false;
		}
	}
	
	public void Play(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found");
			return;
		}
			
		s.source.Play();
	}
}
