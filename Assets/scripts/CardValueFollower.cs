using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardValueFollower : MonoBehaviour {

    public Text up_value;
    public Text down_value;
    public Text left_value;
    public Text right_value;
    public GameObject card_value_parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(card_value_parent!= null)
        {
            Vector3 text_pos = Camera.main.WorldToScreenPoint(this.transform.position);
            Quaternion text_rot = this.transform.localRotation;
            card_value_parent.transform.position = text_pos;
            card_value_parent.transform.localRotation = text_rot;

            
        }
        
        
        //up_value.transform.position = text_pos;
        //down_value.transform.position = text_pos;
        //left_value.transform.position = text_pos;
        //right_value.transform.position = text_pos;


    }

    public void setCardTextValues(GameObject cardValueParent)
    {
        this.card_value_parent = cardValueParent;
    }

    public void resetCardTextValues(int up, int down, int left, int right)
    {
        card_value_parent.transform.Find("TextUpValue").GetComponent<Text>().text = up.ToString();
        card_value_parent.transform.Find("TextDownValue").GetComponent<Text>().text = down.ToString();
        card_value_parent.transform.Find("TextLeftValue").GetComponent<Text>().text = left.ToString();
        card_value_parent.transform.Find("TextRightValue").GetComponent<Text>().text = right.ToString();
    }
}
