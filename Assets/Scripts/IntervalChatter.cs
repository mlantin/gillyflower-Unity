using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalChatter : MonoBehaviour {
	public static string[] chatterSounds = {
		"Sounds/A2_pluck_A","Sounds/A2_pluck_B","Sounds/A2_pluck_C","Sounds/A2_pluck_D",
		"Sounds/A2_pluck_E","Sounds/A2_pluck_F","Sounds/A2_pluck_G","Sounds/A2_pluck_H",
		"Sounds/A2_pluck_J","Sounds/A3_pluck_A","Sounds/A3_pluck_B","Sounds/A3_pluck_C",
		"Sounds/A3_pluck_D","Sounds/A3_pluck_E","Sounds/A3_pluck_F","Sounds/A3_pluck_G",
		"Sounds/A3_pluck_H","Sounds/A3_pluck_J","Sounds/A3_pluck_K","Sounds/A3_pluck_L",
		"Sounds/A3_pluck_M","Sounds/A3_pluck_N","Sounds/A3_pluck_P","Sounds/A3_pluck_Q",
		"Sounds/A3_pluck_R","Sounds/D2_pluck_A","Sounds/D2_pluck_B","Sounds/D2_pluck_C",
		"Sounds/D2_pluck_D","Sounds/D2_pluck_E","Sounds/D2_pluck_F","Sounds/D2_pluck_G",
		"Sounds/D2_pluck_H","Sounds/D2_pluck_J","Sounds/D2_pluck_K","Sounds/D2_pluck_L",
		"Sounds/D2_pluck_M","Sounds/D2_pluck_N","Sounds/D2_pluck_P","Sounds/D3_pluck_A",
		"Sounds/D3_pluck_B","Sounds/D3_pluck_C","Sounds/D3_pluck_D","Sounds/D3_pluck_E",
		"Sounds/D3_pluck_F","Sounds/D3_pluck_G","Sounds/D4_pluck_A","Sounds/D4_pluck_B",
		"Sounds/D4_pluck_C","Sounds/D4_pluck_D","Sounds/D4_pluck_E","Sounds/D4_pluck_F",
		"Sounds/D4_pluck_G","Sounds/D4_pluck_H","Sounds/D4_pluck_J","Sounds/D4_pluck_K",
		"Sounds/D4_pluck_L","Sounds/F#_pluck_A","Sounds/F#_pluck_B","Sounds/F#_pluck_C",
		"Sounds/F#_pluck_D","Sounds/F#_pluck_E","Sounds/F#_pluck_F","Sounds/F#_pluck_G"
	};

	private AudioSource chatterSource;
	private float delay;
	private bool firstplay = true;
	private bool scheduled = false;

	private Animator animator;

	// Use this for initialization
	void Start () {
		// Pick a random sound
		int soundidx = Mathf.FloorToInt(Random.value*(chatterSounds.GetLength(0)-1));
		AudioClip chatterclip = Resources.Load (chatterSounds [soundidx]) as AudioClip;
		chatterSource = gameObject.GetComponent<ResonanceAudioSource> ().audioSource;
		chatterSource.clip = chatterclip;

		// Check to see if we have an animator
		animator = gameObject.GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (ShowControl.Chattering == false)
			return;
		if (!scheduled && !chatterSource.isPlaying) {
			if (firstplay) {
				delay = Random.value * 10;
				firstplay = false;
			}
			else {
				delay = ShowControl.Interval;
				setFlowerState (false);
			}
			StartCoroutine ("chatterAway");
		} else if (scheduled && chatterSource.isPlaying) {
			setFlowerState (true);
			scheduled = false;
		}
	}

	IEnumerator chatterAway() {
		scheduled = true;
		yield return new WaitForSeconds (delay);
		chatterSource.Play ();
	}

	void setFlowerState(bool openstate) {
		if (animator != null)
			animator.SetBool ("open", openstate);
	}
}
