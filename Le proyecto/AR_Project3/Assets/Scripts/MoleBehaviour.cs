using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour {

    public float up_time = 5.0f;
    public float speed = 1.0f;
    public int inactive_min_time = 1;
    public int inactive_max_time = 10;
    public int active_time = 3;

    //Materials
    public Material idle;
    public Material smashed;
    public Material special;
    //

    //Attached GO
    public GameObject body;
    public GameObject nose;

    delegate void myDelegate();
    myDelegate currentState;

    public float down_y;
    Vector3 up_position;
    Vector3 down_position;
    float max_distance;
    

    System.Random r_generator;
    public float mole_timer = 0.0f;
    public float time_to_appear = 0.0f;
    public bool up = false;

    public float distance;

	// Use this for initialization
	void Start () {
        up_position = transform.position;
        down_position = down_y * transform.up;
        max_distance = (up_position - down_position).magnitude;

        //Creating random generator
        string seed = Time.time.ToString();
        r_generator = new System.Random(seed.GetHashCode());

        transform.position = down_position;

        currentState = GoUp;

        //StartCoroutine(GoUp());
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentState();
        if (Input.GetKey(KeyCode.A))
        {
            body.GetComponent<Renderer>().material = smashed;
        }

    }

    void GoUp()
    {
        if (!up)
        {
            Vector3 movement = transform.up * speed * Time.deltaTime;
            transform.Translate(movement);


            distance = (transform.position - down_position).magnitude;

            if (distance >= max_distance)
            {
                transform.position = up_position;
                up = true;

                mole_timer = 0.0f;
                time_to_appear = active_time;
            }
        }
        else
        {
            if (mole_timer < active_time)
            {
                mole_timer += Time.deltaTime;
            }
            else
            {
                currentState = GoDown;
            }
        }


            //yield return new WaitForSeconds(up_time);
            //StopCoroutine(GoUp());
            //StartCoroutine(GoDown());
     
    }

    void GoDown()
    {
        bool ret = false;


        Vector3 movement = transform.up * -speed * Time.deltaTime;
        transform.Translate(movement);


        distance = (up_position - transform.position).magnitude;

        if (distance >= max_distance)
        {
            transform.position = down_position;
            ret = true;
        }

        if (ret)
        {
            mole_timer = 0.0f;
            time_to_appear = GetRandom(inactive_min_time, inactive_max_time);
            
            currentState = SetNextState;
        }
        
    }

    void SetNextState()
    {

        if (mole_timer <= time_to_appear)
        {
            mole_timer += Time.deltaTime;
        }
        else
        {
            //Wich rotation will it have
            float angle = r_generator.Next(0, 360);
            transform.Rotate(transform.up, angle);

            //Then go up
            up = false;
            currentState = GoUp;
        }
    }

    int GetRandom(int min, int max)
    {
        return r_generator.Next(min, max);
    }
}
