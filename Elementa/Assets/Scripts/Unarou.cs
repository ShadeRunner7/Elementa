using UnityEngine;
using System.Collections;

public class Unarou : MonoBehaviour {

	protected static GameObject[] CharacterList;
	protected static GameObject[] TileList;
	protected static GameObject SelectedChar;
	protected static bool Moving = false;
	protected static Character selected;
	static GameObject MapGene;

	// Use this for initialization
	void Start () { 
		if (!GameObject.Find ("Emilia"))
			Instantiate (Resources.Load ("Characters/Emilia"), new Vector3 (0, 0, 0), transform.rotation).name = "Emilia";
		if (!GameObject.Find ("Flora"))
			Instantiate (Resources.Load ("Characters/Flora"), new Vector3 (0, 2.7f, 0), transform.rotation).name = "Flora";
		CharacterList = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject character in CharacterList)
			character.GetComponent<Character> ().Position ();
		SelectedChar = CharacterList [0];
		selected = CharacterList [0].GetComponent<Character> ();

		if (!GameObject.Find ("MapGen"))
			MapGene = Instantiate (Resources.Load ("MapGen")) as GameObject;
		MapGene.name = "MapGen";

		MapGeneration ();

		foreach (GameObject character in CharacterList) 
			character.GetComponent<Character> ().FoW ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	protected int Distance (int x0, int y0, int z0, int x1, int y1, int z1) {
		return Mathf.Max (Mathf.Abs (x0 - x1), Mathf.Abs (y0 - y1), Mathf.Abs (z0 - z1));
	}

	protected void MapGeneration () {		
		MapGene.GetComponent<MapGen> ().GenerateMap ();
		TileList = GameObject.FindGameObjectsWithTag ("Tile");
		MapGene.GetComponent<HMapGen> ().GenerateHMap ();
	}




}
