using UnityEngine;
using System.Collections;

public class MenuOverlay : MonoBehaviour {

private bool LevelPause = false;

	void OnGUI ()
		{
			overlayMenu();
		}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
		{
			overlayMenuTrigger();
		}
		
		
	void overlayMenuTrigger()
		{
			if(Input.GetKeyUp("escape")) 
				{
				if(Time.timeScale != 0)
					{
						LevelPause = true;
						Time.timeScale = 0;
//						Debug.Log(LevelPause);
					}
				else if (Time.timeScale == 0)
					{
						LevelPause = false;
						Time.timeScale = 1;
//						Debug.Log(LevelPause);
					}
				}
		}
		
	void overlayMenu()
		{
		if (LevelPause == true)
			{
				GUI.BeginGroup(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 300, 400, 550));
			        GUI.Box(new Rect(0, 0, 400, 550), "");	        
			   			if(GUI.Button(new Rect( 200-125, 50, 250, 75), "Return to game"))
		   					{
		   						LevelPause = false;
		   						Time.timeScale = 1;
			   				}
		   				
				        if(GUI.Button(new Rect(200-125, 175, 250, 75), "Main Menu"))
				        	{
				        		Application.LoadLevel(0);
				        		Time.timeScale = 1;
			    	    	}
			        	
			        	if(GUI.Button(new Rect(200-125, 300, 250, 75), "Restart"))
			        		{
			        			Time.timeScale = 1;	
				       			Application.LoadLevel(1);
				        	}

				        if(GUI.Button(new Rect(200-125, 425, 250, 75), "Exit Game"))
				        	{
			    	    		Application.Quit();
			        		}
		        
	    	    GUI.EndGroup();
			}
		}
}
