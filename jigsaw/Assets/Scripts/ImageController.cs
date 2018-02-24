using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour 
{
	public GameObject target;
	public bool startMove = false;
	GameController gameManager;

	// Use this for initialization
	void Start () 
	{
		gameManager = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startMove) {
			startMove = false;
			this.transform.position = target.transform.position;
			gameManager.checkComplete = true;
		}	
	}
}
