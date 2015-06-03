using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public int x, y, z,
			   LoS,
			   MP,
			   AP;
	int oEle, ele, gx, gy, gz, d;
	GameObject tmp;

	// Use this for initialization
	void Start () {
		position ();
//		Debug.Log (transform.position.y / .9f - x / 2);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void FoW () { //Fog of War
		ele = GameObject.Find (x + "," + y + "," + z).GetComponent<Tile> ().elevation;

		for (gx = x - LoS; gx <= x + LoS + MP; gx++) {
			gy = -gx / 2 - (y + LoS + MP);
			gz = -(gx + 1) / 2 + (y + LoS + MP);
			for (; gy <= y + LoS + MP; gy++) {
				tmp = GameObject.Find (gx + "," + gy + "," + gz);
				d = Distance(x, y, z, gx, gy, gz);
				if (x >= 0 && tmp) {
					oEle = tmp.GetComponent<Tile>().elevation;
					Diff();
				}
			gz--;
			}
		}
	}
	
	void Diff() {
		if ((oEle - ele >= 200 + 50 * (Mathf.Max (LoS, 6) - 6) && oEle - ele <= -200 - 50 * (Mathf.Max (LoS, 6) - 6)) || d > LoS)
			tmp.GetComponent<Tile> ().visionLevel = 0;
		else if (oEle - ele < 200 + 50 * (Mathf.Max (LoS, 6) - 6) && oEle - ele >= 50 && d <= LoS)
			tmp.GetComponent<Tile> ().visionLevel = 1;
		else if (oEle - ele < 50 && oEle - ele > -200 - 50 * (Mathf.Max (LoS, 6) - 6) && d <= LoS)
			tmp.GetComponent<Tile> ().visionLevel = 2;
	}

	void position() {
		x = Mathf.RoundToInt(transform.position.x / .75f);
		y = (int)(transform.position.y / .9f) - x / 2;
		z = -(x + y);
	}
		
	int Distance (int x0, int y0, int z0, int x1, int y1, int z1) {
		return Mathf.Max (Mathf.Abs (x0 - x1), Mathf.Abs (y0 - y1), Mathf.Abs (z0 - z1));
	}
}
