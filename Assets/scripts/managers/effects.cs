using UnityEngine;
using System.Collections;

public class effects : MonoBehaviour {
	 
	
	private Renderer[] allChildren; 
	
	// Set transparent
	public void setAlpha (GameObject obj) {
			
			obj.renderer.material.shader =  Shader.Find("Particles/Alpha Blended");			
			allChildren = obj.GetComponentsInChildren<Renderer>();
			foreach (Renderer child in allChildren) 
				{
    				child.renderer.material.shader = Shader.Find("Particles/Alpha Blended");
					child.renderer.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0.25f));
				}}
		
	// Unset transparent
	public void unsetAlpha (GameObject obj) {
			
			obj.renderer.material.shader =  Shader.Find("Unlit/Transparent");			
			allChildren = obj.GetComponentsInChildren<Renderer>();
			foreach (Renderer child in allChildren) 
				{
    				child.renderer.material.shader = Shader.Find("Unlit/Transparent");
					//child.renderer.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0.25f));
				}}
	
	
}
