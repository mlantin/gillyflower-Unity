using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AnimControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playAnimation() {
		#if UNITY_ANDROID && !UNITY_EDITOR
		GetComponent<GvrVideoPlayerTexture> ().Play ();
		#else
		GetComponent<VideoPlayer>().Play();
		#endif
	}

	void OnGUI() {
		Event e = Event.current;
		if (e.isKey && e.keyCode == KeyCode.Space)
			playAnimation ();
	}
}
