using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    public int points = 0;
    public int normal_points = 10;
    public int special_points = 50;

    public float game_time = 60;
    public float special_extra_time = 5;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTime();
	}

    public void AddPoints(bool special)
    {
        if (!special)
            points += 10;
        else
            points += 50;
        
    }

    public void AddTime(bool special)
    {
        if(special)
        {
            game_time += special_extra_time;
        }
    }

    void UpdateTime()
    {
        game_time -= Time.deltaTime;

        if(game_time <= 0)
        {
            //Game over i guess
        }
    }
}
