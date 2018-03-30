using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatterMesh : MonoBehaviour {

	public int numSources;
	public Transform audioSourcePrefab;
	public Transform flowerPrefab;

	private Transform flower;

	// Use this for initialization
	void Start () {
		generateEquidistantSources ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void generateEquidistantSources() {
		// algo from here: https://www.cmu.edu/biolphys/deserno/pdf/sphere_equi.pdf
		float x, y, z;
		float sintheta;
		float a, d, d_theta, d_phi;
		float theta, phi;
		int M_theta, M_phi;
		int pointCount = 0;
		a = (2 * Mathf.PI) / numSources; // Area of the hemisphere divided by the number of sources
		d = Mathf.Sqrt(a);
		M_theta = (int) Mathf.Round (Mathf.PI * .5f / d);
		Debug.Log ("M_theta is " + M_theta);
		d_theta = Mathf.PI * .5f / M_theta;
		d_phi = a / d_theta;
		for (int m = 0; m < M_theta; m++) {
			theta = (Mathf.PI * .5f) *(m + .05f) / M_theta;
			M_phi = (int) Mathf.Round(2*Mathf.PI*Mathf.Sin(theta)/d_phi);
			for (int n = 0; n < M_phi; n++) {
				phi = 2*Mathf.PI * n / M_phi;
				sintheta = Mathf.Sin (theta);
				x = 9 * sintheta * Mathf.Cos (phi);
				y = 9 * sintheta * Mathf.Sin (phi);
				z = 9 * Mathf.Cos (theta)+2.32f;
				Vector3 p = new Vector3 (x, z, y);
				Transform audioSource = Instantiate(audioSourcePrefab);
				// rotate to face the centre of the ground
				audioSource.SetPositionAndRotation (p, Quaternion.LookRotation(-p));
				audioSource.transform.parent = this.gameObject.transform;

				//make a simple gillyflower
				makeFlower(p);
				flower.transform.parent = audioSource.transform;

				pointCount++;
			}
		}
		Debug.Log ("Generated a total of " + pointCount + " audio sources");
	}

	void makeFlower(Vector3 p) {
		flower = Instantiate (flowerPrefab);
		flower.transform.position = p;
		Quaternion r = Quaternion.LookRotation (Vector3.forward, -p);
		flower.transform.rotation = r;
	}
}
