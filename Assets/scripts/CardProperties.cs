using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardProperties : MonoBehaviour {

    public int up_value;
    public int down_value;
    public int left_value;
    public int right_value;
    public string owner;
    private Material surface;
    private Animator anim;
    public bool is_placed_on_slot;
    public bool is_combo_card;

    public GameObject card_value_parent;  //TODO probably not best to store it in this script

	// Use this for initialization
    void OnAwake()
    {
        
        

    }
    void Start ()
    {
        is_combo_card = false;
        is_placed_on_slot = false;
        anim = GetComponent<Animator>();
        surface = gameObject.GetComponent<Renderer>().material;
              
	}
	
	// Update is called once per frame
	void Update () {

       

    }

    //check if one of the adjacent cards has a card placed and marked as 'combo' type
    //the chain will start from the combo card if found
    public void checkComboChain(GameObject adjacent_card)
    {
        //1, first find all adjacent slots from this slot
        //2, check if a card is placed on found slot and if it is marked as combo 
        //a combo card will change its owner to the owner of the flipped card 
        

    }

    //TODO probably delete the reference of the card_value_parent and store it only in the other scripts
    public void initialiseProperties( int up_value ,int down_value,  int left_value,  int right_value, GameObject card_value_parent)
    {
        this.up_value = up_value;
        this.down_value = down_value;
        this.left_value = left_value;
        this.right_value = right_value;
        this.card_value_parent = card_value_parent;

       

    }

    public void setCurrentOwner(string owner_name)
    {
        //TODO replace maybe with defined enum value for type safety
        owner = owner_name;
        surface = gameObject.GetComponent<Renderer>().material;
        anim = GetComponent<Animator>();

        if (owner_name == "player")
        {
            anim.SetTrigger("becomePlayerCard");
            surface.color= Color.blue;
            
        }
        else if (owner_name == "opponent")
        {
            anim.SetTrigger("becomeOpponentCard");
            surface.color = Color.red;
            
        }
    }

    public void resetCurrentOwner(string owner_name)
    {

        anim = GetComponent<Animator>();
        

        if (owner_name == "player")
        {
            anim.SetTrigger("becomePlayerCard");
            owner = "player";
        }
        else if (owner_name == "opponent")
        {
            anim.SetTrigger("becomeOpponentCard");
            owner = "opponent";
        }
    }

}
