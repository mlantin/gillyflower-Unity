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
		Debug.Log ("Going to call the command to start animation");
		NetworkPlayer.LocalPlayer.performerActions.CmdStartAnimation();
	}

	void OnGUI() {
		Event e = Event.current;
		if (e.isKey && e.keyCode == KeyCode.Space && e.type == EventType.KeyDown)
			playAnimation ();
	}
}
