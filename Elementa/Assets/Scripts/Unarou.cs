using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unarou : MonoBehaviour {

	protected static GameObject[] CharacterList;
	protected static GameObject[] TileList;
	protected static GameObject SelectedChar;
	protected static bool Moving = false;
	protected static bool Action = false;
	protected static bool NewMap = false;
	protected static Character selected;
	protected static string[] tiles = {
		"Tiles/WoodTile", "Tiles/FireTile", "Tiles/GroundTile", "Tiles/MetalTile", "Tiles/WaterTile"
	};
	protected static GameObject PlayerTile;
	static GameObject MapGene;
	protected static Sprite[] MA;
	protected static List<GameObject>[] CastAreaList;
	protected static List<GameObject> seen = new List<GameObject> ();
	public static int CAL;

	// Use this for initialization
	void Start () { 
		MA = Resources.LoadAll <Sprite> ("MATiles");
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		//  Characters  //

		//////////////////
		int x = Random.Range (0, 100);
		int y = Random.Range (-(x / 2), 100 - x / 2);
		int z = -x - y;

//		if (!GameObject.Find ("Emilia"))
			Instantiate (Resources.Load ("Characters/Emilia"), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = "Emilia";
		GameObject.Find ("Emilia").GetComponent<Character> ().Position (x, y, z);

		x += Random.Range (1, 2);
		y += Random.Range (1, 2);
		z = -x - y;

//		if (!GameObject.Find ("Flora"))
			Instantiate (Resources.Load ("Characters/Flora"), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = "Flora";
		GameObject.Find ("Flora").GetComponent<Character> ().Position (x, y, z);

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		while (CharacterList == null)
			CharacterList = GameObject.FindGameObjectsWithTag ("Player");
		
//		Debug.Log (CharacterList == null);
		
		int LC = 0; 
		foreach (GameObject c in CharacterList) {
			c.GetComponent<Skills> ().SetUp ();
			LC += c.GetComponent<Character> ().AP;
		}

		CastAreaList = new List<GameObject>[LC];
		for (CAL = 0; CAL < LC; CAL++)
			CastAreaList[CAL] = new List<GameObject> ();
		
		CAL = 0;

		SelectedChar = CharacterList [0];
		selected = CharacterList [0].GetComponent<Character> ();


		
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		//  Map  //
		
		///////////
//		if (!GameObject.Find ("MapGen"))
			MapGene = Instantiate (Resources.Load ("MapGen")) as GameObject;
		MapGene.name = "MapGen";

		MapGeneration (0);
		
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		PlayerTile = GameObject.Find (selected.x + "," + selected.y + "," + selected.z);
//		Debug.Log (PlayerTile);

		foreach (GameObject c in CharacterList) {
			c.GetComponent<Character> ().PlayerSetUp ();
		}

		MapGeneration (1);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	protected int Distance (int x0, int y0, int z0, int x1, int y1, int z1) {
		return Mathf.Max (Mathf.Abs (x0 - x1), Mathf.Abs (y0 - y1), Mathf.Abs (z0 - z1));
	}

	protected void MapGeneration (int xenomorph) {
		if (xenomorph == 0)
			foreach (GameObject c in CharacterList)
				MapGene.GetComponent<MapGen> ().GenerateMap (60, c);
		else
			MapGene.GetComponent<MapGen> ().GenerateMap (0, SelectedChar);
//			int count = 0;
		if (NewMap) {
			TileList = GameObject.FindGameObjectsWithTag ("NewTile");
			foreach (GameObject MapTile in TileList) {
				MapTile.GetComponent <Tile> ().SetUp ();
//					count++;
			}
//			TileList = GameObject.FindGameObjectsWithTag ("Tile");
			NewMap = false;
//				Debug.Log (count);
		}

//		if (xenomorph == 1 || xenomorph == 3) {
//			foreach (GameObject MapTile in TileList) {
//				MapTile.GetComponent <Tile> ().AllCheck ();
//				if (MapTile.GetComponent<Tile> ().OnLoS == -CharacterList.Length) {
//					if (xenomorph == 1)
//						MapTile.GetComponent<SpriteRenderer> ().color = Color.black;
//					else
//						VisionCheck (MapTile);
//				}
//				MapTile.GetComponent <Tile> ().VisionCheck ();
//			}
//		}
	}
	
	internal void VisionCheck (GameObject t) {
		t.GetComponent<Tile> ().AllCheck ();
		if (t.GetComponent<Tile> ().OnLoS == -CharacterList.Length) {
			if (t.GetComponent<SpriteRenderer> ().color != Color.black)
				t.GetComponent<SpriteRenderer> ().color = Color.grey;
		} else
			t.GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
