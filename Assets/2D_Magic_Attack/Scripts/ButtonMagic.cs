using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMagic : MonoBehaviour {

	public GameObject Root;
	public void NumberAdd() {
		Demo.No++;
		Demo demo = Root.GetComponent<Demo>();
		if(Demo.No > demo.Effects.Length - 1){
			Demo.No = 0;
		}
		demo.TextChange();
  	}

	public void NumberMinus() {
		Demo.No--;
		Demo demo = Root.GetComponent<Demo>();
		if(Demo.No < 0){
			Demo.No = demo.Effects.Length - 1;
		}
		demo.TextChange();
  	}
}
