using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : Unarou {

	internal int wood, fire, ground, metal;	
	public int elevation, water, x, y, z;
	public int visionLevel = 0;
	public int OnLoS = 0;
	public bool seen = false;
	public bool NextToPlayer = false;
	public bool HasPlayer = false;
	public int MPC = 1;
	
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

		if (x == 0 || y == -(x / 2)) {
			if (!adj2 && transform.position.y == 0) adj2 = new GameObject();
			if (!adj3) adj3 = new GameObject();
			if (!adj4) adj4 = new GameObject();
			if (!adj5) adj5 = new GameObject();
		}

		ElevationDiff ();
		Update ();
	}

	// Update is called once per frame
	void Update () {
		if (adj0 == PlayerTile ||
		    adj1 == PlayerTile ||
		    adj2 == PlayerTile ||
		    adj3 == PlayerTile ||
		    adj4 == PlayerTile ||
		    adj5 == PlayerTile)
			NextToPlayer = true;
		else NextToPlayer = false;
		int count = 0;
		foreach (GameObject c in CharacterList) {
			if (c.transform.position == transform.position) {
				HasPlayer = true;
				count++;
			} else if (count == 0)
				HasPlayer = false;
		}

		if (!(NextToPlayer && Moving)) {
			if (visionLevel == 0 && !seen) {
				GetComponent<SpriteRenderer> ().color = Color.black;
			} else if (visionLevel == 1 || OnLoS == -CharacterList.Length) {
				GetComponent<SpriteRenderer> ().color = Color.grey;
			} else if (visionLevel == 2 || name == PlayerTile.name) {
				GetComponent<SpriteRenderer> ().color = Color.white;
			}
		} else { 
			int pEle = PlayerTile.GetComponent<Tile> ().elevation;
/*			if (elevation - pEle >= 200 + 50 * (Mathf.Max (selected.LoS, 4) - 4) && !seen) {
				GetComponent<SpriteRenderer> ().color = Color.black;
				MPC = selected.MaxMP + Mathf.FloorToInt((elevation - 100) / 50);
			} else 
*/			if (elevation - pEle >= 100 /* && seen */) {
				GetComponent<SpriteRenderer> ().color = Color.black;
				MPC = selected.MaxMP + Mathf.FloorToInt((elevation - 100) / 50);
			} else if (elevation - pEle < 100 && elevation - pEle >= 50)
				MPC = Mathf.Min(2, selected.MP);
			else if (elevation - pEle < 50 && elevation -pEle > -100)
				MPC = 1;
			else if ((elevation - pEle <= -100 && elevation - pEle > -150) || (water >= Mathf.FloorToInt(pEle / 2) && elevation - pEle < -100 && water != 0))
				MPC = 0;
			else MPC = selected.MP + 1;
			if (MPC <= selected.MP && !HasPlayer)
				GetComponent<SpriteRenderer> ().color = Color.green;

		}


		OnLoS = 0;
		foreach (GameObject player in CharacterList) {
			Character tmp0 = player.GetComponent<Character> ();
			if (Distance(tmp0.x, tmp0.y, tmp0.z, x, y, z) <= tmp0.LoS) {
				OnLoS++;
			}
			else OnLoS--;
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
		
		for (int c = 0; c < adje.Count; c++) {
			if(adje[c])	{
				Diff(adje[c].elevation, adje[c].water, c);
			}
		}		
		
		if (!adjH0) adjH0 = GameObject.Find (x + "," + y + "," + z + ".0");	
		if (!adjH1) adjH1 = GameObject.Find (x + "," + y + "," + z + ".1");	
		if (!adjH2) adjH2 = GameObject.Find (x + "," + y + "," + z + ".2");	
		if (!adjH3) adjH3 = GameObject.Find (x + "," + y + "," + z + ".3");	
		if (!adjH4) adjH4 = GameObject.Find (x + "," + y + "," + z + ".4");	
		if (!adjH5) adjH5 = GameObject.Find (x + "," + y + "," + z + ".5");
	}

	void Diff(int oEle, int oW, int a) {
		bool u150 = elevation - oEle < 150;			//under
		bool io100 = elevation - oEle >= 100;		//is over
		bool ium50 = elevation - oEle <= -50;		//is under minus
		bool om100 = elevation - oEle > -100;		//over minus
		bool ium100 = elevation - oEle <= -100;		//is under minus

		if ((u150 && io100) || (oW >= Mathf.Floor (elevation / 2) && io100 && oW != 0))
			Instantiate (Resources.Load ("HTiles/H-50Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
		else if (ium50 && om100)
			Instantiate (Resources.Load ("HTiles/H-50Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
		else if (ium100)
			Instantiate (Resources.Load ("HTiles/H-100Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
	}

	void OnMouseUp() {
		if (Moving && 
		    selected.MP > 0 && 
		    selected.AP > 0 && 
		    Distance (selected.x, selected.y, selected.z, x, y, z) == 1
		    && GetComponent<SpriteRenderer> ().color == Color.green)
		{
			SelectedChar.transform.position = transform.position;

			selected.x = x;
			selected.y = y;
			selected.z = z;
			
			PlayerTile = GameObject.Find (selected.x + "," + selected.y + "," + selected.z);

			if (MPC > selected.MaxMP)
				selected.MP = 0;
			else if (selected.MP > selected.MaxMP) selected.MP = selected.MaxMP - MPC;
			else selected.MP -= MPC;
			if (selected.MP == 0) {
				Moving = false;
				selected.AP--;
			}
			MapGeneration ();
			selected.FoW ();
			SelectedChar.GetComponent<Skills> ().AddExp (100);
			GameObject.Find ("Canvas/PlayerB").GetComponent<UnlimitedButtonWorks> ().ChangeTexts ();
		}
	}
}
