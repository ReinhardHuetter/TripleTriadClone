using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//TODO make this class a singleton 
public  class GameBoardManager : MonoBehaviour {

    public GameObject newBattleButton;
    public GameObject winnerText;
    public GameObject SlotPrefab;
    public GameObject snappingSlot;
    public static Vector3 board_size;
    float card_x_pos;
    float card_y_pos;
    public int slot_count;
    public ArrayList slots;
    public List<GameObject> cards;

    public GameObject card_prefab;
    public GameObject card_value_parent_prefab;

    public static int slot_id_counter;

    public AudioClip turn_card_sound;
    AudioSource audioSource;
    private enum Turn { player, opponent};
    private Turn current_turn;
    // Use this for initialization
    void Start ()
    {
        winnerText = GameObject.Find("WinnerText").gameObject;
        newBattleButton = GameObject.Find("NewBattle_btn").gameObject;
        newBattleButton.GetComponent<Button>().onClick.AddListener(restartBattle); //TODO assign this inside own function

        newBattleButton.SetActive(false);
        cards = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();

        slot_id_counter = 0;
        slots = new ArrayList();
        snappingSlot = null;
        slot_count = 9;
        board_size = transform.gameObject.GetComponent<Renderer>().bounds.size;
        card_x_pos = -1 / 3f;
        card_y_pos = -1 / 3f;

        generateEmptySlots();

        cards.Add(createCard(4, 1, 2, 1, GameObject.Find("CardStartPos1").transform.position, "player")) ;
        cards.Add(createCard(3, 2, 1, 1, GameObject.Find("CardStartPos2").transform.position, "player")) ;
        cards.Add(createCard(1, 4, 1, 3, GameObject.Find("CardStartPos3").transform.position, "player"));
        cards.Add(createCard(5, 1, 1, 2, GameObject.Find("CardStartPos4").transform.position, "player"));
        cards.Add(createCard(1, 2, 2, 2, GameObject.Find("CardStartPos5").transform.position, "player"));

        cards.Add(createCard(4, 1, 2, 1, GameObject.Find("EnemyCardStartPos1").transform.position, "opponent"));
        cards.Add(createCard(3, 2, 1, 1, GameObject.Find("EnemyCardStartPos2").transform.position, "opponent"));
        cards.Add(createCard(1, 4, 1, 3, GameObject.Find("EnemyCardStartPos3").transform.position, "opponent")) ;
        cards.Add(createCard(5, 1, 1, 2, GameObject.Find("EnemyCardStartPos4").transform.position, "opponent"));
        cards.Add(createCard(1, 2, 2, 2, GameObject.Find("EnemyCardStartPos5").transform.position, "opponent"));

        StartMatch();









    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StartMatch()
    {

        
        current_turn = Turn.player;
        //right now player always starts
        //disable cards of opponent
        foreach(GameObject card in cards)
        {
            CardDrag3D drag_script = card.GetComponent<CardDrag3D>();
         
            if (card.GetComponent<CardProperties>().owner == "opponent")
            {
                Debug.Log("disable script of opponent");

                card.GetComponent<CardDrag3D>().script_enabled = false;
               

            }
        }

        Debug.Log("Match ist starting, start player = blue ");
    }

    public void switchPlayersTurn()
    {
        
        string new_owner = "none";
        if (current_turn == Turn.player)
        {
            current_turn = Turn.opponent;
            new_owner = "opponent";
        }
        else if (current_turn == Turn.opponent)
        {
            current_turn = Turn.player;
            new_owner = "player";
        }
        if (new_owner == "none")
            throw new System.Exception("current_turn is not assigned. This shouldnt happen!");

       
        foreach (GameObject card in cards)
        {
            if (card.GetComponent<CardProperties>().owner == new_owner)
            {

                card.GetComponent<CardDrag3D>().script_enabled = true;
                Debug.Log("enable the current players card scripts");
            }
            //TODO check with an exception or/and make the owner to an enum
            else
            {
                card.GetComponent<CardDrag3D>().script_enabled = false;
                Debug.Log("disable the last players card scripts");
            }
        }

    }

    public void restartBattle()
    {
        SceneManager.LoadScene("FightScene");
    }

    public void playTurnCardSound()
    {
        audioSource.PlayOneShot(turn_card_sound, 0.7F);
    }

    public ArrayList getAllSlots()
    {
        if(slots == null)
            return null;

        return slots;
    }

    public void generateEmptySlots()
    {

        //1. first create the slot objects and position them according to the gameBoard
        int current_child = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //Debug.Log("cardSlot " + current_child);
                //Debug.Log("card_x" + card_x_pos);
                //Debug.Log("card_y" + card_y_pos);
                Vector3 scale_tmp = new Vector3(1, 1, 30);
                float tmp_x = 1 / 3f;
                float tmp_y = 1 / 3f;
                scale_tmp.x = tmp_x;
                scale_tmp.y = tmp_y;
                GameObject child = Instantiate(SlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                child.GetComponent<SlotProperty>().setSlotID(createSlotID());
                Debug.Log("created slot child new ID = " + child.GetComponent<SlotProperty>().getSlotID() );
                child.transform.parent = transform;
                child.transform.localScale = scale_tmp;

                
                Vector3 tmp_pos = child.transform.localPosition;
                tmp_pos.x = card_x_pos;
                tmp_pos.y = card_y_pos;
                child.transform.localPosition = tmp_pos;

                slots.Add(child);
               // Debug.Log("new child pos " + child.transform.localPosition);
                card_x_pos += 1 / 3f;
                current_child++;
            }
            card_x_pos = -1 / 3f;
            card_y_pos += 1 / 3f;
        }

        //2. now assign the slot neighbours IDs
        for (int i = 0; i < slots.Count; ++i)
        {
            GameObject slot = (GameObject)slots[i];
            slot.GetComponent<SlotProperty>().determineSlotNeighbours();
        }

    }

