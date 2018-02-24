using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour 
{
	public GameObject target;
	public bool startMove = false;

	// Update is called once per frame
	void Update () 
	{
		if (startMove) {
			startMove = false;
			this.transform.position = target.transform.position;
		}	
	}
}
