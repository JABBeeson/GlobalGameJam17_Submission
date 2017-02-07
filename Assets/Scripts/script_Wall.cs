using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_Wall : MonoBehaviour {

    public int team = -1;
    public int health = 0;

    script_Manager manager;
    GameObject[] towers;

    private bool markForDeath = false;

    private void Start()
    {
        manager = script_Manager.Instance;

        if (team >= 0)
        {
            manager.AddToTeam(team, gameObject);

            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            towers = new GameObject[renderers.Length];

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.SetColor("_EmissionColor", manager.GetTeamColor(team));
                renderers[i].gameObject.AddComponent<script_explosion>();

                towers[i] = renderers[i].gameObject;
            }
        }
    }
    
    private void Update()
    {
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

    float timer = 0.0f;
    float timerTarget = -1.0f;
    private void Die()
    {
        if (timerTarget < 0.0f)
        {
            for (int i = 0; i < towers.Length; i++)
                StartCoroutine(towers[i].GetComponent<script_explosion>().meshSplit(true, 5));

            GetComponent<BoxCollider>().enabled = false;

            timerTarget = towers[0].GetComponent<script_explosion>().waitTime;
        }

        timer += Time.deltaTime;

        if (timer >= timerTarget)
            manager.Destroy(team, gameObject);
    }
}
