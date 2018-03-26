using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
	public Transform controllerPrefab;
	public PerfomerActions performerActions;

	public static NetworkPlayer LocalPlayer = null;

	// Use this for initialization
	void Start () {
		Debug.Log ("In Start NetworkPlayer");
		if (!isLocalPlayer) {
			gameObject.tag = "RemotePlayer";
			// Disable the camera
			GameObject playercameraObj = gameObject.transform.Find ("PlayerCamera").gameObject;
			Camera playercamera = playercameraObj.GetComponent<Camera>();
			playercamera.enabled = false;
		}
		if (NetworkSetup.HostMode == 1) { // We are the dome
			// Make ourselves invisible and disable the camera
			gameObject.transform.Find ("PlayerCamera/Avatar").gameObject.SetActive (false);
			gameObject.transform.Find ("PlayerCamera").GetComponent<Camera> ().enabled = false;
		}
	}

	public override void OnStartLocalPlayer() {
		Debug.Log ("In StartLocalPlayer for NetworkPlayer");
		// tag the local player so we can look for it later in other objects
		gameObject.tag = "Player";
		LocalPlayer = this;

		if (NetworkSetup.HostMode == 1) // we're the dome so don't change anything
			return;
		
		//Make sure the MainCamera is the one for our local player
		Camera currentMainCamera = Camera.main;
		GameObject playerCameraObj = gameObject.transform.Find ("PlayerCamera").gameObject;
		Camera playercamera = playerCameraObj.GetComponent<Camera> ();
		playercamera.tag = "MainCamera";
		if (currentMainCamera != null)
			currentMainCamera.enabled = false;
		playercamera.enabled = true;

		// Add the Resonance Audio Listener
		playerCameraObj.AddComponent<ResonanceAudioListener>();

		// Add the GVR Physics Raycaster
		playerCameraObj.AddComponent<GvrPointerPhysicsRaycaster>();

		// Add an audio listener
		playercamera.gameObject.AddComponent<AudioListener>();

		// Add a GVR Controller
		Transform controller = Instantiate(controllerPrefab);
		controller.transform.parent = gameObject.transform;
		controller.transform.localPosition = Vector3.zero;
		GvrBasePointer pointer =
			controller.GetComponentInChildren<DaydreamElements.ObjectManipulation.ObjectManipulationPointer> (true);
		if (pointer != null) {
			Debug.Log ("Setting the pointer");
			GvrPointerInputModule.Pointer = pointer;	
		}
	}

	[Command]
	public void CmdAssignAuthority(NetworkInstanceId netInstanceId)
	{
		// Assign authority of this objects network instance id to the client
		bool success = false;
		GameObject obj = NetworkServer.objects [netInstanceId].gameObject;
		// Debug.Log ("Assign authority for " + netId);
		success = NetworkServer.objects [netInstanceId].AssignClientAuthority (connectionToClient);

		if (success)
			Debug.Log ("Successfully assigned authority to " + netInstanceId);
		else
			Debug.Log ("could not assign authority");
	}

	[Command]
	public void CmdRemoveAuthority(NetworkInstanceId netInstanceId)
	{
		// Removes the  authority of this object network instance id to the client
		bool success = NetworkServer.objects[netInstanceId].RemoveClientAuthority(connectionToClient);
		if (success)
			Debug.Log ("Successfully removed authority of object" + netInstanceId + "from " + connectionToClient);
	}


	// Update is called once per frame
	void Update () {
		
	}
}
