using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.XR;

public class NetworkSetup : MonoBehaviour {

	static int HostMode = 0;

	public GameObject domeRig;
	public GameObject gvrGroup;
	public Canvas UICanvas;
	public GameObject UICamera;

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
		UICamera.SetActive(false);
		UICanvas.enabled = false;
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
			break;
		}
	}

	void setupDome(){
		domeRig.SetActive(true);
	}

	void setupGVR() {
		gvrGroup.SetActive (true);
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
