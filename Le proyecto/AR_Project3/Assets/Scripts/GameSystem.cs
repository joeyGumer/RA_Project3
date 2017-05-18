using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    public int points = 0;
    public int normal_points = 10;
    public int special_points = 50;

    public float game_time = 60;
    public float special_extra_time = 5;

    public int int_game_time = 0;

    public Text points_text;
    public Text points2_text;
    public Text time_text;

    public GameObject end_menu = null;
    public GameObject in_game_menu = null;

	// Use this for initialization
	void Start () {
        end_menu.SetActive(false);
        in_game_menu.SetActive(true);
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

        points_text.text = points.ToString();
        
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

        int_game_time = (int)game_time;
        time_text.text = int_game_time.ToString();

        if(game_time <= 0)
        {
            in_game_menu.SetActive(false);
            points2_text.text = points.ToString();
            end_menu.SetActive(true);
        }
    }
}
