using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	GameObject target;
	public Rigidbody2D rb;
	float speed;

	bool up = false,
		 down = false,
		 right = false,
		 left = false;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {		
		if (Input.GetKey (KeyCode.W)) 
			up = true;
		else up = false;
		if (Input.GetKey (KeyCode.S)) 
			down = true;
		else down = false;
		if (Input.GetKey (KeyCode.A)) 
			left = true;
		else left = false;
		if (Input.GetKey (KeyCode.D)) 
			right = true;
		else right = false;
		
		if (up) 
			rb.velocity = new Vector2(0, speed);
		if (down) 
			rb.velocity = new Vector2(0, -speed);
		if (left) 
			rb.velocity = new Vector2(-speed, 0);
		if (right) 
			rb.velocity = new Vector2(speed, 0);
		if (!up && !down && !left && !right)
			rb.velocity = new Vector2(0, 0);
	}

	public void SetTarget(GameObject a) {
		target = a;
		speed = target.GetComponent<Character> ().LoS;
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
		if (target.GetComponent<Character> ().LoS != 0)
			GetComponent<Camera> ().orthographicSize = target.GetComponent<Character> ().LoS + 1 / (float)target.GetComponent<Character> ().LoS;
	}
}
