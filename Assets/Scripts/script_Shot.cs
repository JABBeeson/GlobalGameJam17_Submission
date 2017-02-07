using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class script_Shot : MonoBehaviour {

    // Use this for initialization
    public float speed;
    private Rigidbody Bullet;
    public float lifetime = 2.0f;

    public int exp = 5;
    public int team = -1;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
        Destroy(gameObject.transform.parent.gameObject, lifetime);
        Bullet = GetComponent<Rigidbody>();
    }
    void Start()
    {
        transform.Rotate(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), 0.0f);
        Bullet.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Script_BoidsBehaviour bb = other.GetComponent<Script_BoidsBehaviour>();

            if (bb.group.GetTeam() != team)
            {
                script_playerLevel playerLvl = script_Manager.Instance.GetPlayer(team).GetComponent<script_playerLevel>();

                playerLvl.exp += exp;
                playerLvl.checkLevel();
                other.gameObject.GetComponent<script_enemyInteractions>().Damage(1);

                Destroy(gameObject);
            }
        }
        if (other.tag == "Wall")
        { 
            if (other.gameObject.GetComponent<script_Wall>().team != team)
                Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<Script_Player_Controller>().index != team)
            {
                other.gameObject.GetComponent<Script_Player_Controller>().health -= 1;

                Destroy(gameObject);
            }
               
        }
    }
}
