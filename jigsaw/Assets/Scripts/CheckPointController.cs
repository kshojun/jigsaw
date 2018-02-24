using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour 
{
	public int row, col;
	GameController gameManager;

	// Use this for initialization
	void Start () 
	{
		gameManager = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	private void OnMouseDown()
	{
		gameManager.row = row;
		gameManager.col = col;
		gameManager.canStart = true;
	}
}
