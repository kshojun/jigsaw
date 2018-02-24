﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	public int row, col, countStep;
	public int rowBlank, colBlank;

	int sizeRow = 3, sizeCol = 3;
	int countPoint = 0;
	int countImageKey = 0;

	public bool startControl = false;
	public bool checkComplete;

	public List<GameObject> imageKeyList;
	public List<GameObject> imageOfPictureList;
	public List<GameObject> checkPointList;

	GameObject[,] imageKeyMatrix;
	GameObject[,] imageOfPictureMatrix;
	GameObject[,] checkPointMatrix;

	// Use this for initialization
	void Start () 
	{
		imageKeyMatrix = new GameObject[sizeRow, sizeCol];
		imageOfPictureMatrix = new GameObject[sizeRow, sizeCol];
		checkPointMatrix = new GameObject[sizeRow, sizeCol];

		ImageOfPictureManager();
		CheckPointManager();
		ImageKeyManager();
		SetBlank();
	}

	void SetBlank()
	{
		for (int r = 0; r < sizeRow; r++) {
			for (int c = 0; c < sizeCol; c++) {
				if (imageOfPictureMatrix[r, c].name.CompareTo("blank") == 0) {
					rowBlank = r;
					colBlank = c;
					break;
				}
			}
		}
	}

	void CheckPointManager()
	{
		for (int r = 0; r < sizeRow; r++) {
			for (int c = 0; c < sizeCol; c++) {
				checkPointMatrix[r, c] = checkPointList[countPoint];
				countPoint++;
			}
		}	
	}

	void ImageKeyManager()
	{
		for (int r = 0; r < sizeRow; r++) {
			for (int c = 0; c < sizeCol; c++) {
				imageKeyMatrix[r, c] = imageKeyList[countImageKey];
				countImageKey++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startControl) {
			startControl = false;
			if (imageOfPictureMatrix[row, col] != null && imageOfPictureMatrix[row, col].name.CompareTo("blank") != 0) {
				if ((rowBlank != row && colBlank == col) || (rowBlank == row && colBlank != col)) {
					ExchangeImage();
				}
			}
		}
	}

	void ExchangeImage()
	{
		GameObject temp;

		temp = imageOfPictureMatrix[rowBlank, colBlank];

		imageOfPictureMatrix[rowBlank, colBlank] = imageOfPictureMatrix[row, col];
		imageOfPictureMatrix[rowBlank, colBlank].GetComponent<ImageController>().target = checkPointMatrix[rowBlank, colBlank];
		imageOfPictureMatrix[rowBlank, colBlank].GetComponent<ImageController>().startMove = true;

		imageOfPictureMatrix[row, col] = temp;
		imageOfPictureMatrix[row, col].GetComponent<ImageController>().target = checkPointMatrix[row, col];
		imageOfPictureMatrix[row, col].GetComponent<ImageController>().startMove = true;

		rowBlank = row;
		colBlank = col;
	}

	void ImageOfPictureManager()
	{
		imageOfPictureMatrix[0, 0] = imageOfPictureList[0];
		imageOfPictureMatrix[0, 1] = imageOfPictureList[2];
		imageOfPictureMatrix[0, 2] = imageOfPictureList[5];
		imageOfPictureMatrix[1, 0] = imageOfPictureList[4];
		imageOfPictureMatrix[1, 1] = imageOfPictureList[1];
		imageOfPictureMatrix[1, 2] = imageOfPictureList[7];
		imageOfPictureMatrix[2, 0] = imageOfPictureList[3];
		imageOfPictureMatrix[2, 1] = imageOfPictureList[6];
		imageOfPictureMatrix[2, 2] = imageOfPictureList[8];
	}
}
