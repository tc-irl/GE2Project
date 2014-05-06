using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EyebotAI : EnemyAI
{
	public float moveSpeed = 12.0f;
	public ParticleSystem flame;
	public static bool follow = false;
	private float currentTime;
	private bool doOnce = true;
	
	public override void Start () 
	{
		base.Start ();
		targetTransform = GameObject.FindWithTag("Player").transform; //Get the players transform
		currentTime = Time.time;
	}

	// Update is called once per frame
	public override void Update () 
	{
		base.Update ();

		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toPlayer), rotationSpeed * Time.deltaTime);
		
		if (dist > 5 && follow == true) 
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

			if((currentTime + 29) > Time.time)
			{	
				if(doOnce)
				{
					audio.Play();
					doOnce = false;
				}

				flame.Play();
			}
			else
			{
				flame.Stop();
				audio.Stop();
				follow = false;
				Destroy(this);
				camControl.jetIncoming.camera.enabled = true;
				camControl.eyeBotShoots.camera.enabled = false;
			}

		}

	}
}
