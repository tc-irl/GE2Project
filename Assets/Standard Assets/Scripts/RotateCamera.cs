/**
 * 
 * Tutorials used for this class: https://www.youtube.com/watch?v=ST5FKyZRGy8
 */

using System;
using System.Collections;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{	
	public float rotationSpeed = 1.0f;
	public float maxRotationAngle = 50.0f;
	
	private Quaternion initialRotation;
	
	public virtual void Start () 
	{
		initialRotation = transform.rotation;
		transform.RotateAround(Vector3.up,Mathf.Deg2Rad*maxRotationAngle/2);
		StartCoroutine("RotateLeft");
	}
	
	IEnumerator RotateRight()
	{
		float angle = 0;
		while(angle < Mathf.Deg2Rad*maxRotationAngle)
		{
			transform.RotateAround(Vector3.up,Mathf.Deg2Rad*rotationSpeed*Time.deltaTime);
			angle += Mathf.Deg2Rad*rotationSpeed*Time.deltaTime;
			yield return null;
		}
		StartCoroutine("RotateLeft");
	}
	
	IEnumerator RotateLeft()
	{
		float angle = 0;
		while(angle > -Mathf.Deg2Rad*maxRotationAngle)
		{
			transform.RotateAround(Vector3.up,-Mathf.Deg2Rad*rotationSpeed*Time.deltaTime);
			angle -= Mathf.Deg2Rad*rotationSpeed*Time.deltaTime;
			yield return null;
		}
		StartCoroutine("RotateRight");
	}
	// Update is called once per frame
	public virtual void Update () 
	{
		//
	}
}