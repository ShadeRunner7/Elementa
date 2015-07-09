using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnlimitedButtonWorks : Unarou {

	static Text buttonT, MP, AP, LP, UL, PL, DL;
	static GameObject cam, me0, me1, me2, me3;
	static int k;
	static Button test, me4, me5, me6, me7;

	// Use this for initialization
	void Start () {				
		k = 0;
		buttonT = GameObject.Find ("Canvas/PlayerB/PlayerBText").GetComponent<Text> ();
		MP = GameObject.Find ("Canvas/PlayerB/MPText").GetComponent<Text> ();
		AP = GameObject.Find ("Canvas/PlayerB/APText").GetComponent<Text> ();
		LP = GameObject.Find ("Canvas/LevelUpPanel/Panel/Text").GetComponent<Text> ();
		UL = GameObject.Find ("Canvas/UtilityB/Panel/Text").GetComponent<Text> ();
		PL = GameObject.Find ("Canvas/PowerB/Panel/Text").GetComponent<Text> ();
		DL = GameObject.Find ("Canvas/DefenceB/Panel/Text").GetComponent<Text> ();

		cam = GameObject.Find ("Main Camera");
		
		me0 = GameObject.Find ("Canvas/UtilityB");
		me1 = GameObject.Find ("Canvas/PowerB");
		me2 = GameObject.Find ("Canvas/DefenceB");
		me3 = GameObject.Find ("Canvas/LevelUpPanel");
		me4 = GameObject.Find ("Canvas/PlayerB").GetComponent<Button> ();
		me5 = GameObject.Find ("Canvas/EndTurn").GetComponent<Button> ();
		me6 = GameObject.Find ("Canvas/MoveB").GetComponent<Button> ();
		me7 = GameObject.Find ("Canvas/ActionB").GetComponent<Button> ();
		
		SelectChar ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	internal void ChangeTexts() {
		if (selected.eMP == 0)
			MP.text = selected.MP.ToString ();
		else
			MP.text = selected.MaxMP + "+" + selected.eMP;
		AP.text = selected.AP.ToString ();
		
		LP.text = selected.levelpoints.ToString ();
		UL.text = selected.UtilityLvl.ToString ();
		PL.text = selected.PowerLvl.ToString ();
		DL.text = selected.DefenceLvl.ToString ();
	}

	public void SelectChar () {
		if (Moving || Action) {
			Moving = false;
			Action = false;
			if (selected.Moved || selected.Did) {
				if (selected.Did) {
					selected.CAC = 0;
					CAL++;
				}
				selected.Moved = false;
				selected.Did = false;
				selected.AP--;
			}
		}		
//		PlayerTile.GetComponent<Tile> ().VisionCheck ();
		
		if (k == CharacterList.Length)
			k = 0;
		SelectedChar = CharacterList [k];
		selected = SelectedChar.GetComponent<Character> ();
		PlayerTile = GameObject.Find (selected.x + "," + selected.y + "," + selected.z);
		PlayerTile.GetComponent<Tile> ().IThinkThisIsGonnaBeABadIdea ();
		ChangeTexts ();
		buttonT.text = selected.name;
		cam.SendMessageUpwards ("SetTarget", SelectedChar, SendMessageOptions.DontRequireReceiver);
		
		if (selected.levelpoints > 0) {
			me0.SetActive (true);
			me1.SetActive (true);
			me2.SetActive (true);
			me3.SetActive (true);
		} else {			
			me0.SetActive (false);
			me1.SetActive (false);
			me2.SetActive (false);
			me3.SetActive (false);
		}
		k++;
	}

	public void MoveChar () {
		if (!Moving && selected.AP > 0 && selected.MP > 0) {
			if (Action) {
				Action = false;
				if (selected.Did) {
					selected.AP--;
					selected.CAC = 0;
					CAL++;
					selected.Did = false;
				}
			}
			selected.Moved = false;
			Moving = true;
		} else {
			Moving = false;
			if (selected.Moved && selected.AP > 0) {
				selected.AP--;
				selected.Moved = false;
			}
		}
		ChangeTexts ();
		PlayerTile.GetComponent<Tile> ().IThinkThisIsGonnaBeABadIdea ();
		PlayerTile.GetComponent<Tile> ().MoveCheck ();
	}

	public void ActionChar () {
		if (!Action && selected.AP > 0 && selected.CA != selected.CAC) {
			if (Moving) {
				Moving = false;
				if (selected.Moved) {
					selected.AP--;
					selected.Moved = false;
				}
			}
			Action = true;
			TileList = GameObject.FindGameObjectsWithTag ("Tile");
		} else {
			Action = false;
			if (selected.Did) {
				selected.AP--;
				selected.CAC = 0;
				CAL++;
				selected.Did = false;
			}
		}
		ChangeTexts ();
		PlayerTile.GetComponent<Tile> ().IThinkThisIsGonnaBeABadIdea ();
		PlayerTile.GetComponent<Tile> ().ActionCheck ();
	}
	
	public void AddLevel () {
		while (selected.levelpoints != 0) {
			if (name == "UtilityB")
				selected.UtilityLvl++;
			if (name == "PowerB")
				selected.PowerLvl++;
			if (name == "DefenceB")
				selected.DefenceLvl++;
			selected.levelpoints--;
			if (selected.levelpoints == 0) {
				StartCoroutine ("Hide");
			}
		}
		ChangeTexts ();
	}

	IEnumerator Hide() {
		me4.interactable = false;
		me5.interactable = false;
		me6.interactable = false;
		me7.interactable = false;
		yield return new WaitForSeconds (1);
		me0.SetActive (false);
		me1.SetActive (false);
		me2.SetActive (false);
		me3.SetActive (false);
		me4.interactable = true;
		me5.interactable = true;
		me6.interactable = true;
		me7.interactable = true;
	}

	public void EndTurn () {
		Moving = false;
		Action = false;
		foreach (GameObject chara in CharacterList) {
			Character tmp = chara.GetComponent<Character> ();

			if (tmp.MP == tmp.MaxMP && tmp.eMP <= 57) 
				tmp.eMP++;
			
			tmp.LoS = Mathf.Min (2 + (int)Mathf.Floor (tmp.UtilityLvl * .2f), 7);
			tmp.CR = tmp.LoS - 1;
			tmp.MaxMP = 1 + (int)Mathf.Floor (tmp.UtilityLvl * .2f);
			tmp.MaxAP = 1 + Mathf.Min ((int)Mathf.Floor (tmp.UtilityLvl * .1f), 3);
			
			tmp.CA = 1 + (int)Mathf.Floor (tmp.PowerLvl * .2f);
			tmp.LTPWR = tmp.PWR;
			tmp.PWR = 10 + tmp.PowerLvl;

			tmp.MP = tmp.MaxMP;
			tmp.AP = tmp.MaxAP;
			tmp.CAC = 0;
			chara.GetComponent<Skills> ().AddExp (0, 1);
		}
		ChangeTexts ();

		foreach (List<GameObject> h in CastAreaList) {
			foreach (GameObject hh in h) {
				hh.GetComponent<Tile> ().actionTurn--;
				hh.GetComponent<Tile> ().AllCheck ();
//				hh.GetComponent<Tile> ().VisionCheck ();
			}
		}

		int LC = 0;
		foreach (GameObject c in CharacterList) {
			LC += c.GetComponent<Character> ().AP;
		}

		CastAreaList = new List<GameObject>[LC];
		for (CAL = 0; CAL < LC; CAL++) 
			CastAreaList [CAL] = new List<GameObject> ();		
		
		CAL = 0;

		k = 0;

		MapGeneration (3);
//		Debug.Log (TileList.Length);
		SelectChar ();
	}
}
