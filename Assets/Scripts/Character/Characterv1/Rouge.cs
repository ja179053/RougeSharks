using UnityEngine;
using System.Collections;
using PlayerControlled;

public class Rouge : MonoBehaviour
{
	Rigidbody r;
	void Start ()
	{
		if (m == null) {
			m = GetComponentInChildren<SkinnedMeshRenderer> ().material;
		//	Debug.Log (m.color.r);
		}

		myState = state.Idle;
	}

	void OnCollisionStay (Collision c)
	{
		if (c.gameObject.tag == "Player") {
			change = 1;
			Rigidbody r = GetComponent<Rigidbody> ();
			Player p = c.gameObject.GetComponent<Player> ();
			p.Damage ((r.velocity.x + r.velocity.y + r.velocity.z)/100);
			p.anim.SetTrigger ("Hit");
			myState = state.ShouldGo;
		} else {
			//	Debug.Log (c.collider.name);
		}
	}

	enum state
	{
		Idle,
		ShouldGo,
		Started,
		Ending
	}

	state myState;
	public float flashSpeed = 40;
	float red = 0, change = 1;
	Material m;

	void Update ()
	{
		switch (myState) {
		case state.Idle:
			break;
		case state.ShouldGo:			
			if (red > 254) {
				change *= -1;
				red = 255;
				myState = state.Ending; 
			}
			goto ColourChange;
			break;
		case state.Ending:
			if (red < 0) {
				myState = state.Idle;
				red = 0;
			}
			goto ColourChange;
			break;

		}
		return;
		ColourChange:
//		Debug.Log ("running");
		red += change * flashSpeed;
	//	m.color = new Vector4 (red / 255, m.color.g, m.color.b, m.color.a);
		m.color = new Vector4 (1, 1 - (red/255), 1 - (red/255), m.color.a);
		return;
	}
}
