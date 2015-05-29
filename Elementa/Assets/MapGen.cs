﻿using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

	float x, y;
	int gx = 0, gy = 0, gz = 0;
	GameObject tile;
	string[] tiles = {
		"Tiles/WoodTile", "Tiles/FireTile", "Tiles/GroundTile", "Tiles/MetalTile", "Tiles/WaterTile"
	};

	// Use this for initialization
	void Start () {
		gx = 0;
		gy = 0;
		gz = 0;
		int counter = 0;
		for (gx = 0; gx < 10; gx++) {
				gy = -gx / 2;
				gz = -(gx + 1) / 2;
				for (counter = 0; counter < 10; counter++) {				
					x = gx * .75f;
					y = (gy - gz) * .45f;
//					Debug.Log (gx + " " + gy + " " + gz);
					if (Distance (gx, gy, gz) <= 5)
					TileGenerator (x, y, gx, gy, gz);
					gy++;
					gz--;
				}
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
			tmp.ground = Random.Range (0, 1000);
			if (Random.Range (0, 5) == 5)
				tmp.water = Random.Range (0, 400 - tmp.ground);
			else if (tmp.ground < 400) tmp.ground += 400;
			if (tmp.water <= 0) 
				tmp.water = 0;
			tmp.name = gx + "," + gy + "," + gz;
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
		}
	}
}
