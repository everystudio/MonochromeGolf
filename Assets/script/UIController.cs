using UnityEngine;
using System.Collections;
using System;

using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public float m_fShotPowerInterval = 2.0f;
	public float m_fWindowHalf = 700.0f;

	public Text m_txtStatus;

	public GameObject m_goShotPower;
	public Text m_txtPower;
	protected int m_iShotCount;

	public GameObject m_goResult;
	public Text m_txtResult;
	
	public GameObject m_goGotoTitle;
	public Text m_txtGotoTitlet;
	

	public Slider m_slShotPower;
	public Slider m_slShotAngle;

	public PlayerController m_csPlayer;
	public CameraController m_csCamera;

	// Use this for initialization
	void Start () {
		m_eStep 	= STEP.WAIT;
		m_eStepPre 	= STEP.MAX;

		m_iShotCount = 0;
		if( m_goShotPower == null ){
			m_goShotPower = GameObject.Find("shotPower");
		}
		m_goShotPower.SetActive( true );
		m_goShotPower.transform.localPosition = new Vector3( -180.0f , 180.0f , 0.0f );
		m_txtPower.text = "打数：" + m_iShotCount.ToString();

		m_goResult.transform.localPosition = new Vector3( m_fWindowHalf , 0.0f , 0.0f );
		m_txtResult.text = "けっかー";

		m_txtGotoTitlet.transform.localPosition = new Vector3( m_fWindowHalf , -60.0f , 0.0f );
	}

	public enum STEP {
		WAIT		,
		IDLE 		,
		SHOT_START	,
		BUTTON_RELEASE_CHECK,
		SHOT 		,
		KOROKORO 	,
		END			,

		GOAL		,
		GAMEOVER	,
		GAME_END	,

		RETRY_CHECK	,

		MAX			,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;
	public float m_fTimer;
	private float m_fShotPower;
	public float m_fPowerController = 1.0f;

	public float m_fShotAngle;
	public float m_fShotAngleValue;

	public bool m_bFinishStart = false;

	private void moveAngle( float _fVertical ){

		/*
		if (m_eStep != STEP.IDLE) {
			return;
		}
		*/

		float fDiff = _fVertical * 0.01f;

		m_fShotAngleValue += _fVertical;

		if( m_fShotAngleValue < 0 ){
			m_fShotAngleValue = 0.0f;
		}
		else if( 1.0f < m_fShotAngleValue ){
			m_fShotAngleValue = 1.0f;
		}
		m_slShotAngle.value = m_fShotAngleValue;
	}

	public void inputJoyStick( Vector2 _vec2 ){
		Debug.Log ("here");
		moveAngle (_vec2.y * 0.02f );
		return;
	}


	// Update is called once per frame
	void Update () {

		bool bInit = false;
		if( m_eStepPre != m_eStep ){
			m_eStepPre  = m_eStep;
			bInit = true;
			Debug.Log( m_eStep );
		}

		if( m_bFinishStart == false ){
			//Debug.Log( GameManager.Instance.State );
			if( GameManager.Instance.State == GameManager.STATE.GOAL ){
				m_bFinishStart = true;
				m_eStep = STEP.GOAL;
				Debug.Log("Clear");
			}
			else if( GameManager.Instance.State == GameManager.STATE.GAMEOVER ){
				m_bFinishStart = true;
				m_eStep = STEP.GAMEOVER;
				Debug.Log("GameOver");
			}
			else {

			}
		}

		switch( m_eStep )
		{
		case STEP.WAIT:
			if( bInit ){
				m_fShotPower = 0.0f;
				m_slShotPower.value = m_fShotPower;
			}
			if( m_csPlayer.Step == PlayerController.STEP.IDLE ){
				m_eStep = STEP.IDLE;
			}
			break;
		case STEP.IDLE:
			if (bInit) {
				Debug.Log ("1");
				clearInput ();
				Debug.Log ("2");
			}

			float v = Input.GetAxis ("Vertical");				// 入力デバイスの垂直軸をvで定義
			moveAngle (v);

			if (IsPushed ()) {
				m_eStep = STEP.BUTTON_RELEASE_CHECK;
				m_eStep = STEP.SHOT_START;
			}
			break;
			/*
		case STEP.BUTTON_RELEASE_CHECK:
			if( !Input.GetKey(KeyCode.Space) ){
				m_eStep = STEP.SHOT_START;
			}
			break;
			*/
		case STEP.SHOT_START:
			if (bInit) {
				m_fTimer = 0.0f;
				clearInput ();
			}
			m_fTimer += (Time.deltaTime / m_fShotPowerInterval);

			bool bEnd = false;
			if (IsPushed ()) {
				bEnd = true;
			}
			else if( m_fShotPowerInterval < m_fTimer ){
				m_fTimer = m_fShotPowerInterval;
				bEnd = true;
			}

			if( bEnd ){
				m_eStep = STEP.SHOT;
			}

			m_fShotPower = GetShotPower( m_fTimer , m_fShotPowerInterval );
			m_slShotPower.value = m_fShotPower;
			//Debug.Log( "shotpower=" + m_fShotPower);
			break;

		case STEP.SHOT:
			if( bInit ){
				Vector3 dir = m_csCamera.gameObject.transform.forward;
				dir.y = 0.3f + m_fShotAngleValue * 0.5f;

				m_csPlayer.Shot( dir , m_fShotPower*m_fPowerController );

				m_iShotCount += 1;
				m_txtPower.text = "打数：" + m_iShotCount.ToString();

			}
			m_eStep = STEP.KOROKORO;
			break;
		case STEP.KOROKORO:
			if( m_csPlayer.IsStop()){
				m_eStep = STEP.END;
			}
			m_eStep = STEP.END;
			break;
		case STEP.END:
			if( bInit ){
			}
			m_fShotPower -= 0.05f;
			if( m_fShotPower < 0.0f ){
				m_fShotPower = 0.0f;
				m_eStep = STEP.IDLE;
			}
			m_slShotPower.value = m_fShotPower;
			break;

		case STEP.GOAL:
			if( bInit) {
				m_goResult.transform.localPosition = new Vector3( 1.0f * m_fWindowHalf , 0.0f , 0.0f );
				m_txtResult.text = "ゴール！！！";
			}
			Vector3 tempMoveGoal = m_goResult.transform.localPosition;
			tempMoveGoal.x -= 15.0f;
			m_goResult.transform.localPosition = tempMoveGoal;
			if( tempMoveGoal.x < -1.0f * m_fWindowHalf ){
				m_eStep = STEP.GAME_END;
			}
			break;

		case STEP.GAMEOVER:
			if( bInit) {
				m_goResult.transform.localPosition = new Vector3( 1.0f * m_fWindowHalf , 0.0f , 0.0f );
				m_txtResult.text = "ゲームオーバー";
			}
			Vector3 tempMoveGameover = m_goResult.transform.localPosition;
			tempMoveGameover.x -= 15.0f;
			m_goResult.transform.localPosition = tempMoveGameover;
			if( tempMoveGameover.x < -1.0f * m_fWindowHalf ){
				m_eStep = STEP.GAME_END;
			}
			break;
		case STEP.GAME_END:
			if (bInit) {
				m_goResult.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
				m_txtGotoTitlet.transform.localPosition = new Vector3 (0.0f, -60.0f, 0.0f);
				clearInput ();

			}
			if (IsPushed ()) {
				GameManager.Instance.NextScene();
			}
			break;

		case STEP.MAX:
		default:
			break;
		}
	}

	public bool m_bIsPushed = false;
	public void clearInput(){
		m_bIsPushed = false;
	}
	public bool IsPushed(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			return true;
		}
		return m_bIsPushed;
	}

	public void pushedEasyTouch(){
		m_bIsPushed = true;
	}
	/**
		パワーが０から１の間で返ります
	*/
	public float GetShotPower( float _fTimer , float _fInterval ){
		double dTemp = (double)(_fTimer/_fInterval);
		double angle    = Math.PI * (dTemp * 180) / 180.0;
		float fRet = (float)Math.Sin(angle);
		return fRet;

	}

}











