using UnityEngine;
using System.Collections;
using System;

[RequireComponent( typeof(Rigidbody)) ]
public class PlayerController : MonoBehaviour {

	public Rigidbody m_Rigidbody;
	public Transform m_tfCamera;

	public enum STEP {
		IDLE		,
		READY		,
		MOVING		,
		MAX 		,
	}
	protected STEP m_eStep;
	protected STEP m_eStepPre;

	public STEP Step{
		get{return m_eStep;}
	}

	public void Shot( Vector3 _dir , float _fPower ){
		Debug.Log( "p=" + _fPower + " x=" + _dir.x + " y=" + _dir.y + " z=" + _dir.z);

		GetComponent<Rigidbody>().AddForce( _dir * _fPower );
		return;
	}

	/**
		こいつがとまっているかどうか
	*/
	public bool IsStop(){
		if( GetComponent<Rigidbody>().velocity.magnitude < 1.0f ){
			return true;
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {

		bool bInit = false;

		if( m_eStepPre != m_eStep ){
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch( m_eStep )
		{
		case STEP.IDLE:
			break;
		case STEP.READY:
			break;
		case STEP.MOVING:
			break;
		case STEP.MAX:
		default:
			break;
		}
		if (transform.position.y < -15.0f) {
			GameManager.Instance.GameOver();
		}

	
	}

	public float testfloat = 100.0f;

	public float m_fSpeed = 40.0f;

	public float m_fLimitSpeed = 1.0f;
	protected void limitVelocity( Rigidbody _rb){

		float fx = _rb.velocity.x;
		float fz = _rb.velocity.z;

		float fBuf = fx*fx + fz*fz;
		if( fBuf < m_fLimitSpeed*m_fLimitSpeed){

		}
	}



}




