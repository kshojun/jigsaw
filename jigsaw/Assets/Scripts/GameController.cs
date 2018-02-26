using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	public int row, col, countStep;
	public int rowBlank, colBlank;

	public int maxImageNum;
	private int maxNum;

	int sizeRow = 3, sizeCol = 3;
	int countPoint = 0;
	int countImageKey = 0;

	public bool canStart = false;
	bool isComplete;

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

		SetRandomNum();

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
		SetRandomImages("imagekey");

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
		if (this.canStart && !this.isComplete) {
			this.canStart = false;
			if (imageOfPictureMatrix[row, col] != null && imageOfPictureMatrix[row, col].name.CompareTo("blank") != 0 && CanExchange(row, col)) {
				ExchangeImage();
				PlaySound();
				CheckComplete();
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

	void SetRandomNum()
	{
		this.maxNum = Random.Range(1, this.maxImageNum + 1);
	}
	
	bool CanExchange(int row, int col)
	{
		if (rowBlank != row && colBlank == col && Mathf.Abs(rowBlank - row) == 1) {
			return true;
		} else if (rowBlank == row && colBlank != col && Mathf.Abs(colBlank - col) == 1) {
			return true;
		} else {
			return false;
		}
	}

	void SetRandomImages(string tag)
	{
		// タグつけてまとめて取得
		GameObject[] g = GameObject.FindGameObjectsWithTag(tag);
		// スプライトをランダム取得
		Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/jigsaw" + this.maxNum.ToString());

		// スプライトを置換
		for (int i = 0; i < g.Length; i++) {
			if (g[i].name.CompareTo("blank") != 0) {
				int n = int.Parse(g[i].name) - 1;
				g[i].GetComponent<SpriteRenderer>().sprite = sprites[n];
			}
		}
	}

	void ImageOfPictureManager()
	{	
		SetRandomImages("imageofpicture");

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

	void CheckComplete()
	{
		int cnt = 0;
		for (int r = 0; r < sizeRow; r++) {
			for (int c = 0; c < sizeCol; c++) {
				if (imageKeyMatrix[r, c].name == imageOfPictureMatrix[r,c].name) {
					cnt++;
				}
			}
		}

		if (cnt == 9) {
			Debug.Log("complete!");
			this.isComplete = true;
		}
	}

	void PlaySound()
	{
		AudioSource source = GameObject.Find("sound").GetComponent<AudioSource>();
		AudioClip clip = source.clip;
		source.PlayOneShot(clip);
	}
}
