using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public int x = 0, y = 0, z, gx, gy, gz,
			   LoS = 4,
			   MP = 2,
			   AP = 2;
	MapGen a = new MapGen ();
	int oEle, Ele;
	GameObject tmp;

	// Use this for initialization
	void Start () {
		x = Mathf.RoundToInt(transform.position.x / .75f);
		y = (int)(transform.position.y / .9f) - x / 2;
		z = -(x + y);

//		Debug.Log (transform.position.y / .9f - x / 2);
//		Debug.Log (x + " " + y + " " + z);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void FoW () { //Fog of War
		Ele = GameObject.Find (x + "," + y + "," + z).GetComponent<Tile> ().elevation;

		for (gx = x - LoS; gx <= x + LoS; gx++) {
			gy = -gx / 2 - (y + LoS);
			gz = -(gx + 1) / 2 + (y + LoS);

			tmp = GameObject.Find (x + "," + (y + 1) + "," + (z - 1));
			oEle = tmp.GetComponent<Tile> ().elevation;
		}


/*
		if (oEle - Ele >= 50 || (oEle - Ele <= -200 - 50 * (Mathf.Max (LoS, 6) - 6))) tmp.GetComponent<Tile> ().vision = false;
		tmp = GameObject.Find ((x + 1) + "," + y + "," + (z - 1));	
		oEle = tmp.GetComponent<Tile> ().elevation;
		if (oEle - Ele >= 50 || (oEle - Ele <= -200 - 50 * (Mathf.Max (LoS, 6) - 6))) tmp.GetComponent<Tile> ().vision = false;
		tmp = GameObject.Find ((x + 1) + "," + (y - 1) + "," + z);	
		oEle = tmp.GetComponent<Tile> ().elevation;
		if (oEle - Ele >= 50 || (oEle - Ele <= -200 - 50 * (Mathf.Max (LoS, 6) - 6))) tmp.GetComponent<Tile> ().vision = false;
		tmp = GameObject.Find (x + "," + (y - 1) + "," + (z + 1));	
		oEle = tmp.GetComponent<Tile> ().elevation;
		if (oEle - Ele >= 50 || (oEle - Ele <= -200 - 50 * (Mathf.Max (LoS, 6) - 6))) tmp.GetComponent<Tile> ().vision = false;
		tmp = GameObject.Find ((x - 1) + "," + y + "," + (z + 1));	
		oEle = tmp.GetComponent<Tile> ().elevation;
		if (oEle - Ele >= 50 || (oEle - Ele <= -200 - 50 * (Mathf.Max (LoS, 6) - 6))) tmp.GetComponent<Tile> ().vision = false;
		tmp = GameObject.Find ((x - 1) + "," + (y + 1) + "," + z);
		oEle = tmp.GetComponent<Tile> ().elevation;
		if (oEle - Ele >= 50 || (oEle - Ele <= -200 - 50 * (Mathf.Max (LoS, 6) - 6))) tmp.GetComponent<Tile> ().vision = false;
*/	}
}
