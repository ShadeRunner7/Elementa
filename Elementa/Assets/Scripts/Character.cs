using UnityEngine;
using System.Collections;

public class Character : Unarou {
	
	public int x, y, z;
	public int LoS, MP, AP;
	public int MaxMP, MaxAP, Level, EXP;
	public int UtilityLvl = 0, PowerLvl = 0, DefenseLvl = 0;
	public int levelpoints;
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

		for (gx = x - LoS; gx <= x + LoS * 2 + MP; gx++) {
			gy = -gx / 2 - (y + LoS * 2 + MP);
			gz = -(gx + 1) / 2 + (y + LoS * 2 + MP);
			for (; gy <= y + LoS * 2 + MP; gy++) {
				tmp = GameObject.Find (gx + "," + gy + "," + gz);
				d = Distance(x, y, z, gx, gy, gz);
				if (x >= 0 && tmp && !tmp.GetComponent<Tile>().OnLoS) {
					oEle = tmp.GetComponent<Tile>().elevation;
					Visual();
				}
			gz--;
			}
		}
	}


	void Visual() {
		bool plus200andOver = oEle - ele >= 200 + 50 * (Mathf.Max (LoS, 4) - 4);
		bool lessPlus200 = oEle - ele < 200 + 50 * (Mathf.Max (LoS, 4) - 4);
		bool plus50andOver = oEle - ele >= 50;
		bool lessPlus50 = oEle - ele < 50;
		bool moreMinus200 = oEle - ele > -200 - 50 * (Mathf.Max (LoS, 4) - 4);
		bool minus200andUnder = oEle - ele <= -200 - 50 * (Mathf.Max (LoS, 4) - 4);
		bool Seen2 = tmp.GetComponent<Tile> ().visionLevel == 2;
		bool Seen1 = tmp.GetComponent<Tile> ().visionLevel == 1;

		if ((plus200andOver || minus200andUnder || d > LoS) && !Seen2 && !Seen1)
			tmp.GetComponent<Tile> ().visionLevel = 0;
		else if (lessPlus200 && plus50andOver || (d > LoS && !Seen2)) {
			tmp.GetComponent<Tile> ().visionLevel = 1;
			Seen1 = true;
		} else if (lessPlus50 && moreMinus200 && d <= LoS) {
			tmp.GetComponent<Tile> ().visionLevel = 2;
			Seen2 = true;
		}
	}

	internal void Position() {
		x = (int)(transform.position.x / .75f);
		y = (int)(transform.position.y / .9f - (x / 2));
		z = -(x + y);
	}
}
