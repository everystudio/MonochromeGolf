using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager mInstance;

	public enum STATE {
		NONE		= 0,
		GOAL		,
		GAMEOVER 	,
		MAX 		,
	}
	public STATE m_eState;
	public STATE State{
		get{return m_eState;}
		set{m_eState = value;}
	}
	
	private GameManager () {
	}
	
	public static GameManager Instance {
		
		get {
			
			if( mInstance == null ){
				//mInstance = new GameManager();
				GameObject obj = new GameObject();
				obj.name = "GameManager";
				obj.AddComponent<GameManager>();
				mInstance = obj.GetComponent<GameManager>();
			}
			
			return mInstance;
		}
	}
	
	public int score;

	public void GameOver() {
		m_eState = STATE.GAMEOVER;
		return;
	}

	public void GameClear() {
		Debug.Log("GameClear");
		Debug.Log(GameManager.Instance.State);
		GameManager.Instance.m_eState = STATE.GOAL;
		Debug.Log(GameManager.Instance.State);
	}

	public void NextScene(){
		Application.LoadLevel("title");
		return;
	}


}



/*



public class TimeController : MonoBehaviour
{
    public int time;
    [SceneName]
    public string nextLevel;

    public GUIText timer;

   

    void Update()
    {
        int remainingTime = time - Mathf.FloorToInt(Time.timeSinceLevelLoad * 2.5f);

        if (0 <= remainingTime)
        {
            timer.text = remainingTime.ToString("000");
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                StartCoroutine(LoadNextLevel());
                enabled = false;
            }
        }
    }

    private IEnumerator LoadNextLevel()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            player.SendMessage("TimeOver", SendMessageOptions.DontRequireReceiver);
        }

        yield return new WaitForSeconds(3);

        Application.LoadLevel(nextLevel);
    }
}

*/







