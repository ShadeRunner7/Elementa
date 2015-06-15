using UnityEngine;
using System.Collections;

public class Character : Unarou {
	
	public int x, y, z;
	public int LoS, MP, AP, eMP;
	public int MaxMP, MaxAP, Level, EXP;
	public int UtilityLvl = 0, PowerLvl = 0, DefenseLvl = 0;
	public int levelpoints;
	public int ele;
	int oEle, gx, gy, gz, d;
	GameObject tmp;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	internal void Update () {
		ele = PlayerTile.GetComponent<Tile> ().elevation;
	}

	internal void Position(int ux, int uy, int uz) {
		x = ux;
		y = uy;
		z = uz;
	}
}
