using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	GameObject target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
		if (target.GetComponent<Character> ().LoS != 0)
			GetComponent<Camera> ().orthographicSize = target.GetComponent<Character> ().LoS + 1 / (float)target.GetComponent<Character> ().LoS;
	}

	public void SetTarget(GameObject a) {
		target = a;
	}
}
