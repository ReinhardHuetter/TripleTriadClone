using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPowerUp : MonoBehaviour {


    private int power_up_id;
    public bool is_active;
    private string power_up_name;
    public GameObject card;
   





    // Use this for initialization

    void OnAwake()
    {
        

    }
    void Start()
    {
        is_active = false;
        power_up_name = "abstract";
        power_up_id = -1;
        
    }

    // Update is called once per frame
    void Update() {

    }

    public abstract void activatePowerUp(GameObject poweredUpCard);
    

    

   
        

        
}
