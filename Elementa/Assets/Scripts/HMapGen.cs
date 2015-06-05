using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HMapGen : Unarou {
	
	GameObject adj0, adj1, adj2, adj3, adj4, adj5;

	int x, y, z, elevation;

	// Use this for initialization
	void Start () {	
	}

	internal void GenerateHMap() {
		foreach (GameObject i in TileList) {
			x = i.GetComponent<Tile> ().x;
			y = i.GetComponent<Tile> ().y;
			z = i.GetComponent<Tile> ().z;
			ElevationDiff(i);
		}
	}

	void ElevationDiff(GameObject a) {
		elevation = a.GetComponent<Tile> ().elevation;
		
		adj0 = GameObject.Find (x + "," + (y + 1) + "," + (z - 1));	
		adj1 = GameObject.Find ((x + 1) + "," + y + "," + (z - 1));	
		adj2 = GameObject.Find ((x + 1) + "," + (y - 1) + "," + z);	
		adj3 = GameObject.Find (x + "," + (y - 1) + "," + (z + 1));	
		adj4 = GameObject.Find ((x - 1) + "," + y + "," + (z + 1));	
		adj5 = GameObject.Find ((x - 1) + "," + (y + 1) + "," + z);
		
		List<Tile> adje = new List<Tile> ();

		if (adj0) {
			adje.Add (adj0.GetComponent<Tile> ());
		} else
			adje.Add (null);
		if (adj1) {
			adje.Add (adj1.GetComponent<Tile> ());
		} else
			adje.Add (null);
		if (adj2) {
			adje.Add (adj2.GetComponent<Tile> ());
		} else
			adje.Add (null);
		if (adj3) {
			adje.Add (adj3.GetComponent<Tile> ());
		} else
			adje.Add (null);
		if (adj4) {
			adje.Add (adj4.GetComponent<Tile> ());
		} else
			adje.Add (null);
		if (adj5) {
			adje.Add (adj5.GetComponent<Tile> ());
		} else
			adje.Add (null);
		
		for (int c = 0; c < adje.Count; c++) {
			if(adje[c])	{
				Diff(adje[c].elevation, adje[c].water, c, x < 0);
			}
		}
	}

	void Diff(int oEle, int oW, int a, bool negX) {
		if (!negX) {
			if ((elevation - oEle < 150 && elevation - oEle >= 100) || ((oW >= (Mathf.FloorToInt (elevation / 2)) && elevation >= 100)))
				Instantiate (Resources.Load ("HTiles/H-50Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation);
			else if (elevation - oEle <= -50 && elevation - oEle > -100)
				Instantiate (Resources.Load ("HTiles/H-50Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation);
			else if (elevation - oEle <= -100)
				Instantiate (Resources.Load ("HTiles/H-100Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
