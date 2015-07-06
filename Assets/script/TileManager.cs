using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

	public List<GameObject> tiles;
	public int length;
	public GameObject player;

	[Range(0, 50)]
	public int whiteBalance;
	[Range(0, 50)]
	public int blackBalance;
	[Range(0, 100)]
	public int holeBalance;

	// Use this for initialization
	void Start () {
		StartCoroutine (creteTiles ());
	}

	IEnumerator creteTiles ()
	{
		Debug.Log ("createTiles end");

		for (int n = 0; n < length; n++) {
			for (int m = 0; m < length; m++) {
				TileFriction.TileType type;
				if (n == 0 && m == 1) {
					type = TileFriction.TileType.TileTypeNormal;
				} else if (n == length - 1 && m == length - 1) {
					type = TileFriction.TileType.TileTypeGoal;
				} else {
					int typeIndex = Random.Range(0, 100);
					if (typeIndex <= whiteBalance) {
						type = TileFriction.TileType.TileTypeWhite;
					} else if (typeIndex > 50 && typeIndex < 50 + blackBalance) {
						type = TileFriction.TileType.TileTypeBlack;
					} else {
						int holeIndex = Random.Range(0, 100);
						if (holeIndex > holeBalance) {
							type = TileFriction.TileType.TileTypeNormal;
						} else {
							continue;
						}
					}
				}
				int index = Random.Range(0, tiles.Count - 1);
				float y = Random.Range(0, 1.0f);
				GameObject tile = tiles[index];
				Vector3 pos = new Vector3(n * tile.transform.localScale.x, y, m * tile.transform.localScale.z);
				GameObject t = (GameObject)Instantiate (tile, pos, Quaternion.identity);
				TileFriction friction = t.GetComponent<TileFriction>();
				friction.player = player;
				friction.tileType = type;
			}
		}
		Debug.Log ("createTiles end");
		yield break;
	}

	// Update is called once per frame
	void Update () {
	}
}
