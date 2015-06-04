using UnityEngine;
using System.Collections;

public class Unarou : MonoBehaviour {

	protected static GameObject[] CharacterList;
	protected static GameObject[] TileList;
	GameObject MapGen;

	// Use this for initialization
	void Start () {
		if (!GameObject.Find ("Emilia"))
			Instantiate (Resources.Load ("Characters/Emilia"), new Vector3 (0, 0, 0), transform.rotation).name = "Emilia";
		if (!GameObject.Find ("Flora"))
			Instantiate (Resources.Load ("Characters/Flora"), new Vector3 (0, 2.7f, 0), transform.rotation).name = "Flora";
		CharacterList = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject character in CharacterList)
			character.GetComponent<Character> ().Position ();

		if (!GameObject.Find ("MapGen"))
			MapGen = Instantiate (Resources.Load ("MapGen")) as GameObject;
		MapGen.name = "MapGen";
		MapGen.GetComponent<MapGen> ().GenerateMap ();
		TileList = GameObject.FindGameObjectsWithTag ("FoWTile");
		MapGen.GetComponent<HMapGen> ().GenerateHMap ();

		foreach (GameObject character in CharacterList) 
			character.GetComponent<Character> ().FoW ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	protected int Distance (int x0, int y0, int z0, int x1, int y1, int z1) {
		return Mathf.Max (Mathf.Abs (x0 - x1), Mathf.Abs (y0 - y1), Mathf.Abs (z0 - z1));
	}


}
