using UnityEngine;
using System.Collections;

public class collisionDetect : MonoBehaviour {
	GameObject man;
	void OnTriggerEnter(Collider collider) {
		man = GameObject.Find ("action_pack");
		man.GetComponent<Animator>().enabled = true;
		man.GetComponent<AudioSource> ().Play ();
		GameObject.Destroy(this.gameObject.GetComponent<BoxCollider>());
	}



}
//void OnTriggerEnter(Collider collid) {
//	//Debug.Log ("collision!!!!!!!!!!!!!!!!!!!!!!");
//	man = GameObject.Find ("action_pack");
//	man.GetComponent<Animator>().enabled = true;
//	man.GetComponent<AudioSource> ().Play ();