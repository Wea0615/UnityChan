using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FaceUpdate : MonoBehaviour
{
	public AnimationClip[] animations;

	public GameObject FaceObject;
	public GameObject ButtonInstance;

	private Animator anim;

	public float delayWeight;

	private Vector3 ButtonOffset = new Vector3 (0.0f, -30.0f, 0.0f);
	private Dictionary<string , string> AnimMap_ = new Dictionary<string, string>();

	void Start ()
	{
		anim = FaceObject.GetComponent<Animator> ();

		SetupButtons_ ();
	}

	void SetupButtons_()
	{
		int button_num = 0;

		foreach (var animation in animations) 
		{
			GameObject button = GameObject.Instantiate(ButtonInstance) as GameObject;
			Button buttonUI = button.GetComponent<Button>();
			button.name = animation.name;
			button.GetComponentInChildren<Text>().text = animation.name.Replace("@unitychan" ,"");
			button.GetComponent<RectTransform>().SetParent(transform);
			button.GetComponent<RectTransform>().anchoredPosition = new Vector3( 100.0f , -20.0f , 0.0f) + ButtonOffset * button_num;	

			AnimMap_.Add( button.name , animation.name);
			buttonUI.onClick.AddListener( () => ButtonClick_( AnimMap_[button.name]) );

			++button_num;

			/*if (GUILayout.Button (animation.name)) 
			{
				anim.CrossFade (animation.name, 0);
			}*/
		}
	}

	void ButtonClick_(string animName)
	{
		//Debug.Log ("press button : " + animName);
		anim.CrossFade (animName, 0);
	}

	float current = 0;
	void Update ()
	{

		if (Input.GetMouseButton (0)) {
			current = 1;
		} else {
			current = Mathf.Lerp (current, 0, delayWeight);
		}
		anim.SetLayerWeight (1, current);
	}
}
