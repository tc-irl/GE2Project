using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public Texture2D crosshair;
	public GUIStyle style;
	private bool showLabel = false;
	public Camera ironManFP, ironManTP, eyeBotFP, eyeBotShoots, jetIncoming; 
	private int count = 0;

	private Rect position;
	// Use this for initialization
	void Start () 
	{
		ironManTP.camera.enabled = true;
		ironManFP.camera.enabled = false;
		eyeBotFP.camera.enabled = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.C)) 
		{
			if(count == 0)
			{
				count++;
	
				ironManTP.camera.enabled = false;
				ironManFP.camera.enabled = true;
				eyeBotFP.camera.enabled = false;

				showLabel = true;

			}
			else if(count == 1)
			{
				count = 2;

				ironManTP.camera.enabled = false;
				ironManFP.camera.enabled = false;
				eyeBotFP.camera.enabled = true;
				showLabel = false;
			}
			else
			{
				count = 0;

				ironManTP.camera.enabled = true;
				ironManFP.camera.enabled = false;
				eyeBotFP.camera.enabled = false;
			}
		}
	}

	void OnGUI() 
	{
		position = new Rect((Screen.width - crosshair.width) / 2, (Screen.height - 
		                                                                  crosshair.height) /2, crosshair.width, crosshair.height);
		if (showLabel) 
		{
			GUI.Label (new Rect (10, 500, 100, 520), "+100",style);
			GUI.Label (new Rect (10, 10, 100, 30), "X Coordinate: " + transform.position.x,style);
			GUI.Label (new Rect (10, 40, 100, 60), "Y Coordinate: " + transform.position.y,style);
			GUI.Label (new Rect (10, 70, 100, 90), "Z Coordinate: " + transform.position.z,style);

			GUI.DrawTexture(position,crosshair);
	
		} 
	}
}