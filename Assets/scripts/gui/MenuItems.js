var isQuitEvent = false;
var isStartEvent = false;


function OnMouseEnter()
{
	guiText.material.color = Color(0.8, 0.22, 0, 1);
}

function OnMouseExit()
{
	guiText.material.color = Color.white;
}


function OnMouseUp()
{
	if(isQuitEvent)
	{
		Application.Quit();
	}
	
	if(isStartEvent)
	{
		Application.LoadLevel ("Battlefield");
	}
}