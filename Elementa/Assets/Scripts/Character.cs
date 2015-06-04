using UnityEngine;
using System.Collections;

public class Character : Unarou {
	
	public int x, y, z;
	public int LoS, MP, AP;
	int oEle, ele, gx, gy, gz, d;
	GameObject tmp;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	internal void FoW () { //Fog of War
		ele = GameObject.Find (x + "," + y + "," + z).GetComponent<Tile> ().elevation;

		for (gx = x - LoS; gx <= x + LoS + MP; gx++) {
			gy = -gx / 2 - (y + LoS + MP);
			gz = -(gx + 1) / 2 + (y + LoS + MP);
			for (; gy <= y + LoS + MP; gy++) {
				tmp = GameObject.Find (gx + "," + gy + "," + gz);
				d = Distance(x, y, z, gx, gy, gz);
				if (tmp) Debug.Log(tmp + " " + tmp.CompareTag("FoWTile"));
				if (x >= 0 && tmp && tmp.CompareTag("FoWTile")) {
					oEle = tmp.GetComponent<Tile>().elevation;
					Visual();
				}
			gz--;
			}
		}
	}
	
	void Visual() {
		if ((oEle - ele >= 200 + 50 * (Mathf.Max (LoS, 4) - 4) && oEle - ele <= -200 - 50 * (Mathf.Max (LoS, 4) - 4)) || d > LoS)
			tmp.GetComponent<Tile> ().visionLevel = 0;
		else if (oEle - ele < 200 + 50 * (Mathf.Max (LoS, 4) - 4) && oEle - ele >= 50) {
			tmp.GetComponent<Tile> ().visionLevel = 1;
			tmp.tag = "VisibleTile";
		} else if (oEle - ele < 50 && oEle - ele > -200 - 50 * (Mathf.Max (LoS, 6) - 6) && d <= LoS) {
			tmp.GetComponent<Tile> ().visionLevel = 2;
			tmp.tag = "ClearTile";
		}
	}

	internal void Position() {
		x = Mathf.RoundToInt(transform.position.x / .75f);
		y = (int)(transform.position.y / .9f) - x / 2;
		z = -(x + y);
	}
}
