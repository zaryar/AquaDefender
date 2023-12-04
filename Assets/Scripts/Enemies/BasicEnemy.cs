using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class BasicEnemy : EnemyTemplate
{
    Transform _target;
    Transform _player;
    NavMeshAgent _agent;
    // [SerializeField] float invisibleTime = 5f;
    public GameObject goldPrefab; // Assign the gold prefab in the Inspector window
    GunTemplate _gun;
    int frames;
    public int player_id;
    public string playertype;
    int patroul_number=0;
    int patroul_status = 0;
    SwordTemplate _sword;
    Vector3 old_destination;
    int attack_flag = 0;

    List<List<Vector3>> camper_player = new List<List<Vector3>>() 
        { new List<Vector3>() { new Vector3(28.84362f, 5.20334f, 25.49818f), new Vector3(27.29693f, 4.896906f, 25.13505f),  new Vector3(26.05847f, 4.967204f, 24.14018f), new Vector3(25.54025f, 4.88892f, 22.51293f) }, 
          new List<Vector3>() { new Vector3(25.72059f, 5.660317f, 19.93841f), new Vector3(27.61168f, 5788954f, 18.06712f),  new Vector3(30.22423f, 5.89237f, 17.83356f) },
          new List<Vector3>() { new Vector3(31.63799f, 6.014416f, 18.57949f), new Vector3(46.2789f, 0.8519389f, 8.131048f),  new Vector3(32.36781f, 5.611812f, 22.53408f) },
        };

    List<List<Vector3>> hidden_player = new List<List<Vector3>>()
        { new List<Vector3>() { new Vector3(18.75898f, 1.137792f, 27.06454f), new Vector3(17.65124f, 1.101168f, 28.18887f),  new Vector3(16.49898f, 1.082815f, 27.14907f), new Vector3(17.73894f, 1.154792f, 26.00435f) }
        };

    List<List<Vector3>> patroul_player = new List<List<Vector3>>()
        { new List<Vector3>() { new Vector3(14.1917f, 1.043632f, 4.059807f), new Vector3(41.99136f, 1.429102f, 11.38896f),  new Vector3(38.06299f, 1.494309f, 41.94704f)}
        };



    protected override void Die()
    {
        Instantiate(goldPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void Awake()
    {
        _agent= GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        _target= _player;
        _gun = gameObject.transform.Find("EnemyGun").GetComponent<GunTemplate>();
        //_sword = GetComponent<SwordTemplate>();
        if(playertype == "attack_player")
        {
            _sword = transform.Find("EnemyGun").GetComponent<SwordTemplate>();
        }
    }

    public void attack()
    {
        _agent.destination = _target.position;
        _sword.Attack();
    }

    private Vector3 Get_sorted_distance(List<Vector3> vectors, Vector3 target, string type)
    {
        Vector3 vec = vectors[0];
        float Distance = Vector3.Distance(vec, target);
        //float Distance = float.MaxValue;
        Vector3 Vector = vectors[0];
        

        foreach (Vector3 vector in vectors)
        {
            float distance = Vector3.Distance(vector, target);
            if (distance < Distance && type == "min")
            {
                Distance = distance;
                Vector = vector;
            }
            else if(distance > Distance && type == "max")
            {
                Distance = distance;
                Vector = vector;
            }
        }
        
        return Vector;
    }

    private void orient_player()
    {
        Vector3 dir = _target.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
    }

    private void gun_aim()
    {
        Transform enemy_gun = gameObject.transform.Find("EnemyGun");
        float angle = Vector3.Angle(transform.position - _target.position, Vector3.up);
        float x_rotation = Math.Max(-60f, angle - 90f);
        float z_position = Math.Min(Math.Max(0.2f, 0.2f - (0.002f * (angle - 90f))), 0.35f);
        enemy_gun.transform.localRotation = Quaternion.Euler(x_rotation, 180f, 0f);
        enemy_gun.transform.localPosition = new Vector3(enemy_gun.transform.localPosition.x, enemy_gun.transform.localPosition.y, z_position);
    }

    private void Update()
    {
        frames += 1;
        if(_target == _player)
            StartCoroutine(PlayerVisible());

        orient_player();

        /*if(Vector3.Distance(transform.position, _target.position) < 2f || attack_flag==1)
        {
            old_destination = _agent.destination;
            attack_flag = 1; 
            attack();
        }
        if(Vector3.Distance(transform.position, _target.position) > 10f && attack_flag == 1)
        {
            _agent.destination = old_destination;
            attack_flag = 0;
        }*/

        if(playertype == "camper_player")
        {
            _agent.destination = Get_sorted_distance(camper_player[player_id], _target.position, "min");
            if (Vector3.Distance(transform.position, _agent.destination) <= 0.8 && Vector3.Distance(transform.position, _target.position) < 10f)
            {
                gun_aim();
                _gun.Shoot();
            }
        }
        else if(playertype == "hidden_player")
        {
            Vector3 direction = transform.position - _target.position;
            float angle = Vector3.Angle(_target.forward, direction);
            if (180f - angle <= 30)
            {
                _agent.destination = Get_sorted_distance(hidden_player[player_id], _target.position, "max");
            }
            else if (180 - angle > 30 && Vector3.Distance(transform.position, _target.position) < 20)
            {
                _agent.destination = Get_sorted_distance(hidden_player[player_id], _target.position, "min");
                gun_aim();
                _gun.Shoot();
            }
        }
        else if(playertype == "patroul_player")
        {
            _agent.destination = patroul_player[player_id][patroul_number];
            if(patroul_status==0 && patroul_number == 2)
            {
                patroul_status = 1;
            }
            else if(patroul_status==1 && patroul_number == 0)
            {
                patroul_status = 0;
            }
            
            if(Vector3.Distance(transform.position, _agent.destination)<1.0f && patroul_status==0)
            {  
                patroul_number += 1;
            }
            else if(Vector3.Distance(transform.position, _agent.destination) < 1.0f && patroul_status == 1)
            {
                patroul_number -= 1; 
            }
            if(Vector3.Distance(transform.position, _target.position) < 7f)
            {
                gun_aim();
                _gun.Shoot();
            }


        }
        else if(playertype == "attack_player")
        {
            _agent.destination = _target.position;
            _sword.Attack();
        }
        
    }

    public IEnumerator PlayerVisible()
    {
        if(_player.GetComponent<PlayerMovementController>().invisible)
        {
            _target=null;

            while(_player.GetComponent<PlayerMovementController>().invisible){
                yield return new WaitForSeconds(0.5f);
            }
            _target = _player;
        }
    }
}
