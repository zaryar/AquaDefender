using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroulEnemy : BasicEnemy
{

    [SerializeField] private int player_id = 0;
    private EnemyData data;
    List<Vector3> trajectory;
    int patroul_number = 0;
    int patroul_status = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = gameObject.GetComponent<HealthBar3D>();
        data = gameObject.GetComponent<EnemyData>();
        trajectory = data.Getpatroultrajectory(player_id);
    }

    // Update is called once per frame
    void Update()
    {
        orient_player(); 
        _agent.destination = trajectory[patroul_number];
        if (patroul_status == 0 && patroul_number == 2)
        {
            patroul_status = 1;
        }
        else if (patroul_status == 1 && patroul_number == 0)
        {
            patroul_status = 0;
        }

        if (Vector3.Distance(transform.position, _agent.destination) < 1.0f && patroul_status == 0)
        {
            patroul_number += 1;
        }
        else if (Vector3.Distance(transform.position, _agent.destination) < 1.0f && patroul_status == 1)
        {
            patroul_number -= 1;
        }
        if (Vector3.Distance(transform.position, _target.position) < 7f)
        {
            gun_aim();
            _gun.Shoot();
        }
    }
}
