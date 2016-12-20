using UnityEngine;
using System.Collections;

public class networkSyncPositions : MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Vector3 vel = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	Vector3 realScale = Vector3.zero;
	PhotonView photonView;
	float tlerp = 0.1f;
	public bool syncAnimation = false;
	Animator anim;
	bool walkingR = false;
	bool walkingL = false;
	bool shoot = false;
	public bool isWarrior = false;
	// Use this for initialization
	void Start () {
		photonView = GetComponent<PhotonView>();
		if(syncAnimation) anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if(photonView.isMine){

		}
		else{
			if(realPosition != Vector3.zero)transform.position = Vector3.Lerp(transform.position, realPosition, Time.deltaTime * 12);// + (vel*Time.deltaTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, tlerp);
			if(realScale != transform.localScale) transform.localScale = realScale;
			if(syncAnimation){
				if(!isWarrior){
					anim.SetBool("walkingRight",walkingR);
					anim.SetBool("walkingLeft",walkingL);
				}
				else{
					anim.SetBool("walking",walkingR);
					anim.SetBool("shield",walkingL);
				}
				anim.SetBool("shooting",shoot);
			}
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(transform.localScale);
			if(this.gameObject.GetComponent<Rigidbody>())stream.SendNext(GetComponent<Rigidbody>().velocity);
			if(syncAnimation){
				if(!isWarrior){
					stream.SendNext(anim.GetBool("walkingRight"));
					stream.SendNext(anim.GetBool("walkingLeft"));
				}
				else{
					stream.SendNext(anim.GetBool("walking"));
					stream.SendNext(anim.GetBool("shield"));
				}
				stream.SendNext(anim.GetBool("shooting"));
			}
		}
		else{
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
			realScale = (Vector3)stream.ReceiveNext();
			if(this.gameObject.GetComponent<Rigidbody>())vel = (Vector3)stream.ReceiveNext();
			if(syncAnimation){
				if(!isWarrior){
					walkingR = (bool)stream.ReceiveNext();
					walkingL = (bool)stream.ReceiveNext();
				}
				else {
					walkingR = (bool)stream.ReceiveNext();
					walkingL = (bool)stream.ReceiveNext();
				}
				shoot = (bool)stream.ReceiveNext();
			}
		}
	}
}
