using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_spawner : MonoBehaviour
{
    public int team = 0;
    public Object prefab;
    public List<script_waypoint> startingPoints = new List<script_waypoint>();
    
    script_Manager manager;

    private void Start()
    {
        manager = script_Manager.Instance;
    }

    public void Spawn(int count)
    {
        if (prefab)
        {
            GameObject bO = (Instantiate(prefab, transform.position, Quaternion.identity) as GameObject);
            script_boidGroup bG = bO.GetComponent<script_boidGroup>();

            bG.SetTeam(team);
            bG.waypoint = startingPoints[Random.Range(0, startingPoints.Count)];

            for (int i = 0; i < count; i++)
                bG.AddToGroup(manager.GetTeamColor(team), 5.0f);

            manager.AddToTeam(team, bO);
        }
    }
}
