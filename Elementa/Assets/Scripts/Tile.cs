using UnityEngine;
using System.Collections;

public class Tile : Unarou {

	internal int wood, fire, ground, metal, water;	
	public int elevation, x, y, z;
	internal int visionLevel = 0;
	
	internal GameObject adj0, adj1, adj2, adj3, adj4, adj5;

	// Use this for initialization
	void Start () {		
		adj0 = GameObject.Find (x + "," + (y + 1) + "," + (z - 1));	
		adj1 = GameObject.Find ((x + 1) + "," + y + "," + (z - 1));	
		adj2 = GameObject.Find ((x + 1) + "," + (y - 1) + "," + z);	
		adj3 = GameObject.Find (x + "," + (y - 1) + "," + (z + 1));	
		adj4 = GameObject.Find ((x - 1) + "," + y + "," + (z + 1));	
		adj5 = GameObject.Find ((x - 1) + "," + (y + 1) + "," + z);
	}

	// Update is called once per frame
	void Update () {
		if (visionLevel == 0) {
			GetComponent<SpriteRenderer> ().color = Color.black;
		} else if (visionLevel == 1) {
			GetComponent<SpriteRenderer> ().color = Color.grey;
		} else if (visionLevel == 2) {
			GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}

}
