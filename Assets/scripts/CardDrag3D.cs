using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag3D : MonoBehaviour {

    private Vector3 dist;
    private float pos_x;
    private float pos_y;
    private Vector3 start_pos;
    public GameBoardManager game_board_manager;
    public bool script_enabled;

    public bool start_finished;
    
	// Use this for initialization

    void OnAwake()
    {
        //NEVER initialise variables here!!! There set ups will be lost after the start function        
    }
	void Start ()
    {
        start_finished = true;
        game_board_manager = GameObject.Find("GameBoard").GetComponent<GameBoardManager>();
        Debug.Log("enable card script in start function");
        script_enabled = true;
        start_finished = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {

        if (!script_enabled)
            return;
        start_pos = transform.position;
        Debug.Log("mouseDown");
        dist = Camera.main.WorldToScreenPoint(transform.position);
        pos_x = Input.mousePosition.x - dist.x;
        pos_y = Input.mousePosition.y - dist.y;

    }

   

    void OnMouseDrag()
    {
        if (!script_enabled)
            return;
        Debug.Log("onMouseDrag");
        Vector3 curPos = new Vector3(Input.mousePosition.x - pos_x, Input.mousePosition.y - pos_y, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
    }

    void OnMouseUp()
    {
        if (!script_enabled)
            return;
        bool slot_collision = false;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0f);
        int i = 0;
        while (i < hits.Length)
        {
            RaycastHit hit = hits[i];
            if(hit.collider.gameObject.layer == 8)
            {
                Debug.Log("placing card over a slot, collision");
                Debug.Log(hit.collider.gameObject.name);
                slot_collision = true;
            }
            i++;
        }
        //drop outside of gameBoard, go back to start position of card
        if(!slot_collision)
        {
            transform.position = start_pos;

            Debug.Log("snap card back to its start position");
            game_board_manager.snappingSlot = null;
            return;
        }



        //snap dragging card to card slot
            if (game_board_manager.snappingSlot != null)
        {
            GetComponent<CardProperties>().is_placed_on_slot = true;
            Vector3 tmp = game_board_manager.snappingSlot.transform.position;
            tmp.z = -0.1f; // TODO adapt this dynamically for adaquate z axis

            transform.position = tmp;
            transform.parent = game_board_manager.snappingSlot.transform.Find("Rahmen");
            //check the neighbours card for a possible fight over
            SlotProperty slot_property = game_board_manager.snappingSlot.GetComponent<SlotProperty>();

            Debug.Log("slot id " + slot_property.getSlotID());
            Debug.Log("up_id" + slot_property.slotIdUpNeighbour);
            Debug.Log("down_id" + slot_property.slotIdDownNeighbour);
            Debug.Log("left_id" + slot_property.slotIdLeftNeighbour);
            Debug.Log("right_id" + slot_property.slotIdRightNeighbour);

            fightAgainstNeighbours(slot_property.slotIdUpNeighbour, slot_property.slotIdDownNeighbour,
                                   slot_property.slotIdLeftNeighbour, slot_property.slotIdRightNeighbour);

        }
    }

    public void fightAgainstNeighbours(int neighbour_up_id, int neighbour_down_id, int neighbour_left_id, int neighbour_right_id)
    {
        if (!script_enabled)
            return;
        Debug.Log("start fighting against neighbours");
        Debug.Log("neighbour Down ID = " + neighbour_down_id);
        
        CardProperties card_property = GetComponent<CardProperties>();
        CardProperties neighbour_card_property;
        //check if up neighbour even exists
        if (neighbour_up_id>=1)
        {
            Debug.Log("neighbour slot exists");
            GameObject found_neighbour_card;
            foreach(GameObject neighbour_slot in game_board_manager.slots)
            {
                //search for up neighbour in all slots
                if(neighbour_slot.GetComponent<SlotProperty>().getSlotID() == neighbour_up_id)
                {
                    Debug.Log("neighbour slot found");
                    //does the neighbour slot has a card placed on it?
                    if(neighbour_slot.transform.Find("Rahmen").childCount >0 )
                    {
                        found_neighbour_card = neighbour_slot.transform.Find("Rahmen").GetChild(0).gameObject;
                        neighbour_card_property = found_neighbour_card.GetComponent<CardProperties>();
                        Debug.Log("placed Card value = " + card_property.up_value);
                        Debug.Log("neighbour Card value = " + neighbour_card_property.up_value);


                        //compare up neighbours value with placed card value 
                        //if placed card is stronger then assign the neighbour card to the placed card's owner
                        if (card_property.up_value > neighbour_card_property.down_value)
                        {
                            neighbour_card_property.resetCurrentOwner(card_property.owner);
                            game_board_manager.playTurnCardSound();
                            Debug.Log("placed Card value is greater than its up neighbour!!!!!!!!!");
                            
                        }
                    }
                   
                }
               
            }
        }

         if (neighbour_down_id >= 1)
        {
            Debug.Log("neighbour down slot exists");
            GameObject found_neighbour_card;
            foreach (GameObject neighbour_slot in game_board_manager.slots)
            {
                //search for up neighbour in all slots
                if (neighbour_slot.GetComponent<SlotProperty>().getSlotID() == neighbour_down_id)
                {
                    Debug.Log("neighbour down slot found");
                    //does the neighbour slot has a card placed on it?
                    if (neighbour_slot.transform.Find("Rahmen").childCount > 0)
                    {
                        found_neighbour_card = neighbour_slot.transform.Find("Rahmen").GetChild(0).gameObject;
                        neighbour_card_property = found_neighbour_card.GetComponent<CardProperties>();
                        Debug.Log("placed Card value = " + card_property.down_value);
                        Debug.Log("neighbour Card value = " + neighbour_card_property.down_value);


                        //compare up neighbours value with placed card value 
                        //if placed card is stronger then assign the neighbour card to the placed card's owner
                        if (card_property.down_value > neighbour_card_property.up_value)
                        {
                            neighbour_card_property.resetCurrentOwner(card_property.owner);
                            game_board_manager.playTurnCardSound();

                            Debug.Log("placed Card value is greater than its down neighbour!!!!!!!!!");
                            
                        }
                    }

                }

            }
        }

        if (neighbour_left_id >= 1)
        {
            Debug.Log("neighbour slot exists");
            GameObject found_neighbour_card;
            foreach (GameObject neighbour_slot in game_board_manager.slots)
            {
                //search for up neighbour in all slots
                if (neighbour_slot.GetComponent<SlotProperty>().getSlotID() == neighbour_left_id)
                {
                    Debug.Log("neighbour slot found");
                    //does the neighbour slot has a card placed on it?
                    if (neighbour_slot.transform.Find("Rahmen").childCount > 0)
                    {
                        found_neighbour_card = neighbour_slot.transform.Find("Rahmen").GetChild(0).gameObject;
                        neighbour_card_property = found_neighbour_card.GetComponent<CardProperties>();
                        Debug.Log("placed Card value = " + card_property.left_value);
                        Debug.Log("neighbour Card value = " + neighbour_card_property.right_value);


                        //compare up neighbours value with placed card value 
                        //if placed card is stronger then assign the neighbour card to the placed card's owner
                        if (card_property.left_value > neighbour_card_property.right_value)
                        {
                            neighbour_card_property.resetCurrentOwner(card_property.owner);
                            game_board_manager.playTurnCardSound();

                            Debug.Log("placed Card value is greater than its left neighbour!!!!!!!!!");
                            
                        }
                    }

                }

            }
        }

        if (neighbour_right_id >= 1)
        {
            Debug.Log("neighbour slot exists");
            GameObject found_neighbour_card;
            foreach (GameObject neighbour_slot in game_board_manager.slots)
            {
                //search for up neighbour in all slots
                if (neighbour_slot.GetComponent<SlotProperty>().getSlotID() == neighbour_right_id)
                {
                    Debug.Log("neighbour slot found");
                    //does the neighbour slot has a card placed on it?
                    if (neighbour_slot.transform.Find("Rahmen").childCount > 0)
                    {
                        found_neighbour_card = neighbour_slot.transform.Find("Rahmen").GetChild(0).gameObject;
                        neighbour_card_property = found_neighbour_card.GetComponent<CardProperties>();
                        Debug.Log("placed Card value = " + card_property.right_value);
                        Debug.Log("neighbour Card value = " + neighbour_card_property.right_value);


                        //compare up neighbours value with placed card value 
                        //if placed card is stronger then assign the neighbour card to the placed card's owner
                        if (card_property.right_value > neighbour_card_property.left_value)
                        {
                            neighbour_card_property.resetCurrentOwner(card_property.owner);
                            game_board_manager.playTurnCardSound();

                            Debug.Log("placed Card value is greater than its right neighbour!!!!!!!!!");
                            
                        }
                    }

                }

            }
        }
        //after each fight against cards, check the current battle status
        /////////////////////////////////////////////////////////////////
        game_board_manager.checkBattleStatus();
        game_board_manager.switchPlayersTurn();

    }

    
    
}
