using UnityEngine;
using System.Collections;

public class Skills : Unarou {

	// Use this for initialization
	void Start () {
	}

	internal void SetUp () {
		GetComponent<Character> ().EXP = 0;
		GetComponent<Character> ().Level = 1;
		GetComponent<Character> ().levelpoints = 5;
		GetComponent<Character> ().LoS = 2;
		GetComponent<Character> ().MaxMP = 1;
		GetComponent<Character> ().eMP = 0;
		GetComponent<Character> ().MaxAP = 1;
		GetComponent<Character> ().CR = 1;
		GetComponent<Character> ().CA = 1;
		GetComponent<Character> ().PWR = 10;
	}
	
	// Update is called once per frame
	void Update () {;
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
