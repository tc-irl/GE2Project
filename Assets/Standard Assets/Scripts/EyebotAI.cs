using UnityEngine;
using System.Collections;

public class EyebotAI : EnemyAI
{
	public float moveSpeed = 5.0f;
	public static bool follow = false;

	public override void Start () 
	{
		base.Start ();
		targetTransform = GameObject.FindWithTag("Player").transform; //Get the players transform
	}

	// Update is called once per frame
	public override void Update () 
	{
		base.Update ();

		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toPlayer), rotationSpeed * Time.deltaTime);
		
		if (dist > 5 && follow == true) 
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
	}
}
