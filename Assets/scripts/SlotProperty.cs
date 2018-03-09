using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotProperty : MonoBehaviour {

    public int slot_ID;

    public int slot_id_up_neighbour;
    public int slot_id_down_neighbour;
    public int slot_id_right_neighbour;
    public int slot_id_left_neighbour;

    // Use this for initialization
    void OnAwake()
    {
        
    }
    void Start () {


        //TODO initialise ids with -1, but right now the problem is, 
        //that the start function will be executed after the IDs assignments

    }
    public int getSlotID()
    {
        return slot_ID;
    }
    public int slotIdUpNeighbour
    {
        get { return slot_id_up_neighbour; }
    }
    public int slotIdDownNeighbour
    {
        get { return slot_id_down_neighbour; }
    }
    public int slotIdRightNeighbour
    {
        get { return slot_id_right_neighbour; }
    }
    public int slotIdLeftNeighbour
    {
        get { return slot_id_left_neighbour; }
    }

    // Update is called once per frame
    void Update () {
	}

    void OnMouseOver()
    {
        Debug.Log("mouse over objet");
        GameObject.Find("GameBoard").GetComponent<GameBoardManager>().setSnappingSlot(gameObject);

        Debug.Log("on mouse over slot ids");
        Debug.Log(slot_ID);
    }

    public void setSlotID(int ID)
    {
        this.slot_ID = ID;
        
    }

    public void determineSlotNeighbours()
    {

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.GetComponent<Collider>().bounds.center, 1f);
        ArrayList slot_colliders = new ArrayList();

        int i = 0;
        RaycastHit hit;
        //do a raycast in all 4 directions to determine all possible neighbours
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 10.0f))
        {
          
            GameObject slot_found = hit.collider.gameObject;
            if (hit.collider.gameObject.tag == "Slot")
            {
                if (hit.collider.gameObject.transform.GetComponent<SlotProperty>() != null)
                {
                    slot_id_up_neighbour = slot_found.transform.GetComponent<SlotProperty>().slot_ID;
                    Debug.Log("slod Id found: " + slot_ID);
                    Debug.Log(slot_found.transform.GetComponent<SlotProperty>().slot_ID);
                }
            }
        }
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f))
        {
            GameObject slot_found = hit.collider.gameObject;
            if (hit.collider.gameObject.tag == "Slot")
            {
                if (hit.collider.gameObject.transform.GetComponent<SlotProperty>() != null)
                {
                    Debug.Log("slod Id found: " + slot_ID);
                    Debug.Log(slot_found.transform.GetComponent<SlotProperty>().slot_ID);
                    slot_id_down_neighbour = slot_found.transform.GetComponent<SlotProperty>().slot_ID;
                }
            }
        }

        if (Physics.Raycast(transform.position, Vector3.left, out hit, 2.0f))
        {
            GameObject slot_found = hit.collider.gameObject;
            if (hit.collider.gameObject.tag == "Slot")
            {
                if (hit.collider.gameObject.transform.GetComponent<SlotProperty>() != null)
                {
                    Debug.Log("slod Id found: " + slot_ID);
                    Debug.Log(slot_found.transform.GetComponent<SlotProperty>().slot_ID);
                    slot_id_left_neighbour = slot_found.transform.GetComponent<SlotProperty>().slot_ID;
                }
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, 2.0f))
        {
            GameObject slot_found = hit.collider.gameObject;
            if (hit.collider.gameObject.tag == "Slot")
            {
                if (hit.collider.gameObject.transform.GetComponent<SlotProperty>() != null)
                {
                    Debug.Log("slod Id found: " + slot_ID);
                    Debug.Log(slot_found.transform.GetComponent<SlotProperty>().slot_ID);
                    slot_id_right_neighbour = slot_found.transform.GetComponent<SlotProperty>().slot_ID;
                }
            }
        }

       

    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, 1);
    //}


}
