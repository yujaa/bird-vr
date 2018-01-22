using UnityEngine;
using System.Collections;

public class ThroughDetect : MonoBehaviour {


	void OnTriggerEnter(Collider collider) {
		FlyThroughController.ring.GetComponent<AudioSource> ().Play ();
		ColorBoxByPose.thalmicMyo.Vibrate(Thalmic.Myo.VibrationType.Short);
		GameObject.Find ("pig").GetComponent<AudioSource> ().PlayDelayed (1.0f);
		GameObject.Destroy(this.gameObject);
	}

}
