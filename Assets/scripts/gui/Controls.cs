using UnityEngine;
 
public class Controls : MonoBehaviour
{
	private float LevelArea = 5.2f;    
    public int ScrollArea = 45;
    public int ScrollSpeed = 2;
    public int DragSpeed = 2;
 	public float MoveSpeedDamper = 0.02f;
    public int ZoomSpeed = 3;
    public float ZoomMin = 1;
    public float ZoomMax = 1.25f;
	public Texture2D cursorImage;
	public bool GameCursor = false;
	//------------------------------------------------------------------------
	// Components
	//------------------------------------------------------------------------

	void OnGUI()
	{
	  if (GameCursor == true) {GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 32, 32), cursorImage);}
	}

    void Start()
    {
    	if (GameCursor == true) { Screen.showCursor = false; }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Init camera translation for this frame.
        var translation = Vector3.zero;

 		// Rotate camera by mouse
 
        // Move camera with arrow keys
        translation -= new Vector3(Input.GetAxis("Horizontal") * DragSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * DragSpeed * Time.deltaTime);
 
        // Move camera with mouse
            // Move camera if mouse pointer reaches screen borders
            if (Input.mousePosition.x < ScrollArea)
            {
                translation -= Vector3.right * -ScrollSpeed * Time.deltaTime;
            }
 
            if (Input.mousePosition.x >= Screen.width - ScrollArea)
            {
                translation -= Vector3.right * ScrollSpeed * Time.deltaTime;
            }
 
            if (Input.mousePosition.y < ScrollArea)
            {
                translation -= Vector3.forward * -ScrollSpeed * Time.deltaTime;
            }
 
            if (Input.mousePosition.y > Screen.height - ScrollArea)
            {
                translation -= Vector3.forward * ScrollSpeed * Time.deltaTime;
            }
        // Keep camera within level and zoom area
        var desiredPosition = camera.transform.position + translation;
        if (desiredPosition.x < -LevelArea || LevelArea < desiredPosition.x)
        {
            translation.x = 0;
        }
        if (desiredPosition.y < ZoomMin || ZoomMax < desiredPosition.y)
        {
            translation.y = 0;
        }
        if (desiredPosition.z < -LevelArea || LevelArea < desiredPosition.z)
        {
            translation.z = 0;
        }
 
        // Finally move camera parallel to world axis
        camera.transform.position += translation;
    }
}
