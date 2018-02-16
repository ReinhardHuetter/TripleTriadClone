using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag3D : MonoBehaviour {

    private Vector3 dist;
    private float pos_x;
    private float pos_y;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        Debug.Log("mouseDown");
        dist = Camera.main.WorldToScreenPoint(transform.position);
        pos_x = Input.mousePosition.x - dist.x;
        pos_y = Input.mousePosition.y - dist.y;

    }

    void OnMouseDrag()
    {
        Debug.Log("onMouseDrag");
        Vector3 curPos = new Vector3(Input.mousePosition.x - pos_x, Input.mousePosition.y - pos_y, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
    }
}
