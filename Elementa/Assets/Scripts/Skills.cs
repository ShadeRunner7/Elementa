using UnityEngine;
using System.Collections;

public class Skills : Unarou {
		
	GameObject button;

	// Use this for initialization
	void Start () {
		if (name == "Emilia" || name == "Flora") {
			this.GetComponent<Character> ().EXP = 0;
			this.GetComponent<Character> ().Level = 1;
			this.GetComponent<Character> ().levelpoints = 1;
			this.GetComponent<Character> ().LoS = 2;
			this.GetComponent<Character> ().MaxMP = 1;
			this.GetComponent<Character> ().MaxAP = 1;
		}

		if (name == "UtilityB")
			button = GameObject.Find ("Canvas/" + name);
		if (name == "PowerB")
			button = GameObject.Find ("Canvas/" + name);
		if (name == "DefenseB")
			button = GameObject.Find ("Canvas/" + name);
		if (name == "LevelUpPanel")
			button = GameObject.Find ("Canvas/" + name);
	}
	
	// Update is called once per frame
	void Update () {
		if (button) {
			if (selected.levelpoints > 0)
				button.SetActive (true);
			else
				button.SetActive (false);
		}

		if (name == "Emilia" || name == "Flora") {
			this.GetComponent<Character> ().LoS = 2 + (int)Mathf.Floor(this.GetComponent<Character> ().UtilityLvl * .2f);
			this.GetComponent<Character> ().MaxMP = 1 + (int)Mathf.Floor(this.GetComponent<Character> ().UtilityLvl * .2f);
			this.GetComponent<Character> ().MaxAP = 1 + (int)Mathf.Floor(this.GetComponent<Character> ().UtilityLvl * .15f);
		}
	}

	internal void AddExp () {
		selected.EXP += 10;
		if (selected.EXP == selected.Level * 10) {
			selected.Level++;
			selected.levelpoints++;
			selected.EXP = 0;
		}
	}

	public void AddLevel () {
		if (name == "UtilityB")
			selected.UtilityLvl++;
		if (name == "PowerB")
			selected.PowerLvl++;
		if (name == "DefenseB")
			selected.DefenseLvl++;
		selected.levelpoints--;
	}
}
