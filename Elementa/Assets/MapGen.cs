using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject tile = Instantiate(Resources.Load("Gridparts_0"),transform.position, transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
