using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour {
	float stamina;
	public float maxStamina, staminaRegain;
	void Start(){
		stamina = maxStamina;
	}
	public float Staminar{
		get{
			return stamina;
		} set {
			stamina = Mathf.Clamp(value,0,maxStamina);
			transform.localScale = new Vector3 (transform.localScale.x, stamina / maxStamina, transform.localScale.z);
		}
	}
	public void Drain(float f){
		Staminar -= f;
	}
}
