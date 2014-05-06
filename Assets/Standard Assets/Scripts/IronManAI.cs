/** Author: Tony Cullen C10385847
 * Assignment: Games Engines 2
 * Class: IronMan Class: Code related to the IronMan model goes here. 
 * 
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class IronManAI : MonoBehaviour 
{
	public ParticleSystem leftFoot;
	public ParticleSystem rightFoot;
	private Transform laserTransform;
	public Transform roadTransform,roadTransform2,ironHead;
	public GameObject iron;
	private CameraControl camControl;
	private JetAI jet;
	private Vector3 toRoad,toPoint2,toNextPoint;
	public float rotationSpeed = 3.0f;
	public float moveSpeed = 10.0f;
	private float currentTime;
	private bool rotate = true;
	private bool b = true;
	private bool followPath = false;
	private bool arrived = false;
	private bool doOnce = true;
	private float angle = 0;

	public static Animator anim;
	public static AnimatorStateInfo currentBaseState;
	private float horizontal, vertical;
	private float distToLaser,distToRoad,distToPoint2,distToNextPoint;
	private Vector3 toCentreRoad = new Vector3(6.0f,0.0f,0.0f); 
	public GameObject road;
	private string j;
	private int i = 5;
	private float decel = 0;
	//private float maxRotationAngle = 90.0f;
	private float distanceInX = 0.0f;
	private float distanceInZ = 0.0f;
	private GameObject c;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		anim.SetBool ("Walk", true);

		laserTransform = GameObject.FindWithTag("laser").transform;

		c = GameObject.FindWithTag("camc"); //Get the players transform
		camControl = c.GetComponent <CameraControl>();
		
		currentTime = Time.time;

	}


	// Update is called once per frame
	void Update () 
	{
		toRoad = ((roadTransform.position + toCentreRoad) - transform.position);
		toPoint2 = (roadTransform2.position - transform.position);
		distToRoad = Vector3.Distance (roadTransform.position, transform.position);
		distToLaser = Vector3.Distance (laserTransform.position, transform.position);
		distToPoint2 = Vector3.Distance (roadTransform2.position, transform.position);
		
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		vertical = Input.GetAxis ("Vertical");
		horizontal = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed",vertical);
		anim.SetFloat ("Direction", horizontal);

		distToLaser = Vector3.Distance (laserTransform.position, transform.position);

		if(distToLaser < 5.5f)
		{
			anim.SetBool ("Jump", true);
		}  
		else if(!anim.IsInTransition(0))
		{
			anim.SetBool ("Jump", false);
		}

		if (distToRoad < 22) 
		{
			EyebotAI.follow = true;
		}
		if (distToRoad < 19) 
		{
			b = false;

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toPoint2), rotationSpeed * Time.deltaTime);

			camControl.ironManTP.camera.enabled = false;
			camControl.eyeBotShoots.camera.enabled = true;

		}
		else if(b)
		{
			if((currentTime + 12) < Time.time)
			{
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toRoad), rotationSpeed * Time.deltaTime);
			}
		}

		if(distToPoint2 < 10)
		{
			followPath = true;
		}
		else if(followPath)
		{
			IronManAI.anim.SetBool("Run", false);
			anim.enabled = false;
			iron.rigidbody.useGravity = false;
			leftFoot.Play();
			rightFoot.Play();

			JetAI.follow = true;

			if(transform.position.y < 90)
			{
				transform.Translate(Vector3.up * 10.0f * Time.deltaTime);
			}
		}
	}
}