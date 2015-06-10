using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGen : Unarou {

	GameObject tile, tmp;
	float x, y;
	int gx = 0, gy = 0, gz = 0, cx, cy, cz, LoS;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	internal void GenerateMap () {		
		foreach (GameObject character in CharacterList) {
			cx = character.GetComponent<Character> ().x;
			cy = character.GetComponent<Character> ().y;
			cz = character.GetComponent<Character> ().z;
			LoS = character.GetComponent<Character> ().LoS + 1;			
			
			for (gx = cx - LoS; gx <= cx + LoS; gx++) {
				gy = -gx / 2 - (cy + LoS);
				gz = -(gx + 1) / 2 + (cy + LoS);
				x = gx * .75f;
				for (; gy <= cy + LoS; gy++) {
					y = (gy - gz) * .45f;
					if (x >= 0 && y >= 0) {
						tmp = GameObject.Find (gx + "," + gy + "," + gz);
						if (Distance (cx, cy, cz, gx, gy, gz) <= LoS && !tmp)
							TileGenerator (x, y, gx, gy, gz);
					}
					gz--;
				}
			}
		}
			
/*		for (gx = 0; gx < 50; gx++) {				
			gy = -gx / 2;
			gz = -(gx + 1) / 2;
			x = gx * .75f;
			for (; gy < 50 - gx / 2; gy++) {
				y = (gy - gz) * .45f;
				TileGenerator (x, y, gx, gy, gz);
				gz--;
			}
		}
*/	}

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
			tmp.elevation = 0;
		}
	}
}
