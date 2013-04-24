using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

//using System.Xml.Serialization; 
	
public class sprites : MonoBehaviour {
	
	float x;
	float y;
	float offsetX;
	float offsetY;
	int frame;
	int _frame;
	public  Texture effect;
	public GameObject _obj;

	private ArrayList pos = new ArrayList();
	private TextAsset file;
	private XmlDocument xmldoc = new XmlDocument();

	// Use this for initialization
	void Awake () {
		
		
		float framesPerSecond = 10.0F;
		
		LoadXML();
		
		StartCoroutine("move_sprite");
	}
	
	
	IEnumerator move_sprite()
		{
			int total_frame = 29;
			int frame = 0;
			while(true)
			{
				coords locate = pos[frame] as coords;	

				Vector2 size = new Vector2(locate.x, locate.y);
				Vector2 offset = new Vector2 (locate.offsetX, locate.offsetY);

				_obj.renderer.material.SetTextureScale("_MainTex", size);
				_obj.renderer.material.SetTextureOffset ("_MainTex", offset);			
					
				yield return new WaitForSeconds(0.02F);
				if (frame < total_frame) 
					{
						++frame;
					}
				else 
					{
						frame = 0;
					}
			}
		}
	
	void LoadXML()
	{
		file = Resources.Load("sprites/cfg/" + effect.name) as TextAsset;
		XmlTextReader reader = new XmlTextReader(new StringReader(file.text));		
		
		frame = 1;
		while(reader.Read())
			{
				switch (reader.Name)
					{
						case "x": 		x 		= float.Parse (reader.ReadString()); 	break;
						case "y": 		y 		= float.Parse (reader.ReadString()); 	break;
						case "offsetX": offsetX = float.Parse (reader.ReadString()); 	break;
						case "offsetY": offsetY = float.Parse (reader.ReadString()); 	break;
						case "no" : 	_frame 	= int.Parse(reader.ReadString()); if ( _frame == frame) { pos.Add(new coords(x, y, offsetX, offsetY)); frame++;} break;
					}	
			}
	}
	
	
}

	class coords 
		{
			public float x;
			public float y;
  			public float offsetX; 
  			public float offsetY;
  			
  			public coords(float v1, float v2, float v3, float v4) 
				{
    				x		= v1; 
    				y 		= v2; 
					offsetX = v3; 
					offsetY = v4;	
  				}
			
		}

