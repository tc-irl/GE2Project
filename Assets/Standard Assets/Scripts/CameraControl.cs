using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public Texture2D crosshair;
	public GUIStyle style;
	private bool showLabel = false;
	public Camera ironManFP, ironManTP, ironFlight, eyeBotFP, eyeBotShoots, jetIncoming, jetFP,jetTP,turretCam;
	public Transform eyebot, jet, laser, turretA, turretB;
	public Transform originalFP;
	public Vector3 toBot,toIron,toJet, toJet2, between, between2,toTurret,toTurret1;
	private float distToJet, distToLaser, distToTurretA;
	private bool doOnce = true;
	private bool playOnce = true;
	public GameObject iron, iron2,turretCamObj;


	private Rect position;
	// Use this for initialization

	void Start () 
	{
		ironManTP.camera.enabled = true;
		ironManFP.camera.enabled = false;
		ironFlight.camera.enabled = false;
		eyeBotFP.camera.enabled = false;
		eyeBotShoots.camera.enabled = false;
		jetIncoming.camera.enabled = false; 
		jetFP.camera.enabled = false; 
		jetTP.camera.enabled = false;
		turretCam.camera.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		toBot = eyebot.position - eyeBotShoots.transform.position;

		if(iron!= null)
		{
			toIron = iron.transform.position - eyeBotShoots.transform.position;
		}

		toJet = jet.transform.position - jetIncoming.transform.position;
		//toTurret = turretA.transform.position - turretCamObj.transform.position;
		toTurret = turretA.transform.position - ironManFP.camera.transform.position;

		distToJet = Vector3.Distance(jet.transform.position, jetIncoming.transform.position);
		distToLaser = Vector3.Distance (laser.transform.position, jet.transform.position);
		distToTurretA = Vector3.Distance (new Vector3(iron2.transform.position.x,
		                                              turretA.transform.position.y,
		                                              iron2.transform.position.z)
		                                              ,turretA.transform.position);
		
		
		between = (toBot + toIron) / 2; 

		if(eyeBotShoots.camera.enabled == true)
		{
			eyeBotShoots.camera.transform.rotation = Quaternion.Slerp (eyeBotShoots.camera.transform.rotation, Quaternion.LookRotation (between), 3.0f * Time.deltaTime);
		}

		if(distToJet < 400)
		{
			if(doOnce)
			{
				ironFlight.camera.enabled = false;
				jetIncoming.camera.enabled = true; 
				Destroy(iron);
				iron2.SetActive(true);
				audio.Play();
				doOnce = false;
			}

			jetIncoming.camera.transform.rotation = Quaternion.Slerp (jetIncoming.camera.transform.rotation, Quaternion.LookRotation (toJet), 3.0f * Time.deltaTime);
		}

		if(distToLaser < 130)
		{
			ironFlight.camera.enabled = true;
			jetIncoming.camera.enabled = false;
		}

		if(distToTurretA < 300)
		{

			ironManFP.camera.enabled = true;
			ironFlight.camera.enabled = false;
			
			if(distToTurretA < 200)
			{

				ironManFP.camera.transform.rotation = Quaternion.Slerp (ironManFP.camera.transform.rotation, Quaternion.LookRotation (toTurret), 3.0f * Time.deltaTime);

				Shoot.fire = true;

			}

			//turretCam.camera.transform.rotation = Quaternion.Slerp (turretCam.camera.transform.rotation, Quaternion.LookRotation (between2), 3.0f * Time.deltaTime);
		}

		//turretCam.camera.transform.rotation = Quaternion.Slerp (turretCam.camera.transform.rotation, Quaternion.LookRotation (toIron2), 3.0f * Time.deltaTime);
		if(ironManFP.camera.enabled == true)
		{
			showLabel = true;
		}
		else
		{
			showLabel = false;
		}
	}

	void OnGUI() 
	{
		position = new Rect((Screen.width - crosshair.width) / 2, (Screen.height - 
		                                                                  crosshair.height) /2, crosshair.width, crosshair.height);
		if (showLabel) 
		{
			GUI.Label (new Rect (10, 500, 100, 520), "+100",style);
			GUI.Label (new Rect (10, 10, 100, 30), "X Coordinate: " + iron2.transform.position.x,style);
			GUI.Label (new Rect (10, 40, 100, 60), "Y Coordinate: " + iron2.transform.position.y,style);
			GUI.Label (new Rect (10, 70, 100, 90), "Z Coordinate: " + iron2.transform.position.z,style);

			GUI.DrawTexture(position,crosshair);
	
		} 
	}
}