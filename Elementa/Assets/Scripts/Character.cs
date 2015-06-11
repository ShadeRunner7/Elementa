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
				if (tmp && d <= LoS + 1) {
					oEle = tmp.GetComponent<Tile>().elevation;
					Visual();
					Debug.Log(tmp.name);
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
		Tile Vision = tmp.GetComponent<Tile> ();
		bool isOnLoS = Vision.OnLoS > -CharacterList.Length;

		// OnLos anyones LoS?
		// y - visionLevel
		// 		0 - height check
		//			-> visionLevel 1 / 2, seen
		//		1 - height check for close one
		//			-> visionLevel 2
		// n - visionLevel
		//		2 - visionLevel 1

		for (int k = 0; k <= 1; k++) {
			if (isOnLoS && Vision.visionLevel == 0) {
				if (u200p && io50) {
					Vision.visionLevel = 1;
					Vision.seen = true;
				} else if (u50 && om200m) {
					Vision.visionLevel = 2;
					Vision.seen = true;
				}
			}
			if (isOnLoS && Vision.visionLevel == 1 && u50 && om200m)
				Vision.visionLevel = 2;
			if (isOnLoS && Vision.visionLevel == 2 && !(u50 && om200m))
				Vision.visionLevel = 1;
			if (!isOnLoS && Vision.visionLevel == 2)
				Vision.visionLevel = 1;
		}
	}

	internal void Position(int ux, int uy, int uz) {
		x = ux;
		y = uy;
		z = uz;
	}
}
