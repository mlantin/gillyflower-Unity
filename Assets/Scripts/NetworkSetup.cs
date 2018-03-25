using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.XR;

public class NetworkSetup : MonoBehaviour {

	public static int HostMode = 0;

	public GameObject domeRig;
	public GameObject domeObj;
	public GameObject gvrGroup;
	public Material gvrVideoMat;
	public Canvas UICanvas;
	public GameObject UICamera;


	Material[] domeMats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startNetwork(int hostmode) {
		// Modes:
		// 1: Dome Host
		// 2: Performer Host (no dome)
		// 3: Performer Client (dome is host)

		HostMode = hostmode;
		NetworkManager.singleton.networkAddress = PrefsAndUI.ServerIP;
		UICanvas.gameObject.SetActive (false);
		switch (hostmode) {
		case 1: // Dome Host
			setupDome ();
			NetworkManager.singleton.StartHost ();
			break;
		case 2: // Performer Host (standalone performer)
			StartCoroutine(switchToVR());
			NetworkManager.singleton.StartHost ();
			break;
		case 3:
			StartCoroutine (switchToVR ());
			NetworkManager.singleton.StartClient ();
			break;
		}
	}

	void setupDome(){
		UICamera.SetActive(false);
		domeRig.SetActive(true);
		// Enable the Unity Video Texture Player
		domeObj.GetComponentInChildren<VideoPlayer>().enabled = true;
	}

	void setupGVR() {
		gvrGroup.SetActive (true);

		#if UNITY_ANDROID && !UNITY_EDITOR
		// Assign a different material
		domeMats = domeObj.GetComponent<Renderer>().materials;
		domeMats [0] = gvrVideoMat;
		domeObj.GetComponent<Renderer>().materials = domeMats;
		// Enable the gvr video texture player to play the file jar:file://${Application.dataPath}!/assets/animaticpoem_1.mp4
		GvrVideoPlayerTexture gvrplayer = domeObj.GetComponentInChildren<GvrVideoPlayerTexture>();
		gvrplayer.enabled = true;
		gvrplayer.Init ();
		#else
		// Enable the Unity Video Texture Player
		domeObj.GetComponentInChildren<VideoPlayer>().enabled = true;
		#endif
	}

	// Call via `StartCoroutine(SwitchToVR())` from your code. Or, use
	// `yield SwitchToVR()` if calling from inside another coroutine.
	IEnumerator switchToVR() {
		// Device names are lowercase, as returned by `XRSettings.supportedDevices`.
		string desiredDevice = "daydream"; // Or "cardboard".

		XRSettings.LoadDeviceByName(desiredDevice);

		// Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
		yield return null;

		// Now it's ok to enable VR mode.
		XRSettings.enabled = true;
		setupGVR ();
	}
}
