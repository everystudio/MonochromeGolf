using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Material))]
public class background_scroll : MonoBehaviour {

	public GameObject m_goTarget;

	protected Transform m_tfTarget;
	protected Transform m_tfSelf;

	protected Material m_matBack;

	Material workingMaterial;

	// スクロールするスピード
	public float speed = 0.1f;

	void Start(){
		//workingMaterial = new Material(m_matBack);
		//RenderSettings.skybox = workingMaterial;

		m_tfSelf = gameObject.transform;
		m_tfTarget = m_goTarget.transform;
	}

	void Update ()
	{

		m_tfSelf.position = new Vector3( m_tfTarget.position.x , m_tfTarget.position.y , m_tfSelf.position.z );


		// 時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
		float x = Mathf.Repeat (Time.time * speed, 1);

		// Yの値がずれていくオフセットを作成
		Vector2 offset = new Vector2 (x, 0);

		// マテリアルにオフセットを設定する
		GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
	}

}

/*
MissingComponentException: There is no 'Renderer' attached to the "test_background" game object, but a script is trying to access it.
You probably need to add a Renderer to the game object "test_background". Or your script needs to check if the component is attached before using it.
background_scroll.Update () (at Assets/Scripts/2d_action/background_scroll.cs:27)

*/