using UnityEngine;
using System.Collections;

public class AntMovement : MonoBehaviour {

	public float moveSpeed = 0.5f;
	public bool moving = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
		}
	}
	
	public void Turn() {
		transform.Rotate(0, 0, 90);
	}
}
