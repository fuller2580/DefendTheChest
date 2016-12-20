using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {
	float speed = 15f;
	public bool isBoss = false;
	Rigidbody2D rig;
	public bool split = false;
	GameObject follow;
	public float realHP = 2f;
	// Use this for initialization
	void Start () {
		if(isBoss) realHP = 5f;
		rig = GetComponent<Rigidbody2D>();
		if(transform.position.x > 0) speed *= -1;
		realHP += Mathf.Floor((PhotonNetwork.playerList.Length - 1)/3);
	}
	
	// Update is called once per frame
	void Update () {
		if(follow != null){
			transform.position = follow.transform.position;
		}
		else rig.velocity = transform.right * Time.deltaTime * speed;
	}

	void OnTriggerEnter2D(Collider2D col){
		
		if(col.gameObject.tag == "fireball"){
			PhotonNetwork.RPC(col.gameObject.GetComponent<PhotonView>(), "killMe", PhotonTargets.AllBuffered,false);
			realHP --;
			//print(realHP);
		}
		if(col.gameObject.tag == "sword"){
			realHP--;
		}
		if(col.gameObject.tag == "flamePillar"){
			realHP -= 5f;
		}
		if(col.gameObject.tag == "iceball"){
			realHP -= 4f;
		}
		if(col.gameObject.tag == "slowIce"){
			realHP --;
			speed = speed *.5f;
		}
		if(col.gameObject.tag == "shield"){
			follow = col.gameObject;

		}
		if(realHP <= 0){
			if(split){
				Vector3 pos = transform.position;
				Vector3 scal = transform.localScale;
				GameObject smGO;
				scal = new Vector3(scal.x*.66f,scal.y*.66f,1);
				pos.x = pos.x -.5f;
				smGO = PhotonNetwork.Instantiate("enemy3Sm", pos,Quaternion.identity,0);
				smGO.transform.localScale = scal;
				smGO.GetComponent<enemyScript>().enabled = true;
				//smGO.GetComponent<enemyScript>().setSpeed((speed-15)*2);
				pos.x = pos.x +.25f;
				smGO = PhotonNetwork.Instantiate("enemy3Sm", pos,Quaternion.identity,0);
				smGO.transform.localScale = scal;
				smGO.GetComponent<enemyScript>().enabled = true;
				//smGO.GetComponent<enemyScript>().setSpeed((speed-15)*2);
				pos.x = pos.x +.25f;
				smGO = PhotonNetwork.Instantiate("enemy3Sm", pos,Quaternion.identity,0);
				smGO.transform.localScale = scal;
				smGO.GetComponent<enemyScript>().enabled = true;
				//smGO.GetComponent<enemyScript>().setSpeed((speed-15)*2);
				pos.x = pos.x +.25f;
				smGO = PhotonNetwork.Instantiate("enemy3Sm", pos,Quaternion.identity,0);
				smGO.transform.localScale = scal;
				smGO.GetComponent<enemyScript>().enabled = true;
				//smGO.GetComponent<enemyScript>().setSpeed((speed-15)*2);
				pos.x = pos.x +.25f;
				smGO = PhotonNetwork.Instantiate("enemy3Sm", pos,Quaternion.identity,0);
				smGO.transform.localScale = scal;
				smGO.GetComponent<enemyScript>().enabled = true;
				//smGO.GetComponent<enemyScript>().setSpeed((speed-15)*2);
			}
			PhotonNetwork.RPC(GetComponent<PhotonView>(), "killMe", PhotonTargets.AllBuffered,false);
		}
	}

	public void setSpeed(float f){
		speed += (f);
		realHP += (f);
		//print(realHP);
	}
	 
}
