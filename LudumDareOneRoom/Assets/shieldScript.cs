using UnityEngine;
using System.Collections;

public class shieldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float temp = GetComponent<BoxCollider2D>().size.x;
		Vector2 tempVec = new Vector2(temp+(Time.deltaTime*.0000000001f),GetComponent<BoxCollider2D>().size.y);
			GetComponent<BoxCollider2D>().size = tempVec;
	}
}
