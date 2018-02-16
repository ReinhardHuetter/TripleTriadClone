using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour {

    public GameObject SlotPrefab;
    public static Vector3 board_size;
    float card_x_pos;
    float card_y_pos;
    public int slot_count;
	// Use this for initialization
	void Start ()
    {
        slot_count = 9;
        board_size = transform.gameObject.GetComponent<Renderer>().bounds.size;
        card_x_pos = -1 / 3f;
        card_y_pos = -1 / 3f;

        generateEmptySlots(); 
       
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void generateEmptySlots()
    {
        int current_child = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Debug.Log("cardSlot " + current_child);
                Debug.Log("card_x" + card_x_pos);
                Debug.Log("card_y" + card_y_pos);
                Vector3 scale_tmp = new Vector3(1, 1, 30);
                float tmp_x = 1 / 3f;
                float tmp_y = 1 / 3f;
                scale_tmp.x = tmp_x;
                scale_tmp.y = tmp_y;
                GameObject child = Instantiate(SlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                child.transform.parent = transform;
                child.transform.localScale = scale_tmp;

                Vector3 tmp_pos = child.transform.localPosition;
                tmp_pos.x = card_x_pos;
                tmp_pos.y = card_y_pos;
                child.transform.localPosition = tmp_pos;
                Debug.Log("new child pos " + child.transform.localPosition);
                card_x_pos += 1 / 3f;
                current_child++;
            }
            card_x_pos = -1 / 3f;
            card_y_pos += 1 / 3f;
        }
    }
}
