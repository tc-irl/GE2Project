/**
 * 
 * Tutorials for this class: https://www.youtube.com/watch?v=NPlRbxJtKxE
 */

using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public Rigidbody projectile;
	public ParticleSystem explosion;
	private Rigidbody pel;
	public float speed = 10;
	public static bool fire = false;
	private int count = 1;
	private Vector3 toTurret;
	public Transform turret;
	public GameObject turretObj;
	private float dist,dist2;
	private bool playOnce = true;

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		if(fire == true)
		{
			if(count == 1)
			{
				pel = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;

				audio.Play();
				count++;
			}

			if(turretObj != null)
			{
				pel.transform.Translate(Vector3.forward * 50.0f * Time.deltaTime);
				toTurret = turret.transform.position - pel.transform.position;
				pel.transform.rotation = Quaternion.Slerp(pel.transform.rotation, Quaternion.LookRotation (toTurret), 2.0f * Time.deltaTime);

				dist = Vector3.Distance (turret.transform.position, pel.transform.position);
		
				if(dist < 30)
				{
					turretObj.audio.Play();
					explosion.Play();
			
					Destroy(turretObj);
				}

			}
		}
	}
}
