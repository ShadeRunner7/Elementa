using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

	float x, y;
	int gx = 0, gy = 0, gz = 0;
	GameObject tile;
	string[] tiles = {
		"Tiles/WoodTile", "Tiles/FireTile", "Tiles/GroundTile", "Tiles/MetalTile", "Tiles/WaterTile"
	};
	int[,] grid = new int[10,3];



	// Use this for initialization
	void Start () {
		gx = 0;
		gy = 0;
		gz = 0;
		int counter = 0;
		/*
		for (x = 0; x < 5; x += 1.5f) {
			gy = -gx / 2;
			gz = gy;
			for (y = 0; y < 5; y += .9f) {				
				Debug.Log (gx + " " + gy + " " + gz);
				TileGenerator(x, y, gx, gy, gz);
				gy++;
				gz--;
			}
			gx += 2;
			counter += 2;
		}
		gx = 1;
		counter = 1;
		for (x = .75f; x < 5; x += 1.5f) {
			gy = (-gx + 1) / 2; gz = -gx - gy;
			for (y = .45f; y < 5; y += .9f) {
				Debug.Log (gx + " " + gy + " " + gz);
				TileGenerator(x, y, gx, gy, gz);
				gy++;
				gz--;
			}
			gx += 2;
			counter += 2;
		}
		*/
		bool loop = true;
		while (loop == true) 
			if (Distance (gx, gy, gz) <= 5) {
				gy = -gx / 2;
				gz = -(gx + 1) / 2;
				for (counter = 0; counter < 5; counter++) {				
					x = gx * .75f;
					y = (gy - gz) * .45f;
					Debug.Log (gx + " " + gy + " " + gz);
					TileGenerator (x, y, gx, gy, gz);
					gy++;
					gz--;
				}
				gx++;
				if (gx == 5) 
					loop = false;
			}

	}

	int Distance (int x, int y, int z) {
		return (Mathf.Abs (-x) + Mathf.Abs (-y) + Mathf.Abs (-z)) / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
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
			tmp.ground = Random.Range (0, Mathf.RoundToInt (tmp.wood / 4));
			if (Random.Range (0, 10) == 10)
				tmp.water = Random.Range (0, 400 - tmp.ground);
			if (tmp.water <= 0) 
				tmp.water = 0;
		}
		else if (leEle == fire) {
			tile = Instantiate (Resources.Load (tiles [1]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			tmp.fire = Random.Range (0, 500);
			tmp.ground = Random.Range (0, 500);
		}		
		else if (leEle == ground) {
			tile = Instantiate (Resources.Load (tiles [2]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			tmp.ground = Random.Range(-400, 1000);
			if (Random.Range (0,20) == 20) {
				int k = Random.Range (0, 500);
				tmp.metal = k - k % 400;
			}
			if (Random.Range (0, 10) == 10)
				tmp.wood = Random.Range (0, Mathf.RoundToInt (tmp.ground / 10));
		}
		else if (leEle == metal) {
			tile = Instantiate (Resources.Load (tiles [3]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			metal = Random.Range(0, 1000);
			if (metal > 400)
				tmp.metal = metal - metal % 400;
			else tmp.metal = metal;
			if (Random.Range(0,100) == 100)
			    tmp.ground = Random.Range (600, 1000);
			else if (Random.Range (0,20) == 20)
				tmp.ground = Random.Range (300, 599);
			else if (Random.Range(0,10) == 10)
				tmp.ground = Random.Range(0, 299);
			tile = Instantiate (Resources.Load (tiles [3]), new Vector3 (x, y, tmp.elevation), transform.rotation) as GameObject;
		}
		else if (leEle == water) {
			tile = Instantiate (Resources.Load (tiles [4]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;
			tmp.water = Random.Range (0, 400);
			if (Random.Range (0,20) == 20)
				tmp.wood = Random.Range(0, tmp.water);
		}
	}
}
