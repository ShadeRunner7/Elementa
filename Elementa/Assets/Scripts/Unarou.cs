using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Unarou : MonoBehaviour {

	protected static GameObject[] CharacterList;
	protected static GameObject[] TileList;
	protected static GameObject SelectedChar;
	protected static bool Moving = false;
	protected static bool Action = false;
	protected static bool NewMap = false;
	protected static bool SweepIsRunning = false;
	protected static Character selected;
	protected static string[] tiles = {
		"Tiles/WoodTile", "Tiles/FireTile", "Tiles/GroundTile", "Tiles/MetalTile", "Tiles/WaterTile"
	};
	protected static GameObject PlayerTile;
//	static GameObject MapGene;
	protected static Sprite[] MA;
	protected static List<GameObject>[] CastAreaList;
	protected static List<GameObject> seen = new List<GameObject> ();
	public static int CAL;
	protected float DELAY = .0f;

	void Start () {
		Debug.Log ("Start");
		StartCoroutine ("WorldCreation");
	}

	IEnumerator WorldCreation () { 
		MA = Resources.LoadAll <Sprite> ("MATiles");
		Debug.Log ("Phase complete: MATile Sprites Loaded");
		yield return new WaitForSeconds (0);
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																																				//
		//  Characters  //																														//
																																				//
		//////////////////																														//
																																				//
		int x = Random.Range (0, 100);																											//
		int y = Random.Range (-(x / 2), 100 - x / 2);																							//
		int z = -x - y;																															//
																																				//
//		if (!GameObject.Find ("Emilia"))																										//
			Instantiate (Resources.Load ("Characters/Emilia"), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = "Emilia";	//
		GameObject.Find ("Emilia").GetComponent<Character> ().Position (x, y, z);																//
																																				//
		x += Random.Range (1, 2);																												//
		y += Random.Range (1, 2);																												//
		z = -x - y;																																//
																																				//
//		if (!GameObject.Find ("Flora"))																											//
			Instantiate (Resources.Load ("Characters/Flora"), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = "Flora";	//
		GameObject.Find ("Flora").GetComponent<Character> ().Position (x, y, z);																//
																																				//
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		Debug.Log ("Phase complete: Characters Created");
		yield return new WaitForSeconds (1);

		//////// CharacterList filled for first time //////////
		CharacterList = GameObject.FindGameObjectsWithTag ("Player");
		///////////////////////////////////////////////////////
		Debug.Log ("Phase complete: CharacterList created");
		yield return new WaitForSeconds (0);

		////////// Setup CastAreaList //////////
		int LC = 0; 
		foreach (GameObject c in CharacterList) {
			c.GetComponent<Skills> ().SetUp ();
			LC += c.GetComponent<Character> ().AP;
		}

		CastAreaList = new List<GameObject>[LC];
		for (CAL = 0; CAL < LC; CAL++)
			CastAreaList[CAL] = new List<GameObject> ();
		CAL = 0;
		////////////////////////////////////////
		Debug.Log ("Phase complete: CastAreaList created");
		yield return new WaitForSeconds (0);

		SelectedChar = CharacterList [0];
		selected = CharacterList [0].GetComponent<Character> ();
		Debug.Log ("Phase complete: SelectedChar created");
		yield return new WaitForSeconds (0);
		
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																																				//
		//  Map  //																																//
																																				//
		///////////																																//
																																		//
		MapGeneration (0);																														//
																																				//
		while(SweepIsRunning)
			yield return new WaitForSeconds (DELAY);
		Debug.Log ("Phase complete: Map generated");

		GameObject.Find ("Canvas/PlayerB").GetComponent<UnlimitedButtonWorks> ().SelectChar ();
		Debug.Log ("Phase complete: PlayerTile set");
		yield return new WaitForSeconds (0);

		foreach (GameObject c in CharacterList) {
			c.GetComponent<Character> ().PlayerSetUp ();
		}
		Debug.Log ("Phase complete: Players set up");
		yield return new WaitForSeconds (0);

		Debug.Log("World Creation: Complete");

//		MapGeneration (1);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	protected int Distance (int x0, int y0, int z0, int x1, int y1, int z1) {
		return Mathf.Max (Mathf.Abs (x0 - x1), Mathf.Abs (y0 - y1), Mathf.Abs (z0 - z1));
	}

	protected void MapGeneration (int xenomorph) {
		StartCoroutine ("MakeMap", xenomorph);
	}

	IEnumerator MakeMap(int xenomorph) {
		MapGen unarou = GameObject.Find ("Unarou").GetComponent<MapGen> ();
		////////// First Time //////////
		if (xenomorph == 0)
			foreach (GameObject c in CharacterList) {
				unarou.GenerateMap (60, c);
				while (SweepIsRunning)
					yield return new WaitForSeconds (DELAY);
			}
		////////////////////////////////
		////////// When Moving //////////
		else if (xenomorph != 0) {
			try {
				unarou.GenerateMap (0, SelectedChar);
			} catch {
				Debug.Log ("Fail, SelectedChar = " + SelectedChar);
			}
			while (SweepIsRunning)
				yield return new WaitForSeconds (DELAY);
		}
		if (NewMap) { // Put this into newly created tiles only? only do this if all adjes are in place?
			TileList = GameObject.FindGameObjectsWithTag ("NewTile");
			SweepIsRunning = true;
			foreach (GameObject MapTile in TileList) {
				MapTile.GetComponent <Tile> ().SetUp ();
//				yield return new WaitForSeconds (DELAY);
			}
			SweepIsRunning = false;
			NewMap = false;
			yield return new WaitForSeconds (DELAY);
		}		
		/////////////////////////////////
	}
	
/*	internal void VisionCheck (GameObject t) {
//		t.GetComponent<Tile> ().AllCheck ();
		if (t.GetComponent<Tile> ().OnLoS == -CharacterList.Length) {
			if (t.GetComponent<SpriteRenderer> ().color != Color.black)
				t.GetComponent<SpriteRenderer> ().color = Color.grey;
		} else
			t.GetComponent<SpriteRenderer> ().color = Color.white;
	}*/
}
