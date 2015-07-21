using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : Unarou {

	internal int wood, fire, ground, metal;	
	public int elevation, water, x, y, z;
	public int visionLevel = 0;
	public int OnLoS = 0;
	public bool NextToPlayer = false;
	public bool HasPlayer = false;
	public bool IsActioned = false;
	public int actionTurn = 0;
	public int MPC = 1, APC = 0, eMPC;
	public int EffectedPower;
	public Sprite original;
	
	public GameObject adj0, adj1, adj2, adj3, adj4, adj5;
	public GameObject adjH0, adjH1, adjH2, adjH3, adjH4, adjH5;	
	public GameObject ActionedBy;
	internal List<Tile> adje;
	static List<GameObject> ActionHexes;
	public int MCALN;	//MyCastAreaListNumber

//	int meh = 0;

	// Use this for initialization
	void Start () {	
	}

	internal void SetUp (){			
		original = GetComponent<SpriteRenderer> ().sprite;

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


		ElevationDiff (); //ADJES SET
		ProxNEffCheck ();
		
		if (adj0 && adj1 && adj2 && adj3 && adj4 && adj5)// || meh == 1)
			tag = "Tile";

		if (tag == "NewTile" || OnLoS == -CharacterList.Length)
			GetComponent<SpriteRenderer> ().color = Color.black;
		else 
			GetComponent<SpriteRenderer> ().color = Color.white;

//		meh++;
	}

	// Update is called once per frame
	void Update () {
	}

	internal void MoveCheck () {
		if (selected.AP > 0) {
			foreach (Tile hex in adje) {
				if (hex) {
					if (Moving) {
						if (hex.elevation - elevation >= 100) {
							hex.MPC = 1;
							hex.eMPC = Mathf.CeilToInt ((hex.elevation - elevation - 100) / 50 + 1); 		//HERE
							hex.APC = 1;
						} else if (hex.elevation - elevation < 100 && hex.elevation - elevation >= 50) {
							hex.MPC = Mathf.Min (2, selected.MP);											//AND HERE
							hex.eMPC = selected.eMP;
							hex.APC = 0;
						} else if (hex.elevation - elevation < 50 && hex.elevation - elevation > -100) {
							hex.MPC = 1;																	//HERE
							hex.eMPC = 0;
							hex.APC = 0;
						} else if ((hex.elevation - elevation <= -100 && hex.elevation - elevation > -150)									|| 
								   (hex.water >= Mathf.FloorToInt (elevation / 2) && hex.elevation - elevation < -100 && hex.water != 0)	) {
							hex.MPC = 0;																	//HERE
							hex.eMPC = selected.eMP;
							hex.APC = 0;
						} else {
							hex.MPC = selected.MP + 1;														//HERE
							hex.eMPC = selected.eMP + 1;
							hex.APC = selected.AP + 1;
						}
						if (!hex.HasPlayer && selected.AP >= hex.APC && selected.MP >= hex.MPC && selected.eMP >= hex.eMPC && Moving) {
							hex.GetComponent<SpriteRenderer> ().sprite = MA [0];
							if (hex.IsActioned)
								hex.GetComponent<SpriteRenderer> ().sprite = MA [1];
						} else {
							hex.GetComponent<SpriteRenderer> ().sprite = MA [2];				
						}
					}
					else 
						origins (hex, hex.IsActioned);
//				else
//					origins (hex, hex.IsActioned);
				}
			}
		}
	}

	internal void ProxNEffCheck () {
		int count = 0;
		foreach (GameObject c in CharacterList) {
			if (c.transform.position == transform.position) {
				HasPlayer = true;
				count++;
			} else if (count == 0)
				HasPlayer = false;
		}
				
		OnLoS = 0;
		foreach (GameObject player in CharacterList) {
			Character tmp0 = player.GetComponent<Character> ();
			if (Distance (tmp0.x, tmp0.y, tmp0.z, x, y, z) <= tmp0.LoS) {
				OnLoS++;
			} else
				OnLoS--;
		}

		if (actionTurn <= 0 && ActionedBy)
			effects ();
	}

	void origins (Tile a, bool b) {
		if (!b)
			a.GetComponent<SpriteRenderer> ().sprite = a.original;
		else
			a.GetComponent<SpriteRenderer> ().sprite = MA [4];
/*		if (visionLevel == 0) {
			a.GetComponent<SpriteRenderer> ().color = Color.black;
		} else if (visionLevel == 1) {
			a.GetComponent<SpriteRenderer> ().color = Color.grey;
		} else if (visionLevel == 2) {
			a.GetComponent<SpriteRenderer> ().color = Color.white;
		}
*/	}

	void ElevationDiff() {	
		adje = new List<Tile> ();
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

		if (!GameObject.Find (x + "," + y + "," + z + "." + a)) {
			if ((u150 && io100) || (oW >= Mathf.Floor (elevation / 2) && io100 && oW != 0))
				Instantiate (Resources.Load ("HTiles/H-50Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
			else if (ium50 && om100)
				Instantiate (Resources.Load ("HTiles/H-50Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
			else if (ium100)
				Instantiate (Resources.Load ("HTiles/H-100Tile" + a), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = x + "," + y + "," + z + "." + a;
		}
	}

	void OnMouseUp() {
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		//  Movement  //
		
		////////////////
		if (Moving 												&& 
			selected.MP > 0 									&& 
			selected.AP > 0										&&
			(GetComponent<SpriteRenderer> ().sprite == MA [0]	||
		 	 GetComponent<SpriteRenderer> ().sprite == MA [1]	) ) {

			foreach (Tile t in PlayerTile.GetComponent<Tile> ().adje)
				if (t)
					t.GetComponent<SpriteRenderer> ().sprite = t.original;

			SelectedChar.transform.position = transform.position;
			selected.Position (x, y, z);
			selected.ele = elevation;
			
			PlayerTile = GameObject.Find (selected.x + "," + selected.y + "," + selected.z);			
			foreach (Tile t in PlayerTile.GetComponent<Tile> ().adje)
				if (t)
					t.ProxNEffCheck ();
			PlayerTile.GetComponent<Tile> ().HasPlayer = true;

			selected.MP -= MPC;
			selected.eMP = 0;
			selected.AP -= APC;
			selected.Moved = true;

			MapGeneration (1);

//			IThinkThisIsGonnaBeABadIdea ();

			if (selected.MP == 0 && selected.AP == 0)
				Moving = false;

			if (selected.MP == 0 && selected.AP != 0) {
				selected.AP--;
				Moving = false;
			} else 
				MoveCheck ();

			SelectedChar.GetComponent<Skills> ().AddExp ((eMPC + MPC) * 1000, 0);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		//  Actions  //
		
		///////////////
/*		if (Action 												&& 
			selected.AP > 0 									&&
			selected.CAC < selected.CA							&&
		    GetComponent<SpriteRenderer> ().sprite == MA [3]	) {

			selected.CAC++;
			GetComponent<SpriteRenderer> ().sprite = MA [4];
			IsActioned = true;
			actionTurn = 1;
			ActionedBy = SelectedChar;
			selected.Did = true;
			CastAreaList[CAL].Add(GameObject.Find (name));
			MCALN = CAL;

			if (selected.CAC == selected.CA) {
				selected.AP--;
				Action = false;
				selected.Did = false;
				selected.CAC = 0;
				CAL++;
			} else {
				IThinkThisIsGonnaBeABadIdea ();
				ActionCheck ();
			}

			if (!Action)
				IThinkThisIsGonnaBeABadIdea ();
		}
*/		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		GameObject.Find ("Canvas/PlayerB").GetComponent<UnlimitedButtonWorks> ().ChangeTexts ();
	}

	void effects () {
		Character tmp = ActionedBy.GetComponent<Character> ();
		EffectedPower = (int)Mathf.Floor (tmp.LTPWR / CastAreaList[MCALN].Count);
		ActionedBy.GetComponent<Skills> ().AddExp (EffectedPower * 100, 0);
		IsActioned = false;
		ActionedBy = null;
	}
}
