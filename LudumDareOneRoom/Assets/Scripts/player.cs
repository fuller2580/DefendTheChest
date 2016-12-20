using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class player : MonoBehaviour {
	Animator anim;
	public float speed = 0f;
	public float waitTime = .5f;
	int dir = -1;
	bool notWaiting = true;
	bool firstShot = true;
	public GameObject fireball;
	public GameObject flamepillar;
	public GameObject iceball;
	public int special;
	bool abilOnCD = false;
	float timeOffCD;
	float scd;
	public Text cd;
	public bool isWarrior = false;
	public GameObject name;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		GameObject temp = GameObject.FindGameObjectWithTag("cd");
		cd = temp.GetComponent<Text>();
		if(isWarrior) dir = 1;
	}
	
	// Update is called once per frame
	void Update () {
		inputs();
		if(abilOnCD){
			scd = Mathf.Floor(timeOffCD-Time.time);
			cd.text = "Special Cooldown: "+ scd;
		}
		else cd.text = "Special Cooldown: 0";
	}

	void inputs(){
		if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.W)) firstShot = true;
		if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.W)){
			if(!isWarrior){
			anim.SetBool("walkingLeft", false);
			anim.SetBool("walkingRight", false);
			}
			else{
				anim.SetBool("walking",false);
			}
			anim.SetBool("shooting",true);
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			if(!isWarrior){
				if(notWaiting){
					notWaiting = false;
					if(!firstShot){
						GameObject fb = PhotonNetwork.Instantiate(fireball.name,transform.position, Quaternion.identity,0) as GameObject;
						Vector3 tempScale = fb.transform.localScale;
						tempScale.x *= dir;
						fb.transform.localScale = tempScale;
						fb.GetComponent<Rigidbody2D>().velocity = transform.right * (-3*dir);
					}
					else firstShot = false;
					StartCoroutine(wait());
				
				}
			}
			else{
				if(notWaiting){
					notWaiting = false;
					if(!firstShot){
					Vector3 temploc = transform.position;
					temploc.x -= .25f*dir;
					GameObject sw = PhotonNetwork.Instantiate("swordHit",temploc,Quaternion.identity,0) as GameObject;
					sw.GetComponent<delete>().enabled = true;
					}
					else firstShot = false;
					StartCoroutine(wait());
				}
			}
		}
		else{
			anim.SetBool("shooting",false);
			if((Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.S)) && abilOnCD == false){
				abilOnCD = true;
				Vector3 tempLoc = transform.position;
				switch(special){
					case 1:
						tempLoc.x += -2*dir;
						tempLoc.y -= .2f;
						PhotonNetwork.Instantiate(flamepillar.name, tempLoc, Quaternion.Euler(-90,0,0),0);
						StartCoroutine(specialWait(45));
						break;
					case 2:
						GameObject ib = PhotonNetwork.Instantiate(iceball.name, tempLoc, Quaternion.identity,0) as GameObject;
						ib.GetComponent<spawnRain>().xforce *= -dir;
						StartCoroutine(specialWait(30));
						break;
					case 3:
						anim.SetBool("shield",true);
						Vector3 temploc = transform.position;
						temploc.x -= .25f*dir;
						temploc.y += .3f;
						GameObject sh = PhotonNetwork.Instantiate("shield",temploc,Quaternion.identity,0) as GameObject;
						sh.GetComponent<Rigidbody2D>().velocity = transform.right * (-1*dir);
						anim.SetBool("shield",false);
						StartCoroutine(specialWait(15));
						break;
					default:
						StartCoroutine(specialWait(0));
						break;
				}
			}
			if(!isWarrior){
				if(Input.GetKey(KeyCode.A)){
					anim.SetBool("walkingLeft", true);
					dir = 1;
					GetComponent<Rigidbody2D>().velocity = transform.right * Time.deltaTime * -speed;
				}
				else anim.SetBool("walkingLeft", false);

				if(Input.GetKey(KeyCode.D) && !anim.GetBool("walkingLeft")){
					anim.SetBool("walkingRight", true);
					dir = -1;
					GetComponent<Rigidbody2D>().velocity = transform.right * Time.deltaTime * speed;
				}
				else anim.SetBool("walkingRight", false);
				if(anim.GetBool("walkingLeft") == false && anim.GetBool("walkingRight") == false) GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}
			else{
				if(Input.GetKey(KeyCode.A)){
					anim.SetBool("walking",true);
					dir = 1;
					GetComponent<Rigidbody2D>().velocity = transform.right * Time.deltaTime * -speed;
					Vector3 tempScale = transform.localScale;
					tempScale.x = .12f;
					Vector3 tempNameScale = name.transform.localScale;
					tempNameScale.x = 8.3333333f;
					name.transform.localScale = tempNameScale;
					transform.localScale = tempScale;
				}
				else if(Input.GetKey(KeyCode.D)){
					anim.SetBool("walking",true);
					dir = -1;
					GetComponent<Rigidbody2D>().velocity = transform.right * Time.deltaTime * speed;
					Vector3 tempScale = transform.localScale;
					tempScale.x = -.12f;
					Vector3 tempNameScale = name.transform.localScale;
					tempNameScale.x = -8.3333333f;
					name.transform.localScale = tempNameScale;
					transform.localScale = tempScale;
				}
				else{
					anim.SetBool("walking",false);
					GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				}
			}
		}
	}

	public void setSpecial(int i){
		special = i;
	}

	IEnumerator specialWait(float w){
		timeOffCD = Time.time + w;
		yield return new WaitForSeconds(w);
		abilOnCD = false;
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(waitTime);
		notWaiting = true;
	}
}