    public  void setSnappingSlot(GameObject slot)
    {

        snappingSlot = slot;
    }

    public  GameObject createCard(int up, int down, int left, int right, Vector3 pos, string owner)
    {
        GameObject card = Instantiate(card_prefab, pos, Quaternion.identity) ;
        GameObject card_value_parent = Instantiate(card_value_parent_prefab, pos, Quaternion.identity) ;
        card_value_parent.transform.SetParent(GameObject.Find("Canvas").transform);
        card_value_parent.transform.Find("TextUpValue").GetComponent<Text>().text = up.ToString();
        card_value_parent.transform.Find("TextDownValue").GetComponent<Text>().text = down.ToString();
        card_value_parent.transform.Find("TextLeftValue").GetComponent<Text>().text = left.ToString();
        card_value_parent.transform.Find("TextRightValue").GetComponent<Text>().text = right.ToString();

        card.GetComponent<CardProperties>().initialiseProperties(up, down, left, right, card_value_parent);
        card.GetComponent<CardProperties>().setCurrentOwner(owner);

        card.GetComponent<CardValueFollower>().setCardTextValues(card_value_parent);

        return card;
    }

    public static int createSlotID()
    {
        slot_id_counter += 1;
        return slot_id_counter;
    }

    public void checkBattleStatus()
    {
        bool card_left = false;
        int cards_left_nr = 10;
        int player_cards = 0;
        int opponent_cards = 0;
        string winner;
        //TODO could be much easier
        //make a placed cards on slot counter and check this variable to 
        //see if the whole match is over!!!!
        foreach (GameObject card in cards)
        {
            //check if no cards are left to play
            if( card.GetComponent<CardProperties>().is_placed_on_slot)
            {
                cards_left_nr -= 1;
            }

            if (card.GetComponent<CardProperties>().owner == "player")
                player_cards += 1;
            else if (card.GetComponent<CardProperties>().owner == "opponent")
                opponent_cards += 1;

        }
        //end game and calculate the winner
        if(cards_left_nr == 1)
        {
            Debug.Log("player cards number" + player_cards);
            Debug.Log("opponent cards number" + opponent_cards);
            if (player_cards > opponent_cards)
                winner = "blue_player wins";
            else if (opponent_cards > player_cards)
                winner = "red_player wins";
            else
                winner = "draw";

            winnerText.GetComponent<Text>().text = winner ;
            newBattleButton.SetActive(true);

        }
    }
}
