using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

	public GameObject m_goPlayer;
	public Vector3 m_vecOffset;
	public float m_fRotation;
	protected float m_fDistance;

	// Use this for initialization
	void Start () {
		m_fRotation = 235.0f;
		this.m_vecOffset = this.transform.position - this.m_goPlayer.transform.position;
		this.m_vecOffset.y = 0.0f;

		m_fDistance = m_vecOffset.magnitude;
		this.m_vecOffset.y = 5.0f;
	}

	// Update is called once per frame
	void Update () {
		float add_y = 5.0f;
		double angle    = Math.PI * m_fRotation / 180.0;

		this.transform.position = new Vector3 (
			this.m_goPlayer.transform.position.x + (float)Math.Sin(angle)*m_fDistance, 
			this.m_goPlayer.transform.position.y + this.m_vecOffset.y, 
			this.m_goPlayer.transform.position.z + (float)Math.Cos(angle)*m_fDistance );

		this.gameObject.transform.LookAt(m_goPlayer.transform);//posでもOK
	}

	private void moveCamera( float _fHorizontal ){
		//_fHorizontal *= -1.0f;

		m_fRotation += (_fHorizontal * 1.0f);
		if( m_fRotation < 0.0 ){
			m_fRotation += 360.0f;
		}
		else if( 360.0f < m_fRotation ){
			m_fRotation -= 360.0f;
		}
		else {
			;// ok
		}
	}


	/*
	void FixedUpdate(){
		float h = Input.GetAxis("Horizontal");				// 入力デバイスの水平軸をhで定義

		moveCamera (h);

		return;
	}
	*/

	public void InputCheck(Vector2 _vec){

		moveCamera (_vec.x);

		return;

	}






}