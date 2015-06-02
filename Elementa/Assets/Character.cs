using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public int x = 0, y = 0, z,
			   LoS = 4,
			   MP = 2,
			   AP = 2;

	// Use this for initialization
	void Start () {
		x = Mathf.RoundToInt(transform.position.x / .75f);
		y = (int)(transform.position.y / .9f) - x / 2;
		z = -(x + y);
		Debug.Log (transform.position.y / .9f - x / 2);
		Debug.Log (x + " " + y + " " + z);

	}
	
	// Update is called once per frame
	void Update () {
	}
}
