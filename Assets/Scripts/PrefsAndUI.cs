using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefsAndUI : MonoBehaviour {

	public static string ServerIP = "";
	public static string SoundServerIP = "";

	// Use this for initialization
	void Start () {
		populateUI ();
	}
	
	public void changeServerIP(string serverIP){
		ServerIP = serverIP;
		PlayerPrefs.SetString ("ServerIP", ServerIP);
		Debug.Log ("Server IP is now " + ServerIP);
	}

	public void changeSoundServerIP(string soundServerIP) {
		SoundServerIP = soundServerIP;
		PlayerPrefs.SetString ("SoundServerIP", SoundServerIP);
	}

	void populateUI() {
		// Set Server IP
		if (PlayerPrefs.HasKey ("ServerIP"))
			ServerIP = PlayerPrefs.GetString ("ServerIP");
		else
			ServerIP = "127.0.0.1";

		InputField serverip = transform.Find ("Panel/Server/ServerField").gameObject.GetComponent<InputField> ();
		Debug.Log ("The server ip is " + ServerIP);
		serverip.text = ServerIP;

		// Set Sound Server IP
		if (PlayerPrefs.HasKey ("SoundServerIP"))
			SoundServerIP = PlayerPrefs.GetString ("SoundServerIP");
		else
			SoundServerIP = "165.227.5.178";

		InputField soundserverip = transform.Find ("Panel/SoundServer/SoundServerField").gameObject.GetComponent<InputField> ();
		soundserverip.text = SoundServerIP;
	}

	void OnApplicationQuit(){
		PlayerPrefs.Save ();
	}
}
