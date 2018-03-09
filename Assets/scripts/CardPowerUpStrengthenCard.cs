using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPowerUpStrengthenCard : CardPowerUp {

    private GameObject poweredUpCard;
    public CardProperties card_property;
	// Use this for initialization
	void Start ()
    {
        card_property = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public override void activatePowerUp(GameObject poweredUpCard)
    {
        this.poweredUpCard = poweredUpCard;
        card_property = poweredUpCard.GetComponent<CardProperties>();
        StrengthenRuleNr1();
    }

    //TODO seperate the rules to own classes
    // increase the value of all directions from the card 
    public void StrengthenRuleNr1()
    {
        this.card_property.up_value += 1;
        this.card_property.down_value += 1;
        this.card_property.left_value += 1;
        this.card_property.right_value += 1;

        CardValueFollower card_value_follower = poweredUpCard.GetComponent<CardValueFollower>();
        card_value_follower.resetCardTextValues(this.card_property.up_value, this.card_property.down_value,
                                             this.card_property.left_value, this.card_property.right_value);


    }
}
