﻿using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public int x, y, z,
			   wood, fire, ground, metal, water,
			   elevation;

	public GameObject adj0, adj1, adj2, adj3, adj4, adj5;

	// Use this for initialization
	void Start () {
		elevation = ground + Mathf.RoundToInt(metal / 2) - water;
		
		adj0 = GameObject.Find (x + "," + (y + 1) + "," + (z - 1));	
		adj1 = GameObject.Find ((x + 1) + "," + y + "," + (z - 1));	
		adj2 = GameObject.Find ((x + 1) + "," + (y - 1) + "," + z);	
		adj3 = GameObject.Find (x + "," + (y - 1) + "," + (z + 1));	
		adj4 = GameObject.Find ((x - 1) + "," + y + "," + (z + 1));	
		adj5 = GameObject.Find ((x - 1) + "," + (y + 1) + "," + z);	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
