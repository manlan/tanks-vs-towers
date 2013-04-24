using UnityEngine;
using System.Collections;

public class game_rules : MonoBehaviour {
	
	private GameObject lives;
	public string start_lives = "20";
	// Use this for initialization
	void Awake ()
	{
		lives = GameObject.Find("lives_counter");	
		lives.guiText.text = start_lives;
	}
	
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
