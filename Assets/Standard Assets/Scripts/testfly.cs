/** Author: Tony Cullen C10385847
 * Assignment: Games Engines 2
 * Class: IronMan Class: Code related to the IronMan model goes here. 
 * 
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testfly : MonoBehaviour 
{
	public ParticleSystem leftFoot;
	public ParticleSystem rightFoot;
	public GameObject iron;
	private CameraControl camControl;
	private JetAI jet;
	private Vector3 toRoad,toPoint2,toNextPoint;
	public float rotationSpeed = 3.0f;
	public float moveSpeed = 20.0f;
	private float currentTime;
	private bool rotate = true;
	private bool b = true;
	private bool followPath = false;
	private bool arrived = false;
	private bool doOnce = true;
	private bool playOnce = true;
	private bool showTexture = false;
	private float angle = 0;
	
	private float distToNextPoint;
	private Transform currentPoint;
	public Transform jetTrans, ironTrans;
	public GameObject road;
	private GameObject c;
	private string j;
	private int i = 5;
	private float decel = 0;
	private Vector3 toJet,toJet2;
	// Use this for initialization
	void Start () 
	{
		c = GameObject.FindWithTag("camc"); //Get the players transform
		camControl = c.GetComponent <CameraControl>();

		currentTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (currentTime);
		leftFoot.Play ();
		rightFoot.Play ();

		toJet = jetTrans.transform.position - ironTrans.transform.position;
		toJet2 = jetTrans.transform.position - camControl.ironManFP.camera.transform.position;
		
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		
		if(i < 10)
		{
			j = "Marker000";
		}
		else
		{
			j = "Marker00";
		}
		
		
		toNextPoint = (new Vector3(road.transform.Find(j + i).transform.position.x, 
		                           transform.position.y,
		                           road.transform.Find(j + i).transform.position.z) - transform.position);

		//Quaternion.AngleAxis(30.0f,Vector3.forward)
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(toNextPoint) * Quaternion.AngleAxis(-30.0f,Vector3.forward), rotationSpeed * Time.deltaTime);	

		
		distToNextPoint = Vector3.Distance (new Vector3(
			road.transform.Find(j + i).transform.position.x,
			transform.position.y, 
			road.transform.Find(j + i).transform.position.z), 
		                                    transform.position);

		if((road.transform.Find(j + i).transform.name) == "Marker0028")
		{
			if(playOnce)
			{
				jetTrans.audio.Play();
				playOnce = false;
			}

			moveSpeed = 0.0f;
			ironTrans.transform.rotation = Quaternion.Slerp (ironTrans.transform.rotation, Quaternion.LookRotation (toJet), 3.0f * Time.deltaTime);
			camControl.ironManFP.camera.transform.rotation = Quaternion.Slerp (camControl.ironManFP.camera.transform.rotation, Quaternion.LookRotation (toJet2), 3.0f * Time.deltaTime);
		}
		else
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(toNextPoint), rotationSpeed * Time.deltaTime);		
		}
		
		if((road.transform.Find(j + i).transform.name) == "Marker0055")
		{
			decel = (distToNextPoint / 5);
			
			moveSpeed = moveSpeed * decel;
		}
		
		if(distToNextPoint < 2)
		{
			i++;
		}
	}
}