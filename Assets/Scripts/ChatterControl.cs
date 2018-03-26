using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatterControl : MonoBehaviour {

	public float GlobalChatterInterval = 3; // In Seconds

	public static ChatterControl singleton;

	// Use this for initialization
	void Start () {
		singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static float Interval {
		get {
			return singleton.GlobalChatterInterval;
		}
	}
}
