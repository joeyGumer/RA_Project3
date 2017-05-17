using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour {

    public float up_time = 5.0f;
    public float speed = 1.0f;
    public float special_speed = 3.0f;
    public int inactive_min_time = 1;
    public int inactive_max_time = 10;
    public int active_time = 3;
    public int special_chance = 10;

    //Materials
    public Material m_idle;
    public Material m_smashed;
    public Material m_special;
    //

    //Attached GO
    public GameObject go_gameSystem;
    public GameObject go_body;
    public GameObject go_nose;

    delegate void myDelegate();
    myDelegate currentState;

    public float down_y;
    public float up_y;
    Vector3 up_position;
    Vector3 down_position;
    float max_distance;
    

    System.Random r_generator;
    public float mole_timer = 0.0f;
    public float time_to_appear = 0.0f;
    public bool up = false;
    public bool to_smash = false;
    public bool special = false;

    public float distance;

	// Use this for initialization
	void Start () {
        max_distance = (up_position - down_position).magnitude;

        //Creating random generator
        string seed = Time.time.ToString();
        r_generator = new System.Random(seed.GetHashCode());

        distance = down_y;
        UpdatePosition(); 

        currentState = GoUp;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        currentState();
        UpdatePosition();

    }

    void OnMouseDown()
    {
        OnSmash();
    }

void GoUp()
    {
        if (!up)
        {
            float sp;
            if (!special)
                sp = speed;
            else
                sp = special_chance;


            distance += speed * Time.deltaTime;

            if (distance >= up_y)
            {
                distance = up_y;
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

        float sp;
        if (!special)
            sp = speed;
        else
            sp = special_chance;


        distance -= speed * Time.deltaTime;

        if (distance <= down_y)
        {
            distance = down_y;
            ret = true;
        }

        if (ret)
        {
            mole_timer = 0.0f;
            time_to_appear = GetRandom(inactive_min_time, inactive_max_time);
            to_smash = false;


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
            transform.Rotate(0.0f, angle, 0.0f, Space.Self);

            //Set if it's special or not
            int sp_random = r_generator.Next(1, 100);

            if(sp_random > special_chance)
            {
                special = false;
                SetBodyMaterial(m_idle);
            }
            else
            {
                special = false;
                SetBodyMaterial(m_special);
            }

            //Then go up
            up = false;
            to_smash = true;
            currentState = GoUp;
        }
    }

    //Utils
    int GetRandom(int min, int max)
    {
        return r_generator.Next(min, max);
    }

    void SetBodyMaterial(Material mat)
    {
        go_body.GetComponent<Renderer>().material = mat;
    }

    public void OnSmash()
    {
        if (to_smash)
        {
            SetBodyMaterial(m_smashed);
            go_gameSystem.GetComponent<GameSystem>().AddPoints(special);
            go_gameSystem.GetComponent<GameSystem>().AddTime(special);
            to_smash = false;
            currentState = GoDown;
        }
    }

    void UpdatePosition()
    {
        transform.position = transform.parent.position + transform.parent.up * distance;
    }
}
