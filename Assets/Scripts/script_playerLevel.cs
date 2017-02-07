using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_playerLevel : MonoBehaviour {

    public int exp;
    public int weapon;
    int[] levelReq;

    Script_Player_Controller player;

	// Use this for initialization
	void Start ()
    {
        player = GetComponent<Script_Player_Controller>();
        weapon = 0;
        levelReq = new int[5] { 0, 100, 220, 360, 520};
	}
	
	// Update is called once per frame
	void Update () {
		switch (weapon)
        {
            case 1:
                player.shotCount = 6;
                break;
            case 2:
                player.shotCount = 9;
                break;
            case 3:
                player.shotCount = 13;
                break;
            case 4:
                player.shotCount = 18;
                break;
        }
	}

    public void checkLevel()
    {
        if (exp >= levelReq[weapon])
        {
            if (weapon < 4)
            {
                weapon++;
            }
        }
    }
}
