using System;
using System.Collections;
using UnityEngine;

public class LaserMotion : MonoBehaviour 
{
	public float laserSpeed = 2.0f;
	private float currentTime;
	
	public virtual void Start () 
	{
		currentTime = Time.time;
	}

	public virtual void Update () 
	{
		if((currentTime + 10) < Time.time) 
		{
			transform.Translate(Vector3.left * laserSpeed * Time.deltaTime);
		}
		else
		{
			transform.Translate(Vector3.right * laserSpeed * Time.deltaTime);
		}
	}
}

