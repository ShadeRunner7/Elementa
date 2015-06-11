﻿using UnityEngine;
using System.Collections;

public class Skills : Unarou {

	// Use this for initialization
	void Start () {
	}

	internal void SetUp () {
		this.GetComponent<Character> ().EXP = 0;
		this.GetComponent<Character> ().Level = 1;
		this.GetComponent<Character> ().levelpoints = 1;
		this.GetComponent<Character> ().LoS = 2;
		this.GetComponent<Character> ().MaxMP = 1;
		this.GetComponent<Character> ().MaxAP = 1;
	}
	
	// Update is called once per frame
	void Update () {
			this.GetComponent<Character> ().LoS = Mathf.Min(2 + (int)Mathf.Floor(this.GetComponent<Character> ().UtilityLvl * .2f), 5);
			this.GetComponent<Character> ().MaxMP = 1 + (int)Mathf.Floor(this.GetComponent<Character> ().UtilityLvl * .2f);
			this.GetComponent<Character> ().MaxAP = 1 + (int)Mathf.Floor(this.GetComponent<Character> ().UtilityLvl * .15f);
	}

	internal void AddExp (int a) {
		selected.EXP += a;
		while (selected.EXP >= selected.Level * 10) {
			selected.EXP -= selected.Level * 10;
			selected.Level++;
			selected.levelpoints++;
		}
	}
}
