using UnityEngine;
using System.Collections;


public class EnemyAI : MonoBehaviour 
{
	public Transform targetTransform;
	public float rotationSpeed = 3.0f;
	public Vector3 toPlayer;
	public float dist;

	public virtual void Start () 
	{
		// Do something
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		toPlayer = targetTransform.position - transform.position;
		dist = Vector3.Distance (targetTransform.position, transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toPlayer), rotationSpeed * Time.deltaTime);
	}
}