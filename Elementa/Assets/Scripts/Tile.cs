using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : Unarou {

	internal int wood, fire, ground, metal, water;	
	public int elevation, x, y, z;
	internal int visionLevel = 0;
	internal bool OnLoS = false;
	
	public GameObject adj0, adj1, adj2, adj3, adj4, adj5;
	public GameObject adjH0, adjH1, adjH2, adjH3, adjH4, adjH5;

	// Use this for initialization
	void Start () {		
	}

	internal void SetUp (){		
		if (!adj0) adj0 = GameObject.Find (x + "," + (y + 1) + "," + (z - 1));
		if (!adj1) adj1 = GameObject.Find ((x + 1) + "," + y + "," + (z - 1));	
		if (!adj2) adj2 = GameObject.Find ((x + 1) + "," + (y - 1) + "," + z);	
		if (!adj3) adj3 = GameObject.Find (x + "," + (y - 1) + "," + (z + 1));	
		if (!adj4) adj4 = GameObject.Find ((x - 1) + "," + y + "," + (z + 1));	
		if (!adj5) adj5 = GameObject.Find ((x - 1) + "," + (y + 1) + "," + z);

		if (!adjH0) adjH0 = GameObject.Find (x + "," + y + "," + z + ".0");	
		if (!adjH1) adjH1 = GameObject.Find (x + "," + y + "," + z + ".1");	
		if (!adjH2) adjH2 = GameObject.Find (x + "," + y + "," + z + ".2");	
		if (!adjH3) adjH3 = GameObject.Find (x + "," + y + "," + z + ".3");	
		if (!adjH4) adjH4 = GameObject.Find (x + "," + y + "," + z + ".4");	
		if (!adjH5) adjH5 = GameObject.Find (x + "," + y + "," + z + ".5");

		ElevationDiff ();
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

	void ElevationDiff() {		
		List<Tile> adje = new List<Tile> ();
		if (adj0) adje.Add (adj0.GetComponent<Tile> ());
		else adje.Add (null);
		if (adj1) adje.Add (adj1.GetComponent<Tile> ());
		else adje.Add (null);
		if (adj2) adje.Add (adj2.GetComponent<Tile> ());
		else adje.Add (null);
		if (adj3) adje.Add (adj3.GetComponent<Tile> ());
		else adje.Add (null);
		if (adj4) adje.Add (adj4.GetComponent<Tile> ());
		else adje.Add (null);
		if (adj5) adje.Add (adj5.GetComponent<Tile> ());
		else adje.Add (null);
		
		List<GameObject> adjeH = new List<GameObject> ();		
		if (adjH0) adjeH.Add (adjH0);
		else adjeH.Add (null);
		if (adjH1) adjeH.Add (adjH1);
		else adjeH.Add (null);
		if (adjH2) adjeH.Add (adjH2);
		else adjeH.Add (null);
		if (adjH3) adjeH.Add (adjH3);
		else adjeH.Add (null);
		if (adjH4) adjeH.Add (adjH4);
		else adjeH.Add (null);
		if (adjH5) adjeH.Add (adjH5);
		else adjeH.Add (null);
		
		for (int c = 0; c < adje.Count; c++) {
			if(adje[c] && !adjeH[c])	{
				Diff(adje[c].elevation, adje[c].water, c);
			}
		}
	}

	void Diff(int oEle, int oW, int a) {
		if ((elevation - oEle < 150 && elevation - oEle >= 100) || ((oW >= (Mathf.FloorToInt (elevation / 2)) && elevation >= 100)))
			Instantiate (Resources.Load ("HTiles/H-50Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
		else if (elevation - oEle <= -50 && elevation - oEle > -100)
			Instantiate (Resources.Load ("HTiles/H-50Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
		else if (elevation - oEle <= -100)
			Instantiate (Resources.Load ("HTiles/H-100Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
	}

	void OnMouseUp() {
		if (Moving && 
		    selected.MP > 0 && 
		    selected.AP > 0 && 
		    Distance (selected.x, selected.y, selected.z, x, y, z) == 1) 
		{
			SelectedChar.transform.position = transform.position;

			selected.x = x;
			selected.y = y;
			selected.z = z;

			selected.MP--;
			if (selected.MP == 0)
				selected.AP--;
			if (selected.MP == 0)
				Moving = false;
//			MapGeneration ();
			selected.FoW ();
			SelectedChar.GetComponent<Skills> ().AddExp ();
			GameObject.Find ("Canvas/PlayerB").GetComponent<UnlimitedButtonWorks> ().ChangeTexts ();
		}
	}
}
