using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_boidGroup : MonoBehaviour
{
    public GameObject boidPrefab;
    public script_waypoint waypoint;
    public float speed = 1.0f;
    public List<Script_BoidsBehaviour> boids = new List<Script_BoidsBehaviour>();
    
    public float distance;

    private int team = -1;
    public int maxCount = 0;
    private script_Manager manager;
    public bool canPanic = true;

    private void Start()
    {
        manager = script_Manager.Instance;
    }

    public void AddToGroup(Color c, float distribution)
    {
        Script_BoidsBehaviour bb = (Instantiate(boidPrefab, transform.position, boidPrefab.transform.rotation, transform) as GameObject).GetComponent<Script_BoidsBehaviour>();
        bb.focus = gameObject;
        bb.transform.position += new Vector3(Random.Range(-distribution, distribution), 0f, Random.Range(-distribution, distribution));
        bb.GetComponent<Renderer>().material.SetColor("_EmissionColor", c * 2.0f);
        bb.group = this;

        boids.Add(bb);
        maxCount++;
    }

    public void AddToGroup(Script_BoidsBehaviour bb)
    {
        bb.focus = gameObject;
        bb.group = this;

        boids.Add(bb);
        maxCount++;
    }

    private void Update()
    {
        GameObject target = CalculateTarget();
        if (target && canPanic)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * 2.0f * Time.deltaTime);
        else if (waypoint)
            transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, speed * Time.deltaTime);

        for (int i = 0; i < boids.Count; i++)
        {
            if (boids[i] == null)
                boids.RemoveAt(i);
        }

        if (boids.Count <= 0)
            manager.Destroy(team, gameObject);
        else if(boids.Count <= maxCount * 0.5f && canPanic)
        {
            script_boidGroup closest = FindClosestGroup();

            if (closest)
            {
                for (int i = 0; i < boids.Count; i++)
                {
                    // might not be needed
                    if (boids[i])
                    {
                        boids[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                        // 
                        closest.AddToGroup(boids[i]);
                        boids[i].transform.parent = closest.transform;
                    }
                }

                manager.Destroy(team, gameObject);
            }
        }
    }

    public void SetTeam(int index)
    {
        team = index;
    }

    public int GetTeam()
    {
        return team;
    }

    public void ResetColor()
    {
        for (int i = 0; i < boids.Count; i++)
            boids[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", script_Manager.Instance.GetTeamColor(team));
    }

    private GameObject CalculateTarget()
    {
        script_Manager.Team opponent = manager.GetOpponent(team);
        float maxRadius = 10.0f;
        float shortestDistance = maxRadius;
        int shortestIndex = -1;

        for (int i = 0; i < opponent.GetSize(); i++)
        {
            GameObject target = opponent.GetObject(i);

            if (target)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);

                if (distance < maxRadius && distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestIndex = i;
                }
            }
        }

        if (shortestIndex != -1)
            return opponent.GetObject(shortestIndex);
        return null;
    }

    private script_boidGroup FindClosestGroup()
    {
        script_Manager.Team allies = manager.GetTeam(team);

        float shortestDistance = 500.0f;
        int shortestIndex = -1;
        
        for (int i = 0; i < allies.GetSize(); i++)
        {
            GameObject target = allies.GetObject(i);

            if (target.GetComponent<script_boidGroup>())
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance != 0 && distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestIndex = i;
                }
            }
        }

        if (shortestIndex != -1)
            return allies.GetObject(shortestIndex).GetComponent<script_boidGroup>();
        return null;
    }
}
