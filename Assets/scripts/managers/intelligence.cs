using UnityEngine;
using System.Collections;

public class intelligence : MonoBehaviour {
	
	//private float osq = .833F;

	// default vectors
	// X -10.4165
	// Z -0.1
	
	// Y -2.0825
	// Y -1.2495
	// Y -0.4165
	// Y  0.4165
	// Y  1.2495
	
	Hashtable waypoints = new Hashtable();
	Hashtable _current_objects = new Hashtable();
	Hashtable towerList;
	
	bool weapon_is_active = false; 
	public float gridSpacing = .833F;
	private GameObject instance;
	public float scan_frequency = 0.1F; 
	public string start_funds = "1000";
	public int time_per_wave = 120;
	public GameObject test_tank;
	// Units
	public int limit_of_enemeies = 60;
	private int current_quantity_of_enemies = 1;
	public int simultaneous_limit_of_enemies = 10;
	
	//private int dead_enemies = 0;
	
	public float min_time_respawn = 15.0F;
	public float max_time_respawn = 45.0F;
	
	private GameObject[] enemies;
	private GameObject[] towers;
	
	private GameObject	current_line;
	public int enemy_damage;
	public int allied_damage;
	private float distance_in_cells;
	public float enemy_speed;
	
	public int health;
	
	public float allied_speed_weapon;
	public float enemy_speed_weapon;
	
	public float enemy_distance_visibility;	
	public float allied_distance_visibility;
	
	public float distance_of_path;
	private float current_distance;
	
	// Booleans
	private bool instance_created;
	private bool pause_and_wait_some_time;
	
	int randNum;
	
	void Start()
		{
			StartCoroutine("create_wave");
			StartCoroutine("nearest_object");
		}
	
	void Awake() 
		{
			GameObject funds = GameObject.Find("funds_counter");
			enemies = GameObject.FindGameObjectsWithTag("enemy");
			towers = GameObject.FindGameObjectsWithTag("tower");
			
			// Set waypoints
			waypoints[1] = new Vector3(-10.4165F, -1.933F, 0.1F);
			waypoints[2] = new Vector3(-10.4165F, -1.1F  , 0.1F);
			waypoints[3] = new Vector3(-10.4165F, -0.2675F, 0.1F);
			waypoints[4] = new Vector3(-10.4165F,  0.566F, 0.1F);
			waypoints[5] = new Vector3(-10.4165F,  1.399F, 0.1F);
			
			funds.guiText.text = start_funds;		
			//InvokeRepeating("LaunchProjectile", 2, 0.3F);
		}
	
		
		IEnumerator create_wave() { yield return StartCoroutine(create_instance(Random.Range(min_time_respawn, max_time_respawn))); }
		IEnumerator create_instance(float waitTime)
			{	
				while(current_quantity_of_enemies <= limit_of_enemeies && current_quantity_of_enemies <= simultaneous_limit_of_enemies)
					{
						yield return new WaitForSeconds(waitTime);
						int randNum = Random.Range(1, 6);
						
						instance = Instantiate(Resources.Load("Prefabs/tank")) as GameObject;
						instance.name = "enemy";
			 			//instance  = Instantiate(test_tank) as GameObject;
						instance.transform.localPosition =  (Vector3) waypoints[randNum]; // Set waypoint by random
						
						// +1
						instance.AddComponent<health>();
						instance.AddComponent<weapons>();
			
						instance.SendMessage("init_health", health);
			
						instance.animation["shoot"].layer = 1;
						instance.animation["move"].layer = 2;
	
						//instance.transform.localScale = new Vector3(1.25F, 1.25F, 1.25F);
						
			 
						current_line = new GameObject("current_line");
						current_line.tag  = "line_"+randNum;
						current_line.transform.parent = instance.transform;
			
						enemies = GameObject.FindGameObjectsWithTag("enemy");
						current_quantity_of_enemies++;
					}	
			}
		
		IEnumerator	nearest_object()
			{
				while(true)
					{
						yield return new WaitForSeconds(scan_frequency);
						update_object_list_on_a_map();
						towers = GameObject.FindGameObjectsWithTag("tower");
			
						foreach (GameObject obj in enemies)
							{
								string enemy_line = (obj.transform.Find("current_line").tag);
								foreach (GameObject obj2 in towers)
									{					
										bool enemy_weapon_enabled = obj.GetComponent<weapons>().weapon_status;
										bool allied_weapon_enabled = obj2.GetComponent<weapons>().weapon_status;
					
										string tower_line = (obj2.transform.Find("current_line").tag);
					
										if ( enemy_line == tower_line)
											{
												distance_in_cells = Mathf.Abs(Mathf.Round((obj.transform.position.x - obj2.transform.position.x) / gridSpacing)-1);
												//print ("Distance in cells is:" + distance_in_cells);
						
												if (enemy_distance_visibility >= distance_in_cells && enemy_weapon_enabled == false )
													{	
														_current_objects[1] = obj;
														_current_objects[2] = obj2;
														StartCoroutine("shoot", _current_objects);
													}
												
												if (allied_distance_visibility >= distance_in_cells && allied_weapon_enabled == false)
													{
														_current_objects[1] = obj2;
														_current_objects[2] = obj;
														StartCoroutine("shoot", _current_objects);
													}
						
												if (obj.transform.position.x - obj2.transform.position.x >= .15F && obj.transform.position.x - obj2.transform.position.x <= .5F)
													{
														obj.animation.Stop("move");
													}

											}

									}
							}
					}
			}
	
		IEnumerator shoot  (Hashtable objects) { yield return StartCoroutine("_shoot", objects); }
		IEnumerator _shoot (Hashtable objects)
			{
				GameObject unit = objects[1] as GameObject;
				GameObject target = objects[2] as GameObject;
		
				unit.SendMessage("switch_weapon", true);
				
				float speed = 0;
				float damage = 0;
		
				while( target != null)
					{
						yield return new WaitForSeconds(speed);
			
						if ( unit != null && unit.name == "enemy")
							{		
								speed = enemy_speed_weapon;
								damage = enemy_damage;
							}
			
						else if (unit != null && unit.name == "tower")
							{
								speed = allied_speed_weapon;
								damage = allied_damage;
							}
						
						if (unit != null) unit.animation.CrossFade("shoot");
						TakeDamage(target, damage);
					}
				
				if ( unit != null && unit.name == "enemy") unit.animation.Play("move");
				if ( unit != null) 
					{
						unit.animation.Stop("shoot");
						unit.SendMessage("switch_weapon", false);
					}
				
				//StopCoroutine("_shoot");
			}
	
	void TakeDamage(GameObject target, float damage)
		{
			if (target != null) target.SendMessage("receive_damage", damage);
			else enemies = GameObject.FindGameObjectsWithTag("enemy");
			//print (enemy.name + ": make damage"+ damage);
		}
	
	//void move
	void update_object_list_on_a_map()
	{
		enemies = GameObject.FindGameObjectsWithTag("enemy");
		towers = GameObject.FindGameObjectsWithTag("tower");
	}
	
	void updateTowerList(Hashtable placedObject)
		{
			towerList = placedObject;
		}

	void Update () 
		{	
			if(enemies != null)
				{
					foreach (GameObject obj in enemies)
						{
							obj.transform.localPosition += new Vector3(Time.deltaTime*enemy_speed, 0, 0);
							if ( obj != null && obj.animation.IsPlaying("move")) obj.transform.localPosition += new Vector3(Time.deltaTime*enemy_speed, 0, 0);
						}
				}
		}
}
