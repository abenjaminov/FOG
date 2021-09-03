using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Demo : MonoBehaviour,IPointerClickHandler {

	public GameObject[] Effects = new GameObject[0];
	public Text EffectText;
	[System.NonSerialized] public static int No;
	private Vector3 clickPosition;
	// Use this for initialization
	void Start () {
		No = 0;
		TextChange();
	}

	public void TextChange(){
		EffectText.text = Effects[No].name;
	}
	
	public void OnPointerClick (PointerEventData eventData)
	{
			clickPosition = Input.mousePosition;
			clickPosition.z = 10f;
			GameObject obj = Instantiate(Effects[No], Camera.main.ScreenToWorldPoint(clickPosition), Effects[No].transform.rotation);
			EffectText.text = Effects[No].name;
			Destroy(obj,3f);
	}
}
