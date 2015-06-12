using UnityEngine;
using System.Collections;

public class Skills : Unarou {

	// Use this for initialization
	void Start () {
	}

	internal void SetUp () {
		GetComponent<Character> ().EXP = 0;
		GetComponent<Character> ().Level = 1;
		GetComponent<Character> ().levelpoints = 1;
		GetComponent<Character> ().LoS = 2;
		GetComponent<Character> ().MaxMP = 1;
		GetComponent<Character> ().MaxAP = 1;
		GetComponent<Character> ().ele = 10000;
	}
	
	// Update is called once per frame
	void Update () {
			this.GetComponent<Character> ().LoS = Mathf.Min(2 + (int)Mathf.Floor(this.GetComponent<Character> ().UtilityLvl * .2f), 7);
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
