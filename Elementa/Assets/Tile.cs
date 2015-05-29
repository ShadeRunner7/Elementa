using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public int x, y, z,
			   wood, fire, ground, metal, water,
			   elevation;

	public GameObject adj0, adj1, adj2, adj3, adj4, adj5;
	GameObject hTile;

	// Use this for initialization
	void Start () {
		elevation = ground + Mathf.RoundToInt(metal / 2) - 400 + water;

		adj0 = GameObject.Find (x + "," + (y + 1) + "," + (z - 1));	
		adj1 = GameObject.Find ((x + 1) + "," + y + "," + (z - 1));	
		adj2 = GameObject.Find ((x + 1) + "," + (y - 1) + "," + z);	
		adj3 = GameObject.Find (x + "," + (y - 1) + "," + (z + 1));	
		adj4 = GameObject.Find ((x - 1) + "," + y + "," + (z + 1));	
		adj5 = GameObject.Find ((x - 1) + "," + (y + 1) + "," + z);

		ElevationDiff ();
	}

	void ElevationDiff() {
		List<Tile> adje = new List<Tile> ();
		adje.Add (adj0.GetComponent<Tile> ());
		adje.Add (adj1.GetComponent<Tile> ());
		adje.Add (adj2.GetComponent<Tile> ());
		adje.Add (adj3.GetComponent<Tile> ());		
		adje.Add (adj4.GetComponent<Tile> ());
		adje.Add (adj5.GetComponent<Tile> ());

		/*
		int c = 0;
		foreach (Tile a in adje) {
			Diff(adje[c].elevation, adje[c].water, c);
			c++;
		}
		*/

		for (int c = 0; c < adje.Count; c++) {
			Diff(adje[c].elevation, adje[c].water, c);
		}

	}

	void Diff(int oEle, int oW, int a) {
		if (elevation - oEle <= -100) {
			hTile = Instantiate (Resources.Load ("HTiles/H-100Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation) as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
