using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {


    GameObject clicked_mole;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Nose")
                {
                    GameObject mole = hit.collider.gameObject.transform.parent.gameObject;

                    mole.GetComponent<MoleBehaviour>().OnSmash();
                }
            }
        }

    }
}
