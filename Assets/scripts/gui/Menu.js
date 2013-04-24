#if UNITY_STANDALONE_OSX
#if UNITY_STANDALONE_WIN

var fadeDuration:float;
var initialDelay:float;
var timeLeft:float;

var start : GameObject; 
var exit : GameObject; 


function Start()
{
	Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
}
   
function Awake ()
{
   timeLeft = fadeDuration;	
}


function Update()
	{
	
	if(Input.GetKeyUp("space"))
		print("space key was released");
		
	 if (initialDelay > 0){
      initialDelay = initialDelay-Time.deltaTime;
    } 
   	 else 
   	 {
         if ( timeLeft > 0)
         	fade(false);
         	
         else
         	menu();
      }

}



function menu()
{
	start.guiText.enabled = true;
	exit.guiText.enabled = true;	
	
}

function fade(direction:boolean){
   scr = GameObject.Find("Logo");
   var alpha;
   if (direction){
      if (scr.guiTexture.color.a < 0.5){
         timeLeft = timeLeft - Time.deltaTime;
         alpha = (timeLeft/fadeDuration);
         scr.guiTexture.color.a = 0.5-(alpha/2);
      } else {
         timeLeft = fadeDuration;
      }
   } else {
      if (scr.guiTexture.color.a > 0){
         timeLeft = timeLeft - Time.deltaTime;
         alpha = (timeLeft/fadeDuration);
         scr.guiTexture.color.a = alpha/2;
      } else {
         timeLeft = fadeDuration;
      }
   }
}

#endif   
#endif   