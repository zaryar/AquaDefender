using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamperEnemy : BasicEnemy
{
    [SerializeField] private int player_id=0;
    private EnemyData data;
    List<Vector3> trajectory; 

    public void Start()
    {
        healthbar = gameObject.GetComponent<HealthBar3D>();
        data = gameObject.GetComponent<EnemyData>();
        trajectory = data.Getcampertrajectory(player_id);
    }


    // Update is called once per frame
    void Update()
    {
        orient_player();
        _agent.destination = Get_sorted_distance(trajectory, _target.position, "min");
        if (Vector3.Distance(transform.position, _agent.destination) <= 0.8 && Vector3.Distance(transform.position, _target.position) < 10f)
        {
            gun_aim();
            _gun.Shoot();
        }

    }
}
