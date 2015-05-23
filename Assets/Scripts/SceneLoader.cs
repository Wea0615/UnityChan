using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum SceneType
{
	ActionShow = 0,
	PoseShow,
	Locomotion,
}

public class SceneLoader : MonoBehaviour {

	public GameObject ButtonInstance;

	private Dictionary<int , SceneType> typeMap_ = new Dictionary<int, SceneType>();
	private Vector3 ButtonOffset = new Vector3 (0.0f, 30.0f, 0.0f);

	private Dictionary<int , string> SceneName_ = new Dictionary<int, string>();

	void Start()
	{
		SceneName_.Add (0, "動作展示");
		SceneName_.Add (1, "姿態展示");
		SceneName_.Add (2, "行動展示");

		SetupButtons_();
	}

	void SetupButtons_()
	{

		for (int i=0 ;i < Enum.GetNames(typeof(SceneType)).Length ; i++)
		{
			GameObject button = GameObject.Instantiate(ButtonInstance) as GameObject;
			button.name = Enum.GetName( typeof(SceneType) , i);
			button.GetComponentInChildren<Text>().text = SceneName_[i];
			
			RectTransform buttonTrans = button.GetComponent<RectTransform>();
			buttonTrans.anchorMin = new Vector2( 1.0f , 0.0f);
			buttonTrans.anchorMax = new Vector2( 1.0f , 0.0f);
			buttonTrans.SetParent(transform);
			buttonTrans.anchoredPosition = new Vector3( -85.0f , 20.0f , 0.0f) + ButtonOffset * i;	

			Button buttonUI = button.GetComponent<Button>();
			typeMap_.Add( i , (SceneType)i );
			SceneType type = typeMap_[i];
			buttonUI.onClick.AddListener( () => LoadScene_(type) );
		}
	}

	void LoadScene_(SceneType type)
	{
		Application.LoadLevel( (int)type );
	}

}
