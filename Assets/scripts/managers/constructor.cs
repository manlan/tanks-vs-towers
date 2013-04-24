using UnityEngine;
using System.Collections;

public class constructor : MonoBehaviour {

  // Public events and others
    public float gridSpacing = .833F;
	public Vector3 field;
	private string entity;
	public float health;
  // Triggers
	private bool toggle;
	private bool NewObject;
	
  // Objects
	private GameObject obj;
	
 // Coadjutor objects
	private GameObject co_sample;
	private GameObject co_helper;
	private GameObject current_line;

  // Managers
	private GameObject ai;
	private effects fx;
	
  // Vectors
	private Vector3 coord;
	private Vector3 coord2;
	private Vector3 position;
	private Vector3 co_sample_position;
	private Vector2 pos;
	
	// Arrays
	Hashtable placedObjects = new Hashtable(); 		// Placed objects on a grid;
	Hashtable currentLine = new Hashtable();
	Hashtable grid_offset = new Hashtable();
	Hashtable co_helper_offset = new Hashtable();	
	//Hashtable sample_offset = new Hashtable();	
	
	void Awake()
	{
		// Connect AI manager
		ai = GameObject.Find("/manager/intelligence");
		fx = GameObject.Find("/manager/effects").GetComponent<effects>() as effects;
		
		currentLine["1.5"]  = "5";
		currentLine["0.5"]  = "4";
		currentLine["0"] 	= "3";
		currentLine["-1"] 	= "2";
		currentLine["-1.5"] = "1";
		
		// Offset for cursor helper
		co_helper_offset["tower"] = new Vector3(5F, 25F, 0.1F);
		co_helper_offset["oil"] =   new Vector3(180F, 50F, 0.1F);
		
		// Offset for grid
		grid_offset["co_sample"] = new Vector3(0.0833F, 0.0833F, 0.1F);
		grid_offset["tower"]  = new Vector3(0.0833F, 0.0833F, 0.1F);
		grid_offset["oil"]    = new Vector3(-1.3F, 0.4F, 0.1F);
	}
	
	void moveByGrid()
		{
			position = obj.transform.position;
			co_sample_position = co_sample.transform.position;
		
			Vector3 offset = (Vector3)  grid_offset[entity];
			Vector3 _offset = (Vector3) grid_offset["co_sample"];
			//Vector3 _offset = (Vector3) grid_offset[entity];
		
			// X AND Y DEFAULT OFFSET IS 0.0833F
			position.x = (Mathf.Round(position.x / gridSpacing) * gridSpacing)-offset.x;
			position.y = (Mathf.Round(position.y / gridSpacing) * gridSpacing)-offset.y;
		
			co_sample_position.x = (Mathf.Round(co_sample_position.x / gridSpacing) * gridSpacing)-_offset.x;
			co_sample_position.y = (Mathf.Round(co_sample_position.y / gridSpacing) * gridSpacing)-_offset.y;
		
			//print(co_sample.transform.position);
		
			co_sample.transform.position = co_sample_position;
			obj.transform.position = position;
		}

	void createObject(string _entity)
		{
			entity = _entity;

			NewObject = true;
		
			string path = "Prefabs/" + entity;
		
			Screen.showCursor = false;
			
			// Create cursor object
			co_helper = Instantiate(Resources.Load(path)) as GameObject;
			co_helper.transform.localScale = new Vector3(1.25F, 1.25F, 1.25F);
			co_helper.name = "cursor helper";
			co_helper.tag = "co_helper";
		
			// Create sample object for hashtable
			co_sample = Instantiate(Resources.Load("Prefabs/sample")) as GameObject;
			co_sample.transform.localScale = new Vector3(1.25F, 1.25F, 1.25F);
			co_sample.name = "co_sample";
			co_sample.tag = "sample";
		
			// Create origin object for game
			obj = Instantiate(Resources.Load(path)) as GameObject;
			obj.transform.localScale = new Vector3(1.25F, 1.25F, 1.25F);
			obj.name = entity;
			obj.tag = "_tower";
			
			fx.setAlpha(obj);
			moveObject();
		}
	
	void moveObject()
		{
			Vector3 _coord = (Vector3) co_helper_offset[entity];
		
			coord2 = Input.mousePosition;
			coord2.z = 0.1F;
			coord2.x -= _coord.x;
			coord2.y -= _coord.y;
			
			pos = Camera.main.ScreenToWorldPoint(coord2);
			co_helper.transform.position = Camera.main.ScreenToWorldPoint(coord2);	

			if (pos.y <= 1.5f && pos.y >= -2f && pos.x <= 7.3 && pos.x >= -10)
			{
				coord = Input.mousePosition;
				coord.z = 0.2F;
				
				co_sample.transform.position = Camera.main.ScreenToWorldPoint(coord);
				obj.transform.position = Camera.main.ScreenToWorldPoint(coord);
				moveByGrid();
			}
		}
	
	void placeObject()
		{
		
			if(placedObjects.ContainsValue(co_sample.transform.position)) 
				{
    				toggle = true;
					print("if");
				}
			else
			    {
					float _line = Mathf.Round(co_sample.transform.position.y * 2) / 2;
					string line = _line.ToString();

					current_line = new GameObject("current_line");
					current_line.tag  = "line_"+currentLine[line];
					current_line.transform.parent = obj.transform;

					print("objectPlaced");
					obj.tag = "tower";
			

					
					placedObjects.Add(co_sample.transform.position,co_sample.transform.position);			
						
					ai.SendMessage("updateTowerList", placedObjects);
			
					Screen.showCursor = true;
					NewObject = false; 
					fx.unsetAlpha(obj);
			
					obj.AddComponent<health>();
					obj.AddComponent<weapons>();
					obj.SendMessage("init_health", health);
			
					print(co_sample.transform.position);
					Destroy(co_helper);
					Destroy(co_sample);
				}
		}
	
	void Update()
		{
			if (NewObject == true) moveObject();
			
			if (NewObject == true && Input.GetMouseButtonUp(0))
				{
					toggle = !toggle;
					
					if (toggle){}
					else placeObject(); 
						
				}
		}
	
}