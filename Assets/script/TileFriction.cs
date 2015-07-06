using UnityEngine;
using System.Collections;

public class TileFriction : MonoBehaviour {

	[HideInInspector]
	public GameObject player;
	
	[HideInInspector]
	public enum TileType {
		TileTypeNormal,
		TileTypeWhite,
		TileTypeBlack,
		TileTypeGoal,
	}

	[HideInInspector]
	public TileType tileType;

	[Range(0, 1)]
	public float friction;

	void Start () {
		Color color;
		switch(tileType) {
		case TileType.TileTypeWhite:
			color = Color.white;
			break;
		case TileType.TileTypeBlack:
			color = new Color(0.2f, 0.2f, 0.2f);
			friction = 1;
			break;
		case TileType.TileTypeGoal:
			color = Color.red;
			break;
		default:
			color = Color.gray;
			break;
		}
		GetComponent<Renderer>().material.color = color;
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter() {
		/*
		if (tileType == TileType.TileTypeWhite) {
//			player.rigidbody.velocity = new Vector3(player.rigidbody.velocity.x * 10,
//			                                        player.rigidbody.velocity.y + 20,
//			                                        player.rigidbody.velocity.z * 10);
			player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity * 2;
		}
		*/
	}

	void OnCollisionStay() {

		/*
		if (!player.GetComponent<Rigidbody>().velocity.normalized.Equals(Vector3.zero)) {
			float x = Mathf.Abs (player.GetComponent<Rigidbody>().velocity.x) - friction;
			if (x < 0) {
				x = 0;
			}
			if (Mathf.Sign(player.GetComponent<Rigidbody>().velocity.x) < 0) {
				x = x * -1;
			}

			float z = Mathf.Abs (player.GetComponent<Rigidbody>().velocity.z) - friction;
			if (z < 0) {
				z = 0;
			}
			if (Mathf.Sign(player.GetComponent<Rigidbody>().velocity.z) < 0) {
				z = z * -1;
			}
			player.GetComponent<Rigidbody>().velocity = new Vector3 (x, player.GetComponent<Rigidbody>().velocity.y, z);
		} else if (tileType == TileType.TileTypeGoal) {
			GameManager.Instance.GameClear();
		}
		*/
	}
}
