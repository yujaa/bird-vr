using UnityEngine;
using System.Collections;

public class train : MonoBehaviour {

		void OnTriggerEnter(Collider collider) {
		FlyThroughController.ring.GetComponent<AudioSource> ().Play ();
		GameObject.Find ("trainsound").GetComponent<AudioSource> ().Play ();
		GameObject.Destroy(this.gameObject);
		}
		

}
