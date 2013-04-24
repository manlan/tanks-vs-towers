using UnityEngine;
using System.Collections;

public class grid : MonoBehaviour {
	
	//public bool	show_grid = false;

	public bool	show_grid_cursor = false;
	public Vector2 f_size;
	
	private Hashtable grid_array;
	private Hashtable occupied_cells;
	private Hashtable free_cells;
	
	private int cells_level = 1;
	private int cell_pos_in_level = 0;
	private Vector2 cell_pos;
	//private int allCells = field_size.x;
	
	void Awake()
	{
		GameObject map = GameObject.FindWithTag("terrain");

		// Calculate one cell size and dimensions X and Y on terrain
		float map_x = map.renderer.bounds.size.x;
		float map_y = map.renderer.bounds.size.z;
		float tile_x = map_x / (int) f_size.x;
		float tile_y = map_y / (int) f_size.y;
		
		// Get lower left corner of terrain
		Vector3 map_center = map.renderer.bounds.center;
		Vector3 map_local_position = map.transform.position;
		
		// map center must be Vector2(0,0 0,0)
		float start_x = (float) map_center.x/2 - map_x/2 + tile_x/2;
		float start_y = (float) -2.081;
		
		Vector3 starting_point =  new Vector3(start_x, start_y, 0.1F);
		
		float total_cells = f_size.x * f_size.y;
		print(start_x);
		print(tile_x);
		// create hashtable of grid
		for( int cur_cell = 1; cur_cell <= total_cells; ++cur_cell)
			{
				// backet counter for current level
				if (cell_pos_in_level < f_size.x) cell_pos_in_level++;
				else cell_pos_in_level = 1;
			
				// check current limit of level section and then add new level for grid array
				if ( cur_cell > (int) f_size.x * cells_level && cells_level < (int) f_size.y) cells_level++;
				
				//print(tile_x);
				print(start_x += tile_x*cell_pos_in_level);
				
				//grid_array.Add(cells_level, cell_pos_in_level);
				//print( cells_level + "." + cell_pos_in_level);
			}
	}
	
	void show_grid()
	{
	}
	
	
	void move_by_grid()
	{
	}
	
	
	void check_cell()
	{
	}
	
	void free_cell()
	{
	}
	
}
