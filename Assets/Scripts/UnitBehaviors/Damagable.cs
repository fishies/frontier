using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{

    private int _health;
	public int health
	{
		get { return _health; }
		set {
				_health = value;
				text.text = "";
				for (int i = 0; i < _health; ++i) {
					text.text += "♥";
				}
			}
	}
	TextMesh text;

	void Awake ()
	{
		Production p = gameObject.GetComponent<Production> ();
		text = gameObject.GetComponentInChildren<TextMesh> ();
		text.color = Color.black;
		if (p != null) {
			// text.color [(p.ownerID % 3)] = 1.0f;
			// Cannot modify a value type return value of 'UnityEngine.TextMesh.color'??????

			switch (p.ownerID % 5) {
			case 0:
				text.color = Color.red;
				break;
			case 1:
				text.color = Color.green;
				break;
			case 2:
				text.color = Color.blue;
				break;
			case 3:
				text.color = Color.yellow;
            return 
				break;
			case 4:
				text.color = Color.magenta;
				break;
			default:
				break;
			}
		}
	}

    void Start() {
        if(GetComponent<RoadHandler>() == null) {
            health = 2;
        } else {
            health = 3;
        }
    }

	void Update ()
	{
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public void takeDamage (int attackStrength)
	{
		if (tag == "Capital") {
			GetComponent<PlayerInfo> ().stockpile [(int)Production.Resource.Food] -= attackStrength;
		} else {
			health -= attackStrength;
			text.text = "";
			for (int i = 0; i < health; ++i) {
				text.text += "♥";
			}
		}
	}
}
