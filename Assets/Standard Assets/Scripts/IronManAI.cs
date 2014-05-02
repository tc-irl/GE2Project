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
	private Vector3 toRoad,toPoint2,toNextPoint;
	public float rotationSpeed = 3.0f;
	private float moveSpeed = 6.0f;
	private float currentTime;
	private bool rotate = true;
	private bool b = true;
	private bool followPath = false;
	private bool arrived = false;
	private float maxRotationAngle = 80.0f;

	public static int runState = Animator.StringToHash("Base Layer.Run");
	public static int walkState = Animator.StringToHash("Base Layer.Walk");
	public static int jumpState = Animator.StringToHash("Base Layer.Jump");
	public static int rightState = Animator.StringToHash("Base Layer.Turn Right");

	public static Animator anim;
	public static AnimatorStateInfo currentBaseState;
	private float horizontal, vertical;
	private float distToLaser,distToRoad,distToPoint2,distToNextPoint;
	private Vector3 toCentreRoad = new Vector3(6.0f,0.0f,0.0f); 
	private Transform[] wayPoints;
	private Transform currentPoint;
	public GameObject road;
	private string j;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		anim.SetBool ("Walk", true);

		laserTransform = GameObject.FindWithTag("laser").transform;

		currentTime = Time.time;

	}

	void FollowPath (string marker, int markNo)
	{
		arrived = false;

		anim.SetBool("Run",false);
		anim.enabled = false;
		leftFoot.Play ();
		rightFoot.Play ();

		
		while(arrived == false)
		{
				transform.RotateAround(Vector3.right * 20.0f * Time.deltaTime);

				ironHead.RotateAround(Vector3.right * 20.0f * Time.deltaTime);

			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
			toNextPoint = (road.transform.Find(marker + markNo).transform.position - transform.position);

			//Debug.Log("Next Point: ");
			//Debug.Log(toNextPoint);
			Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toNextPoint), rotationSpeed * Time.deltaTime);

			distToNextPoint = Vector3.Distance (road.transform.Find(marker + markNo).transform.position, transform.position);

			if(distToNextPoint < 2)
			{
				arrived = true;
			}
		}
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

		//Debug.Log("Dist: " + distToPoint2);

		if(distToLaser < 5.5f)
		{
			anim.SetBool ("Jump", true);
		}  
		else if(!anim.IsInTransition(0))
		{
			anim.SetBool ("Jump", false);
		}

		if (distToRoad < 19) 
		{
			b = false;
			EyebotAI.follow = true;

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toPoint2), rotationSpeed * Time.deltaTime);

		}
		else if(b)
		{
			if((currentTime + 12) < Time.time)
			{
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toRoad), rotationSpeed * Time.deltaTime);
			}
		}

		if(distToPoint2 < 7)
		{
			followPath = true;
		}

		if(followPath)
		{
			for(int i = 3; i < 55; i++)
			{
				if(i < 10)
				{
					FollowPath("Marker000",i);
				}
				else
				{
					FollowPath("Marker00",i);
				}
			}
		}
	}
}