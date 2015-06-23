using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlimitedButtonWorks : Unarou {

	static Text buttonT, MP, AP;
	static GameObject cam, me0, me1, me2, me3;
	static int k;
	Button test;

	// Use this for initialization
	void Start () {	
		if (name == "PlayerB") {			
			k = 0;
			buttonT = transform.FindChild ("PlayerBText").GetComponent<Text> ();
			MP = transform.FindChild ("MPText").GetComponent<Text> ();
			AP = transform.FindChild ("APText").GetComponent<Text> ();
			cam = GameObject.Find ("Main Camera");
			
			me0 = GameObject.Find ("Canvas/UtilityB");
			me1 = GameObject.Find ("Canvas/PowerB");
			me2 = GameObject.Find ("Canvas/DefenceB");
			me3 = GameObject.Find ("Canvas/LevelUpPanel");

			SelectChar ();
		} else {
			me0 = GameObject.Find ("Canvas/UtilityB");
			me1 = GameObject.Find ("Canvas/PowerB");
			me2 = GameObject.Find ("Canvas/DefenceB");
			me3 = GameObject.Find ("Canvas/LevelUpPanel");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Moving)
			GameObject.Find ("Canvas/MoveB").GetComponent<Button> ().IsActive ();
	}

	internal void ChangeTexts() {
		if (selected.eMP == 0)
			MP.text = selected.MP.ToString ();
		else
			MP.text = selected.MaxMP + "+" + selected.eMP;
		AP.text = selected.AP.ToString ();
	}

	public void SelectChar () {
		if (Moving || Action) {
			Moving = false;
			Action = false;
			if (selected.Moved || selected.Did) {
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
		
		if (selected.levelpoints > 0 && selected.MP >= selected.MaxMP && selected.AP == selected.MaxAP) {
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
					selected.Did = false;
				}
			}
			selected.Moved = false;
			Moving = true;
		} else {
			Moving = false;
			if (selected.Moved) {
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
		} else {
			Action = false;
			if (selected.Did) {
				selected.AP--;
				selected.Did = false;
			}
		}
		ChangeTexts ();
		PlayerTile.GetComponent<Tile> ().IThinkThisIsGonnaBeABadIdea ();
		PlayerTile.GetComponent<Tile> ().ActionCheck ();
	}
	
	public void AddLevel () {
		if (selected.levelpoints != 0) {
			if (name == "UtilityB")
				selected.UtilityLvl++;
			if (name == "PowerB")
				selected.PowerLvl++;
			if (name == "DefenceB")
				selected.DefenceLvl++;
			selected.levelpoints--;
			if (selected.levelpoints == 0 ){
				me0.SetActive (false);
				me1.SetActive (false);
				me2.SetActive (false);
				me3.SetActive (false);
			}
		}
	}

	public void EndTurn () {
		Moving = false;
		Action = false;
		foreach (GameObject chara in CharacterList) {
			Character tmp = chara.GetComponent<Character> ();
			if (tmp.MP < tmp.MaxMP) 
				tmp.MP = tmp.MaxMP;
			else if (tmp.MP == tmp.MaxMP && tmp.eMP <= 57) 
				tmp.eMP++;
			tmp.AP = tmp.MaxAP;
			tmp.LTCAC = tmp.CAC;
			tmp.CAC = 0;
			
			tmp.LoS = Mathf.Min (2 + (int)Mathf.Floor (tmp.UtilityLvl * .2f), 7);
			tmp.CR = tmp.LoS - 1;
			tmp.MaxMP = 1 + (int)Mathf.Floor (tmp.UtilityLvl * .2f);
			tmp.MaxAP = 1 + Mathf.Min ((int)Mathf.Floor (tmp.UtilityLvl * .1f), 3);

			tmp.CA = 1 + (int)Mathf.Floor (tmp.PowerLvl * .2f);
			tmp.PWR = 10 + tmp.PowerLvl;
		}
		ChangeTexts ();

		foreach (GameObject h in TileList)
			if (h.GetComponent <Tile> ().IsActioned) {
				h.GetComponent<Tile> ().actionTurn--;
				h.GetComponent<Tile> ().AllCheck ();
				h.GetComponent<Tile> ().VisionCheck ();
			}
		
		k = 0;
		SelectChar ();
	}
}
