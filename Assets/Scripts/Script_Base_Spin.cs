using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Base_Spin : MonoBehaviour
{
    public int team = -1;
    public int health = 0;

    script_Manager manager;
    script_explosion coreExplosion;

    private bool markForDeath = false;

    private void Start()
    {
        manager = script_Manager.Instance;
        manager.AddToTeam(team, transform.parent.gameObject);
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", manager.GetTeamColor(team));
    }

    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 20 * Time.deltaTime, 0);

        if (markForDeath)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Script_BoidsBehaviour bb = other.GetComponent<Script_BoidsBehaviour>();

            if (bb.group.GetTeam() != team)
            {
                script_playerLevel playerLvl = script_Manager.Instance.GetPlayer(team).GetComponent<script_playerLevel>();
                other.gameObject.GetComponent<script_enemyInteractions>().Drain(ref health);

                if (health <= 0)
                    markForDeath = true;
            }
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
