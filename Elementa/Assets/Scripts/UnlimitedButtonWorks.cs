using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlimitedButtonWorks : MonoBehaviour {

	Text buttonT, MP, AP;
	GameObject[] players;
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
			selectChar();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void testi(){
		Debug.Log (name);
	}

	public void selectChar () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		if (k == players.Length)
			k = 0;
		buttonT.text = players [k].GetComponent<Character> ().name;
		MP.text = players [k].GetComponent<Character> ().MP.ToString ();
		AP.text = players [k].GetComponent<Character> ().AP.ToString ();
		cam.SendMessage ("SetTarget", players [k], SendMessageOptions.DontRequireReceiver);
		k++;
	}

	public void MoveChar () {
		players [k].SendMessage ("position", SendMessageOptions.DontRequireReceiver);
		players [k].SendMessage ("FoW", SendMessageOptions.DontRequireReceiver);
	}
}
