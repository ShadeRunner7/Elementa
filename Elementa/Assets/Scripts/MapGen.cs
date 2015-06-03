using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGen : MonoBehaviour {

	GameObject[] cList;
	GameObject tile, tmp;
	float x, y;
	int gx = 0, gy = 0, gz = 0, cx, cy, cz, LoS;
	string[] tiles = {
		"Tiles/WoodTile", "Tiles/FireTile", "Tiles/GroundTile", "Tiles/MetalTile", "Tiles/WaterTile"
	};

	// Use this for initialization
	void Start () {
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GenerateMap () {	
		cList =  GameObject.FindGameObjectsWithTag ("Player");
//		Debug.Log ("Generating Map");
//		int round = 0;
		
		foreach (GameObject character in cList) {
			cx = character.GetComponent<Character> ().x;
			cy = character.GetComponent<Character> ().y;
			cz = character.GetComponent<Character> ().z;
			LoS = character.GetComponent<Character> ().LoS + 2;
			
			
			for (gx = cx - LoS; gx <= cx + LoS; gx++) {
				gy = -gx / 2 - (cy + LoS);
				gz = -(gx + 1) / 2 + (cy + LoS);			
				x = gx * .75f;
				for (; gy <= cy + LoS; gy++) {
					if (x >= 0) {
						y = (gy - gz) * .45f;
						tmp = GameObject.Find (gx + "," + gy + "," + gz);
						if (Distance (cx, cy, cz, gx, gy, gz) <= LoS && !tmp)
							TileGenerator (x, y, gx, gy, gz);
					} 
					gz--;
				}
			}
			character.SendMessageUpwards ("Fow", SendMessageOptions.DontRequireReceiver); 	
		}
	}

	void TileGenerator(float x, float y, int gx, int gy, int gz) {
		int wood = Random.Range (0, 100), fire = Random.Range (0, 100), ground = Random.Range (0, 100), metal = Random.Range (0, 100), water = Random.Range (0, 100);
		int leEle = Mathf.Max (wood, fire, ground, metal, water);

		if (leEle == wood) {
			tile = Instantiate (Resources.Load (tiles [0]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			tmp.wood = Random.Range(0, 1000);
			tmp.ground = Random.Range (0, 1000);
			if (Random.Range (0, 5) == 5)
				tmp.water = Random.Range (0, 400 - tmp.ground);
			else if (tmp.ground < 400) tmp.ground += 400;
			if (tmp.water <= 0) 
				tmp.water = 0;
			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + Mathf.FloorToInt (tmp.metal / 2) - 400 + tmp.water;
		}
		else if (leEle == fire) {
			tile = Instantiate (Resources.Load (tiles [1]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			tmp.fire = Random.Range (0, 500);
			tmp.ground = Random.Range (400, 1000);
			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + Mathf.RoundToInt (tmp.metal / 2) - 400 + tmp.water;
		}		
		else if (leEle == ground) {
			tile = Instantiate (Resources.Load (tiles [2]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			tmp.ground = Random.Range(400, 1400);
			if (Random.Range (0,20) == 20) {
				int k = Random.Range (0, 500);
				tmp.metal = k - k % 400;
			}
			if (Random.Range (0, 10) == 10)
				tmp.wood = Random.Range (0, Mathf.RoundToInt (tmp.ground / 10));
			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + Mathf.FloorToInt (tmp.metal / 2) - 400 + tmp.water;
		}
		else if (leEle == metal) {
			tile = Instantiate (Resources.Load (tiles [3]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			metal = Random.Range(0, 2800);
			if (metal > 400) tmp.metal = metal - metal % 400;
			else tmp.metal = metal;
			if (Random.Range(0, 100) == 100)     tmp.ground = Random.Range (600, 1000);
			else if (Random.Range (0, 20) == 20)	tmp.ground = Random.Range (300, 599);
			else if (Random.Range (0, 10) == 10) tmp.ground = Random.Range (0, 299);
			else tmp.ground = 400; 
			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + Mathf.FloorToInt (tmp.metal / 2) - 400 + tmp.water;
		}
		else if (leEle == water) {
			tile = Instantiate (Resources.Load (tiles [4]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			tmp.water = Random.Range (1, 400);
			if (Random.Range (0,20) == 20)
				tmp.wood = Random.Range(0, tmp.water);
			tmp.ground = 400 - tmp.water;
			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + Mathf.FloorToInt (tmp.metal / 2) - 400 + tmp.water;
		}
	}

	int Distance (int x0, int y0, int z0, int x1, int y1, int z1) {
		return Mathf.Max (Mathf.Abs (x0 - x1), Mathf.Abs (y0 - y1), Mathf.Abs (z0 - z1));
	}
}
