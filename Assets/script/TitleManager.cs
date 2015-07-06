using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour {

	private int m_iHikitomeIndex;
	public GameObject m_goHikitome;
	public Text m_txtHikitome;
	public string [] m_strHikitome;


	// Use this for initialization
	void Start () {
		m_iHikitomeIndex = 0;
		m_txtHikitome.text = "";
	}

	public void ButtonStart(){
		Application.LoadLevel("moving");
	}

	public void ButtonQuit(){

		if( 0 < m_strHikitome.Length ){
			if( m_iHikitomeIndex < m_strHikitome.Length  ){
				m_txtHikitome.text = m_strHikitome[m_iHikitomeIndex];
				m_iHikitomeIndex += 1;
			}
		}


	}



}
