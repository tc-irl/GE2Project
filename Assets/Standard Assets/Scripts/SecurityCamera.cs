using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]

public class SecurityCamera : EnemyAI
{	
	private Camera secCam; 
	private float currentTime; 
	private bool doOnce = true;
	private Transform laser;
	private float distToLaser;

	public virtual void Start () 
	{
		base.Start ();

		secCam = transform.FindChild("SecCamera").gameObject.camera;
		targetTransform = GameObject.FindWithTag("Player").transform; //Get the players transform
		laser = GameObject.FindWithTag("laser").transform;

		currentTime = Time.time;
	}
	// Update is called once per frame
	public override void Update () 
	{
		base.Update ();

		distToLaser = Vector3.Distance (laser.position, targetTransform.position);

		if(GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(secCam),targetTransform.collider.bounds) && dist < 19)
		{
			if(doOnce)
			{
				//audio.Play();
				IronManAI.anim.SetBool("TurnRight", true);
				doOnce = false;
			}
			else if((distToLaser < 16) && (!doOnce))
			{
				IronManAI.anim.SetBool("Run", true);
				secCam.camera.enabled = false;
			}
		}
	}
}