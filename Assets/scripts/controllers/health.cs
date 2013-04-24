using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {
	
	
	private GameObject ai;
	public float hit_point = 0;
	// Use this for initialization
	
	
	void Awake()
	{
		ai = GameObject.Find("/manager/intelligence");
	}
	
	void init_health (float health)
	{
		hit_point = health;
	}
	
	void receive_damage(float damage)
	{
		if ( hit_point > 0) 
			{
				print(damage);
				hit_point = hit_point - damage;
			}
		else 
			{
				Destroy(this.gameObject);
				//ai.SendMessage("update_enemy_list", damage);
			}
	}
	
	IEnumerator how_healthy()
		{
			return null;
		}

}
