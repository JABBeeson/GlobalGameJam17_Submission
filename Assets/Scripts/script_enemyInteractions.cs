﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_enemyInteractions : MonoBehaviour {

    public int health;
    public int exp = 5;
    BoxCollider hitBox;

	// Use this for initialization
	void Start ()
    {
        hitBox = GetComponent<BoxCollider>();
        gameObject.AddComponent<script_explosion>();
	}

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            health = 0;
            death();
        }
    }

    public void Drain(ref int targetHP)
    {
        int damage = targetHP;
        targetHP -= health;

        Damage(damage);
    }

    private void death ()
    {
        StartCoroutine(gameObject.GetComponent<script_explosion>().meshSplit(true, 5));
    }
}
