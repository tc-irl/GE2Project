using UnityEngine;
using System.Collections;

public class JetAI : EnemyAI
{
	private float moveSpeed = 100.0f;
	private Transform laser;
	public static bool follow = false;
	private float distToLaser;
	
	public override void Start () 
	{
		base.Start ();
		laser = GameObject.FindWithTag("laser").transform;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update ();

		//distToLaser = Vector3.Distance (laser.transform.position, transform.position);
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toPlayer), rotationSpeed * Time.deltaTime);

		if (dist > 40 && follow == true) 
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}

	}
}
