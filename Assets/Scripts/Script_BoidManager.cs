using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_BoidManager : MonoBehaviour {
    //
    // this will be added to a game manager class!!!
    //

    Vector3 focus;
    int timer;
    int NextFocusPoint;

    List<Script_BoidsBehaviour> Boids;
	// Use this for initialization
	void Start ()
    {
        Boids = new List<Script_BoidsBehaviour>();
        //Script_BoidsBehaviour[] tempBoids = FindObjectsOfType(typeof(Script_BoidsBehaviour)) as Script_BoidsBehaviour[];
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objs.Length; i++)
        {
            Boids.Add(objs[i].GetComponent<Script_BoidsBehaviour>());
        }

        timer = 0;
        NextFocusPoint = 0;
        focus = new Vector3(0.0f, 0.0f, 0.0f);

    }

    // Update is called once per frame
    void Update ()
    {
        for (int i = 0; i < Boids.Count; i++)
        {
            Boids[i].computeForce();
        }

        //timer++;
        //if (timer > 29)
        //{
        //    timer = 0;
        //    focus.x = Random.Range(-50.0f, 50.0f);          
        //    focus.z = Random.Range(-50.0f, 50.0f);
        //    focus.y = 0.0f;
        //}
    }
}
