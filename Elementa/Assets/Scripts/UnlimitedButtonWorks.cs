using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlimitedButtonWorks : Unarou {

	static Text buttonT, MP, AP;
	GameObject cam;
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
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Testi(){
		Debug.Log (name);
	}

	internal void ChangeTexts() {		
		MP.text = selected.MP.ToString ();
		AP.text = selected.GetComponent<Character> ().AP.ToString ();
	}

	public void SelectChar () {
		Moving = false;
		if (k == CharacterList.Length)
			k = 0;
		SelectedChar = CharacterList[k];
		selected = SelectedChar.GetComponent<Character> ();
		ChangeTexts ();
		buttonT.text = selected.name;
		cam.SendMessage ("SetTarget", SelectedChar, SendMessageOptions.DontRequireReceiver);
		k++;
	}

	public void MoveChar () {
		if (!Moving)
			Moving = true;
	}

	public void EndTurn () {
		Moving = false;
		foreach (GameObject character in CharacterList) {
			character.GetComponent<Character>().MP = character.GetComponent<Character>().MaxMP;
			character.GetComponent<Character>().AP = character.GetComponent<Character>().MaxAP;
		}
		ChangeTexts ();
	}
}
