using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AudioSource))]
public class FlyThroughController : MonoBehaviour {
	
	[SerializeField]
	GameObject QueryObject;
	
	float speed;
	const float ROTATE_SPEED = 0.0f;
	const float MAX_SPEED  = 1.0f;
	const float ACCELERATE = 0.5f;
	const float DECELERATE = 0.2f;
	int upFlag = 0;
	float lastYPosData = 0;
	float upCount =0;
	float decelerate_cofig=0.0f;
	int delayCount =0;
	int move =0;

	
	QueryAnimationController.QueryChanAnimationType nowFlyingState;
	QueryAnimationController.QueryChanAnimationType previousFlyingState;

	public static GameObject ring;

	[SerializeField]
	GameObject groundCollider;
	
	// Use this for initialization
	void Start () {

		InitQuery ();
		
	}
	
	public void InitQuery () {
		
		speed = 0.0f;
		nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_IDLE;
		previousFlyingState = nowFlyingState;
		QueryObject.GetComponent<QueryAnimationController>().ChangeAnimation(QueryAnimationController.QueryChanAnimationType.FLY_IDLE);

	}
	
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("s")) {
			move = 1;
		}

		if(move ==2)
			updateMove ();

		else if(move ==1)
			updateMove ();


	}
	



	void updateMove()
	{
		GameObject QCF = GameObject.Find ("Query-Chan_FlyThrough");
		GameObject pig = GameObject.Find ("pig");
		GameObject Myo_sphere = GameObject.Find ("Sphere");
		GameObject Cube = GameObject.Find ("Cube");
		GameObject windSound = GameObject.Find ("windSound");
		GameObject flapping = GameObject.Find ("flapping");
		GameObject flapping2 = GameObject.Find ("flapping2");
		GameObject rightWing = GameObject.Find ("rightWing");
		GameObject leftWing = GameObject.Find ("l_wing_3");

		ring = GameObject.Find ("goThrough");
		//CharacterController controller = GetComponent<CharacterController>();

		//rotate to start point
		if (move == 1) {
			QCF.transform.Rotate (0, 90.0f, 0, Space.Self);
			pig.GetComponent<AudioSource> ().Play ();
			move =2;
		}
		// Rotate Right or Left
		if (Input.GetAxis("Horizontal") != 0)
		{
			transform.Rotate(0, Input.GetAxis("Horizontal") * ROTATE_SPEED, 0);
			if (Input.GetAxis("Horizontal") > 0)
			{
				nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_TORIGHT;
			}
			else if (Input.GetAxis("Horizontal") < 0)
			{ 
				nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_TOLEFT;
			}
		}
		else
		{
			this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0);
			nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_STRAIGHT;
		}
		
		
		//Myo Control part///////////////////////////////////////////////////////////////////////////////////////////////////

		Debug.Log (Myo_sphere.transform.position.y);
		decelerate_cofig = 0.0f;
		if (delayCount == 0) {	
			lastYPosData = Myo_sphere.transform.position.y;

			delayCount++;
		}
		if(delayCount >0)
			delayCount++;
		
		if (delayCount == 50)
			delayCount = 0;
		
		if (lastYPosData - Myo_sphere.transform.position.y < 0.01f && lastYPosData - Myo_sphere.transform.position.y > -0.01f ) {
			upFlag=0;
			
		}

		//속도유지
		if (Myo_sphere.transform.position.y > 0.39f) {
			
			//x눌린것처럼

			speed += ACCELERATE * Time.deltaTime;
			if (speed >  MAX_SPEED)
			{
				speed = MAX_SPEED;
			}
			upCount =0.0f;
			//decelerate_cofig = 0.2f;
			upFlag = 1;


		}

		if (Myo_sphere.transform.position.y > 0.43f) {
			
			//x눌린것처럼
			
			speed += (ACCELERATE) * Time.deltaTime;
			if (speed >  MAX_SPEED)
			{
				speed = MAX_SPEED;
			}
			upCount =0.7f;
			upFlag = 1;
			
		}

		
		if(Myo_sphere.transform.position.y < 0.32f)
		{	
			upFlag=0;
		}
		if (lastYPosData - Myo_sphere.transform.position.y < 0.01f && lastYPosData - Myo_sphere.transform.position.y > -0.01f ) {
			upFlag=0;
			
		}
		
		if(upFlag ==1)
		{	//Debug.Log ("Going UP");
			transform.Translate(Vector3.up * (ROTATE_SPEED+upCount) *  Time.deltaTime);
			nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_UP;
			
			if (this.transform.localPosition.y < groundCollider.transform.localPosition.y)
			{
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x, groundCollider.transform.localPosition.y, this.transform.localPosition.z);
			}

		}
		
		if (upFlag == 0) 
		{
			//Debug.Log ("Decrease");
			upFlag=0;
			transform.Translate(Vector3.up *-0.3f *  Time.deltaTime);
			nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_DOWN;
			if (this.transform.localPosition.y < groundCollider.transform.localPosition.y)
			{
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x, groundCollider.transform.localPosition.y, this.transform.localPosition.z);
			}
			
			speed -= (DECELERATE+decelerate_cofig) * Time.deltaTime;
			if (speed < 0.0f)
			{
				speed = 0.0f;
			}
		}

		if (speed <0.4f ) {

			flapping2.GetComponent<AudioSource>().Play ();

		}

		if (speed <0.44f )
			pig.GetComponent<AudioSource>().Play ();

		if (speed < 0.4f) {
			windSound.GetComponent<AudioSource> ().Play ();
			flapping.GetComponent<AudioSource>().Play ();
		}
		

			
		
		//rotate right/left
		//make ratation.x sync with QUERYCHAN
		//Debug.Log (Cube.transform.eulerAngles.y);


		if (Cube.transform.eulerAngles.y <= 45 && Cube.transform.eulerAngles.y > 15) {
			//Debug.Log("Turn Right slowly");
			QCF.transform.Rotate(0, 0.6f, 0, Space.Self);
;

		}
		
		if (Cube.transform.eulerAngles.y < 180 && Cube.transform.eulerAngles.y  > 45) {
			//Debug.Log("Turn Right fastly");
			QCF.transform.Rotate(0, 1.2f, 0, Space.Self);

		}
		
		if (Cube.transform.eulerAngles.y <= 340 && Cube.transform.eulerAngles.y > 310) {
			//Debug.Log("Turn Left slowly");
			QCF.transform.Rotate(0, -0.6f, 0, Space.Self);

		}
		
		if (Cube.transform.eulerAngles.y < 310 && Cube.transform.eulerAngles.y  > 240) {
			//Debug.Log("Turn Left fastly");
			QCF.transform.Rotate(0, -1.2f, 0, Space.Self);

		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Rotate Up or Down
		if (Input.GetAxis("Vertical") != 0)
		{
			transform.Translate(Vector3.up * Input.GetAxis("Vertical") * (ROTATE_SPEED) *  Time.deltaTime);
			if (Input.GetAxis("Vertical") > 0)
			{
				nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_UP;
			}
			else if (Input.GetAxis("Vertical") < 0)
			{ 
				nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_DOWN;
			}
			
			if (this.transform.localPosition.y < groundCollider.transform.localPosition.y)
			{
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x, groundCollider.transform.localPosition.y, this.transform.localPosition.z);
			}
		}
		
		// Move Forward
		Vector3 forwardSpeed = transform.TransformDirection(Vector3.forward * Time.deltaTime * speed);
		QCF.transform.position += forwardSpeed;
		//controller.Move (forwardSpeed);
		
		// Speed Control
		if (Input.GetKey("x"))
		{
			speed += ACCELERATE * Time.deltaTime;
			if (speed >  MAX_SPEED)
			{
				speed = MAX_SPEED;
			}
		}
		else if (Input.GetKey("z"))
		{
			speed -= DECELERATE * Time.deltaTime;
			if (speed < 0.0f)
			{
				speed = 0.0f;
			}
		}
		
		if (speed == 0.0f)
		{
			nowFlyingState = QueryAnimationController.QueryChanAnimationType.FLY_IDLE;
		}
		
		//		// ChangeAnimation
		//		if (previousFlyingState != nowFlyingState)
		//		{
		//			QueryObject.GetComponent<QueryAnimationController>().ChangeAnimation(nowFlyingState);
		//		}
		//		
		//		previousFlyingState = nowFlyingState;
		
	}
	
	
	void OnGUI () {
		
		GUI.Box( new Rect(30 , 10, 150, 30), "speed = " + speed);
		
	}
	
}
