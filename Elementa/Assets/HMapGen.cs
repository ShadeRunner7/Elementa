using UnityEngine;
using System.Collections;

public class HMapGen : MonoBehaviour {

	// Use this for initialization
	void Start () {	
		GameObject[] tmp = GameObject.FindGameObjectsWithTag ("Tile");
		foreach (GameObject i in tmp) {
			Debug.Log (i);
			i.GetComponent<Tile>().ElevationDiff();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
