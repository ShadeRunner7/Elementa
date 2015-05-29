using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public int x, y, z,
			   wood, fire, ground, metal, water,
			   elevation;

	// Use this for initialization
	void Start () {
		elevation = ground + Mathf.RoundToInt(metal / 2) - water;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
