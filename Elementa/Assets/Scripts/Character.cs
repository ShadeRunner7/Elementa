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

		for (gx = x - LoS; gx <= x + LoS + 2; gx++) {
			gy = -gx / 2 - (y + LoS + 2);
			gz = -(gx + 1) / 2 + (y + LoS + 2);
			for (; gy <= y + LoS + 2; gy++) {
				tmp = GameObject.Find (gx + "," + gy + "," + gz);
				d = Distance(x, y, z, gx, gy, gz);
				if (tmp && tmp.GetComponent<Tile>().OnLoS > -CharacterList.Length) {
					oEle = tmp.GetComponent<Tile>().elevation;
					Visual();
				}
			gz--;
			}
		}
	}


	void Visual() {
		bool io200p = oEle - ele >= 200 + 50 * (Mathf.Max (LoS, 4) - 4);
		bool u200p = oEle - ele < 200 + 50 * (Mathf.Max (LoS, 4) - 4);
		bool io50 = oEle - ele >= 50;
		bool u50 = oEle - ele < 50;
		bool om200m = oEle - ele > -200 - 50 * (Mathf.Max (LoS, 4) - 4);
		bool ium200m = oEle - ele <= -200 - 50 * (Mathf.Max (LoS, 4) - 4);
		int OnLos = tmp.GetComponent<Tile> ().OnLoS;
		bool seen = tmp.GetComponent<Tile> ().seen;

		if ((io200p || ium200m) && !seen && d > LoS)
			tmp.GetComponent<Tile> ().visionLevel = 0;
		else if (u200p && io50 && d <= LoS) {
			tmp.GetComponent<Tile> ().visionLevel = 1;
			tmp.GetComponent<Tile> ().seen = true;
		} else if (u50 && om200m && d <= LoS) {
			tmp.GetComponent<Tile> ().visionLevel = 2;			
			tmp.GetComponent<Tile> ().seen = true;
		}
	}

	internal void Position() {
		x = (int)(transform.position.x / .75f);
		y = (int)(transform.position.y / .9f - (x / 2));
		z = -(x + y);
	}
}
