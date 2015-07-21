using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapGen : Unarou {

	GameObject tile, tmp;
	float x, y;
	int gx = 0, gy = 0, gz = 0, cx, cy, cz, geneD;

	internal void GenerateMap (int xenomorph, GameObject c) {
			geneD = xenomorph;
			if (xenomorph == 0)
				geneD = selected.LoS + 1;
			cx = c.GetComponent<Character> ().x;
			cy = c.GetComponent<Character> ().y;
			cz = c.GetComponent<Character> ().z;

//			if (c == SelectedChar)
//				seen = new List<GameObject> ();

		StartCoroutine("Sweep");
			
		/*
		//////////////////////////
		//						//
		//	Alternative Method	//
		//						//
		//////////////////////////

		for (gx = 0; gx < 50; gx++) {				
			gy = -gx / 2;
			gz = -(gx + 1) / 2;
			x = gx * .75f;
			for (; gy < 50 - gx / 2; gy++) {
				y = (gy - gz) * .45f;
				TileGenerator (x, y, gx, gy, gz);
				gz--;
			}
		}

		//////////////////////////*/
	/// 
	}

	IEnumerator Sweep () {
		SweepIsRunning = true;
		for (gx = cx - geneD; gx <= cx + geneD; gx++) {
			gy = -gx / 2 - Mathf.Abs (cy + geneD);	
			gz = -(gx + 1) / 2 + Mathf.Abs (cy + geneD);
			for (; gy <= cy + geneD; gy++) {
				x = gx * .75f;
				y = (gy - gz) * .45f;
				if (y == 8.099999f)
					y = 8.1f;
				if (y == -8.099999f)
					y = -8.1f;
				if (x >= 0 && y >= 0) {
					tmp = GameObject.Find (gx + "," + gy + "," + gz);
					int d = Distance (cx, cy, cz, gx, gy, gz);
					if (d <= geneD && !tmp) {
						TileGenerator (x, y, gx, gy, gz);
						NewMap = true;
//						yield return new WaitForSeconds (DELAY);
					}
					if (tmp && d <= geneD) {
						Tile tmpT = tmp.GetComponent<Tile> ();
						int xenomorph = CharacterList.Length;

						tmpT.OnLoS = 0;
						foreach (GameObject c in CharacterList) {
							Character tmp0 = c.GetComponent<Character> ();
							d = Distance (tmp0.x, tmp0.y, tmp0.z, gx, gy, gz);
							if (d <= tmp0.LoS) {
								tmpT.OnLoS++;
							} else
								tmpT.OnLoS--;
						}

						if (tmpT.OnLoS == -xenomorph && d > selected.LoS)
							tmp.GetComponent<SpriteRenderer> ().color = Color.grey;
						if (tmpT.OnLoS > -xenomorph)
							tmp.GetComponent<SpriteRenderer> ().color = Color.white;
					}
				}
				gz--;
			}
		}
		SweepIsRunning = false;
		yield return new WaitForSeconds (DELAY);
	}
	
	void TileGenerator(float x, float y, int gx, int gy, int gz) {
		int wood = Dice (32), fire = Dice (32), ground = Dice (32), metal = Dice (32), water = Dice (32), leEle = 0;
		while (MultipleMaxes (wood, fire, ground, metal, water)) {
			wood = Dice (32);
			fire = Dice (32);
			ground = Dice (32);
			metal = Dice (32);
			water = Dice (32);
			leEle = 0;
		}
		leEle = Mathf.Max (wood, fire, ground, metal, water);

		//			//
		//	Wood	//
		//			//
		if (leEle == wood) {
			tile = Instantiate (Resources.Load (tiles [0]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;

			//
			// Main Element
			//
			int dice = Dice (32);
			if (dice == 32)
				tmp.wood = Random.Range (900, 1000);
			else if (dice < 32 && dice >= 25)
				tmp.wood = Random.Range (700, 899);
			else if (dice < 25 && dice >= 15)
				tmp.wood = Random.Range (500, 699);
			else if (dice < 15 && dice >= 5)
				tmp.wood = Random.Range (300, 499);
			else if (dice < 5)
				tmp.wood = Random.Range (0, 299);

			//
			// Secondary element
			//
			if (Dice (6) == 6) {
				tmp.water = Random.Range (0, 400);
				tmp.ground = 400 - tmp.water;
			} 
			//
			// Groundlevel
			else {
				dice = Dice (32);
				if (dice == 32)
					tmp.ground += Random.Range (900 - tmp.ground, 1000);
				else if (dice < 32 && dice >= 25)
					tmp.ground += Random.Range (700 - tmp.ground, 899);
				else if (dice < 25 && dice >= 15)
					tmp.ground += Random.Range (500 - tmp.ground, 699);
				else if (dice < 15)
					tmp.ground += Random.Range (400 - tmp.ground, 499);
			}

			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + tmp.water + Mathf.FloorToInt (tmp.metal / 2) - 400;
		} 
		//			//
		//	Ground	//
		//			//
		else if (leEle == ground) {
			tile = Instantiate (Resources.Load (tiles [2]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;

			//
			// Main Element
			//
			int dice = Dice (32);
			if (dice == 32)
				tmp.ground += Random.Range (1000, 1400);
			else if (dice < 32 && dice >= 25)
				tmp.ground += Random.Range (800, 999);
			else if (dice < 25 && dice >= 15)
				tmp.ground += Random.Range (600, 799);
			else if (dice < 15)
				tmp.ground += Random.Range (400, 599);

			//
			// Secondary elements
			//
			if (Dice (32) >= 30) {
				int k = Random.Range (0, 500);
				if (k >= 200)
					tmp.metal = k - k % 200;
			}
			if (Dice (32) >= 25)
				tmp.wood = Random.Range (0, Mathf.RoundToInt (tmp.ground / 10));

			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + Mathf.FloorToInt (tmp.metal / 2) - 400;
		} 
		//			//
		//	Water	//
		//			//
		else if (leEle == water) {
			tile = Instantiate (Resources.Load (tiles [4]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;

			//
			// Main Element
			//
			tmp.water = Random.Range (1, 400);

			//
			// Secondary Element(s)
			//
			if (Random.Range (0, 20) == 20)
				tmp.wood = Random.Range (0, tmp.water);

			//
			// Groundlevel
			tmp.ground = 400 - tmp.water;

			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = 0;
		} 
		//			//
		//	Metal	//
		//			//
		else if (leEle == metal) {
			tile = Instantiate (Resources.Load (tiles [3]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;

			//
			// Main Element
			//
			int dice = Dice (32);
			if (dice == 32)
				tmp.metal += Random.Range (2000, 2800);
			else if (dice < 32 && dice >= 25)
				tmp.metal += Random.Range (1500, 1999);
			else if (dice < 25 && dice >= 15)
				tmp.metal += Random.Range (750, 1499);
			else if (dice < 15)
				tmp.metal += Random.Range (1, 599);

			if (metal > 200)
				tmp.metal = metal - metal % 200;
			else
				tmp.metal = metal;

			//
			// Secondary Element
			//
			dice = Dice (32);
			if (dice == 32)
				tmp.ground = Random.Range (600, 1000);
			else if (dice >= 30 && dice != 32)
				tmp.ground = Random.Range (300, 599);
			else if (dice >= 25 && dice < 30)
				tmp.ground = Random.Range (0, 299);
			else
				tmp.ground = 400; 

			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + Mathf.FloorToInt (tmp.metal / 2) - 400;
		} 
		//			//
		//	Fire	//
		//			//
		else if (leEle == fire) {
			tile = Instantiate (Resources.Load (tiles [1]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
			Tile tmp = tile.GetComponent<Tile> ();
			tmp.x = gx;
			tmp.y = gy;
			tmp.z = gz;

			//
			// Main Element
			//
			tmp.fire = Random.Range (0, 500);

			//
			// Secondary Element
			//
			int dice = Dice (6);
			if (dice == 6) {
				dice = Dice (32);
				if (dice == 32)
					tmp.wood = Random.Range (700, 899);
				else if (dice < 32 && dice >= 25)
					tmp.wood = Random.Range (500, 699);
				else if (dice < 25 && dice >= 15)
					tmp.wood = Random.Range (300, 499);
				else if (dice < 15 && dice >= 5)
					tmp.wood = Random.Range (0, 299);
			}

			//
			// Groundlevel
			dice = Dice (32);
			if (dice == 32)
				tmp.ground += Random.Range (900 - tmp.ground, 1000);
			else if (dice < 32 && dice >= 25)
				tmp.ground += Random.Range (700 - tmp.ground, 899);
			else if (dice < 25 && dice >= 15)
				tmp.ground += Random.Range (500 - tmp.ground, 699);
			else if (dice < 15)
				tmp.ground += Random.Range (400 - tmp.ground, 499);
			
			tmp.name = gx + "," + gy + "," + gz;
			tmp.elevation = tmp.ground + Mathf.RoundToInt (tmp.metal / 2) - 400;
		}
	}

	int Dice (int x) {
		return Random.Range (1, x);
	}

	bool MultipleMaxes (int a, int b, int c, int d, int e) {
		int count = 0, max = 0;
		max = a;
		if (b > max)
			max = b;
		else if (b == max)
			count++;
		if (c > max)
			max = c;
		else if (c == max)
			count++;
		if (d > max)
			max = c;
		else if (d == max)
			count++;
		if (e > max)
			max = c;
		else if (e == max)
			count++;

		if (count == 0)
			return false;
		else
			return true;
	}

/*	void setup(int eleNum, string main, string secondary, bool groundNotMainSecondary) {
		tile = Instantiate (Resources.Load (tiles [0]), new Vector3 (x, y, 0), transform.rotation) as GameObject;
		Tile tmp = tile.GetComponent<Tile> ();

		tmp.x = gx;
		tmp.y = gy;
		tmp.z = gz;

		int mainEle, secondaryEle, levelingGround;
*///}
}
