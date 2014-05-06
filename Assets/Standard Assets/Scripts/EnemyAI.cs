using UnityEngine;
using System.Collections;


public class EnemyAI : MonoBehaviour 
{
	public Transform targetTransform;
	public float rotationSpeed = 3.0f;
	public Vector3 toPlayer;
	public float dist;
	protected CameraControl camControl;
	private GameObject c;
	
	public virtual void Start () 
	{
		c = GameObject.FindWithTag("camc"); //Get the players transform
		camControl = c.GetComponent <CameraControl>();
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		toPlayer = targetTransform.position - transform.position;
		dist = Vector3.Distance (targetTransform.position, transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (toPlayer), rotationSpeed * Time.deltaTime);
	}
}