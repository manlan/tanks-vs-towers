using UnityEngine;
using System.Collections;

public class newObject : MonoBehaviour {
	
	private GameObject constructor;
	private string obj;
	// Use this for initia	lization
	void Start () 
		{
			constructor = GameObject.Find("/manager/constructor");
			obj = this.name;
		}
	

	void OnMouseDown() 
	{
		constructor.SendMessage("createObject", obj);
	}
}
