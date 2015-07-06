using UnityEngine;
using System.Collections;

public class GoalChecker : MonoBehaviour {

	void OnCollisionEnter() {
		Debug.Log ("GoalChecker.OnCollisionEnter");
		GameManager.Instance.GameClear ();
	}


}
