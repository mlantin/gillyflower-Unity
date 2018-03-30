using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControl : MonoBehaviour {

	public float GlobalChatterInterval = 3; // In Seconds
	public bool GlobalChattering = false;

	public static ShowControl singleton;

	// Use this for initialization
	void Start () {
		singleton = this;
		GlobalChattering = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static float Interval {
		get {
			return singleton.GlobalChatterInterval;
		}
	}

	public static bool Chattering {
		get {
			return singleton.GlobalChattering;
		}
	}
}
