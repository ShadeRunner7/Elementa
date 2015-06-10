using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlimitedButtonWorks : Unarou {

	static Text buttonT, MP, AP;
	static GameObject cam, me0, me1, me2;
	int k;
	Button test;

	// Use this for initialization
	void Start () {
		k = 0;
		if (name == "PlayerB") {
			buttonT = transform.FindChild ("PlayerBText").GetComponent<Text> ();
			MP = transform.FindChild ("MPText").GetComponent<Text> ();
			AP = transform.FindChild ("APText").GetComponent<Text> ();
			cam = GameObject.Find ("Main Camera");
			SelectChar();
		}

		me0 = GameObject.Find ("Canvas/UtilityB");
		me1 = GameObject.Find ("Canvas/PowerB");
		me2 = GameObject.Find ("Canvas/DefenseB");
	}
	
	// Update is called once per frame
	void Update () {
		if (Moving)
			GameObject.Find ("Canvas/MoveB").GetComponent<Button> ().IsActive ();
	}

	public void Testi(){
		Debug.Log (name);
	}

	internal void ChangeTexts() {		
		MP.text = selected.MP.ToString ();
		AP.text = selected.AP.ToString ();
	}

	public void SelectChar () {
		if (Moving) {
			Moving = false;
			selected.AP--;
		}
		if (k == CharacterList.Length)
			k = 0;
		SelectedChar = CharacterList[k];
		selected = SelectedChar.GetComponent<Character> ();
		ChangeTexts ();
		buttonT.text = selected.name;
		cam.SendMessageUpwards ("SetTarget", SelectedChar, SendMessageOptions.DontRequireReceiver);
		
		if (selected.levelpoints > 0) {
			me0.SetActive (true);
			me1.SetActive (true);
			me2.SetActive (true);
		}

		k++;
	}

	public void MoveChar () {
		if (!Moving)
			Moving = true;
	}	
	
	public void AddLevel () {
		if (selected.levelpoints != 0) {
			if (name == "UtilityB")
				selected.UtilityLvl++;
			if (name == "PowerB")
				selected.PowerLvl++;
			if (name == "DefenseB")
				selected.DefenseLvl++;
			selected.levelpoints--;
			if (selected.levelpoints == 0 ){
				me0.SetActive (false);
				me1.SetActive (false);
				me2.SetActive (false);
			}
		}
	}

	public void EndTurn () {
		Moving = false;
		foreach (GameObject chara in CharacterList) {
			chara.GetComponent<Character> ().MP = chara.GetComponent<Character> ().MaxMP;
			chara.GetComponent<Character> ().AP = chara.GetComponent<Character> ().MaxAP;
		}
		ChangeTexts ();
		
		k = 0;
		SelectChar ();
	}
}
