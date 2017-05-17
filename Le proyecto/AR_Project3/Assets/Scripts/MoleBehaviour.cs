using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour {

    public float up_time = 5.0f;
    public float speed = 1.0f;

    delegate void myDelegate();
    myDelegate currentState;

    public float down_y;
    Vector3 up_position;
    Vector3 down_position;

	// Use this for initialization
	void Start () {
        up_position = transform.position;
        down_position = new Vector3(0.0f, down_y, 0.0f);

        transform.position = down_position;

        currentState = GoUp;

        //StartCoroutine(GoUp());
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentState();
	}

    void GoUp()
    {
        bool ret = false;

      
            float movement = speed * Time.deltaTime;
            transform.Translate(0.0f, movement, 0.0f);

            if (transform.position.y >= up_position.y)
            {
                transform.position = up_position;
                ret = true;
            }
        
        if(ret)
        {
            currentState = GoDown;
        }


            //yield return new WaitForSeconds(up_time);
            //StopCoroutine(GoUp());
            //StartCoroutine(GoDown());
     
    }

    void GoDown()
    {
        bool ret = false;

        
            float movement = -speed * Time.deltaTime;
            transform.Translate(0.0f, movement, 0.0f);

            if (transform.position.y <= down_position.y)
            {
                transform.position = down_position;
                ret = true;
            }
        
            if(ret)
        {
            currentState = GoUp;
        }
      
            //yield return new WaitForSeconds(up_time);
            //StopCoroutine(GoUp());
            //StartCoroutine(GoDown());
        
    }
}
