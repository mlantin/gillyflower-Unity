using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine.Networking;

public class PerfomerActions : NetworkBehaviour {

	[SyncVar (hook = "playAnimHook")]
	bool playAnim = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Command]
	public void CmdStartAnimation() {
		Debug.Log ("in CmdStartAnimation");
		playAnim = true;
	}

	public void playAnimHook(bool state) {
		playAnim = state;
		GameObject domeObj = GameObject.Find ("Dome");
		#if UNITY_ANDROID && !UNITY_EDITOR
		domeObj.GetComponent<GvrVideoPlayerTexture> ().Play ();
		#else
		domeObj.GetComponent<VideoPlayer>().Play();
        #endif
        PlayableDirector director = domeObj.GetComponent<PlayableDirector>();
		director.Play();
	}
}
