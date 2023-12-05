using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HiddenEnemy : BasicEnemy
{
    [SerializeField] private int player_id=0;
    private EnemyData data;
    List<Vector3> trajectory; 

    public void Start()
    {
        healthbar = gameObject.GetComponent<HealthBar3D>();
        data = gameObject.GetComponent<EnemyData>();
        trajectory = data.Gethiddentrajectory(player_id);
    }


    // Update is called once per frame
    void Update()
    {
        orient_player();
        Vector3 direction = transform.position - _target.position;
        float angle = Vector3.Angle(_target.forward, direction);
        if (180f - angle <= 30)
        {
            _agent.destination = Get_sorted_distance(data.Gethiddentrajectory(player_id), _target.position, "max");
        }
        else if (180 - angle > 30 && Vector3.Distance(transform.position, _target.position) > 2)
        {
            _agent.destination = Get_sorted_distance(data.Gethiddentrajectory(player_id), _target.position, "min");
            if (Vector3.Distance(transform.position, _target.position) < 20)
            {
                gun_aim();
                _gun.Shoot();
            }
        }
        else if (Vector3.Distance(transform.position, _target.position) <= 2)
        {
            gun_aim();
            _gun.Shoot();
        }
    }
}
