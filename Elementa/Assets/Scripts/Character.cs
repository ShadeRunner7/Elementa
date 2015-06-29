using UnityEngine;
using System.Collections;

public class Character : Unarou {
	
	public int x, y, z;
	public int LoS, CR, CA, CAC, LTPWR, MP, AP, eMP, PWR;
	public int MaxMP, MaxAP, Level, EXP;
	public int UtilityLvl = 0, PowerLvl = 0, DefenceLvl = 0;
	public int levelpoints;
	public int ele;
	public bool Moved = false, Did = false;
	int gx, gy, gz, d;
	GameObject tmp;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	internal void Update () {
	}

	internal void PlayerSetUp () {		
		ele = GameObject.Find (x + "," + y + "," + z).GetComponent<Tile> ().elevation;
	}

	internal void Position(int ux, int uy, int uz) {
		x = ux;
		y = uy;
		z = uz;
	}
}
