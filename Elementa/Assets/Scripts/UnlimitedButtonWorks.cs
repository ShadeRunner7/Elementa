using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlimitedButtonWorks : Unarou {

	Text buttonT, MP, AP;
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

	public void SelectChar () {
		if (k == CharacterList.Length)
			k = 0;
		buttonT.text = CharacterList [k].GetComponent<Character> ().name;
		MP.text = CharacterList [k].GetComponent<Character> ().MP.ToString ();
		AP.text = CharacterList [k].GetComponent<Character> ().AP.ToString ();
		cam.SendMessage ("SetTarget", CharacterList [k], SendMessageOptions.DontRequireReceiver);
		k++;
	}

	public void MoveChar () {
		CharacterList [k].SendMessageUpwards ("Position", SendMessageOptions.DontRequireReceiver);
		CharacterList [k].SendMessageUpwards ("FoW", SendMessageOptions.DontRequireReceiver);
	}
}
