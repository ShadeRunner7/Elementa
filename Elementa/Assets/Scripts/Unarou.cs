using UnityEngine;
using System.Collections;

public class Unarou : MonoBehaviour {

	protected static GameObject[] CharacterList;
	protected static GameObject[] TileList;
	protected static GameObject SelectedChar;
	protected static bool Moving = false;
	protected static Character selected;
	protected static string[] tiles = {
		"Tiles/WoodTile", "Tiles/FireTile", "Tiles/GroundTile", "Tiles/MetalTile", "Tiles/WaterTile"
	};
	protected static GameObject PlayerTile;
	static GameObject MapGene;

	// Use this for initialization
	void Start () { 
		int x = Random.Range (0, 100);
		int y = Random.Range (-(x / 2), 100 - x / 2);
		int z = -x - y;

		if (!GameObject.Find ("Emilia"))
			Instantiate (Resources.Load ("Characters/Emilia"), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = "Emilia";
		GameObject.Find ("Emilia").GetComponent<Character> ().Position (x, y, z);

		x += Random.Range (1, 2);
		y += Random.Range (1, 2);
		z = -x - y;

		if (!GameObject.Find ("Flora"))
			Instantiate (Resources.Load ("Characters/Flora"), new Vector3 (x * .75f, (y - z) * .45f, 0), transform.rotation).name = "Flora";
		GameObject.Find ("Flora").GetComponent<Character> ().Position (x, y, z);

		CharacterList = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject character in CharacterList) {
			character.GetComponent<Skills> ().SetUp ();
		}
		SelectedChar = CharacterList [0];
		selected = CharacterList [0].GetComponent<Character> ();

		if (!GameObject.Find ("MapGen"))
			MapGene = Instantiate (Resources.Load ("MapGen")) as GameObject;
		MapGene.name = "MapGen";

		MapGeneration ();

		PlayerTile = GameObject.Find (selected.x + "," + selected.y + "," + selected.z);		
		foreach (GameObject character in CharacterList) {
			character.GetComponent<Character> ().Update ();
		}

		MapGeneration (1);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	protected int Distance (int x0, int y0, int z0, int x1, int y1, int z1) {
		return Mathf.Max (Mathf.Abs (x0 - x1), Mathf.Abs (y0 - y1), Mathf.Abs (z0 - z1));
	}

	protected void MapGeneration () {		
		MapGene.GetComponent<MapGen> ().GenerateMap ();
		TileList = GameObject.FindGameObjectsWithTag ("NewTile");
		foreach (GameObject MapTile in TileList) 
			MapTile.GetComponent <Tile> ().SetUp ();
	}

	protected void MapGeneration (int xenomorph) {		
		TileList = GameObject.FindGameObjectsWithTag ("Tile");
		foreach (GameObject MapTile in TileList) 
			MapTile.GetComponent <Tile> ().VisionCheck ();
	}




}
