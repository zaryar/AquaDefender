using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using static Weapons;
using JetBrains.Annotations;

public class tentacles : EnemyTemplate
{
    // Start is called before the first frame updat 
    public string blendShapeName = "BlendShapeName";
    public float speed = 1f;
    private SkinnedMeshRenderer t1_skinned_mesh_renderer;
    private SkinnedMeshRenderer t2_skinned_mesh_renderer;
    private SkinnedMeshRenderer t3_skinned_mesh_renderer;
    private SkinnedMeshRenderer t4_skinned_mesh_renderer;
    private SkinnedMeshRenderer t5_skinned_mesh_renderer;
    private SkinnedMeshRenderer t6_skinned_mesh_renderer;
    private SkinnedMeshRenderer t7_skinned_mesh_renderer;
    private SkinnedMeshRenderer t8_skinned_mesh_renderer;
    private SkinnedMeshRenderer t9_skinned_mesh_renderer;
    private SkinnedMeshRenderer ausholen_t1_rend;
    private SkinnedMeshRenderer ausholen_t2_rend;
    private SkinnedMeshRenderer ausholen_t3_rend;
    private SkinnedMeshRenderer ausholen_t4_rend;
    GameObject kraken_obj; 
    GameObject kraken_ausholen_obj;
    private int frame;
    private int count_up;
   

    [HideInInspector] public Transform _target;
    [HideInInspector] public Transform _player;
    [HideInInspector] public NavMeshAgent _agent;
    public event Action OnDeath;
    public event Action OnAttack;
    private bool isFreezed = false;
   
    int attack_animation = 0;
    int old_attack_animation = 0; 
    int attack_mode = 1;
    float old_distance = 0;
    int water_attack = 0;
    int water_frames = 0; 

    
    [SerializeField] GameObject waterAmmunition;
    [SerializeField] GameObject WaterDust;
    [SerializeField] public int maximumHealth;


    Transform bulletSpawnPoint;
    Transform bulletSpawnPoint2;
    Transform hitpoint1;
    Transform hitpoint2;
    Transform hitpoint3;
    Transform hitpoint4;

    public int hitdamage;
    Health playerscript;


    public int getmaxHealth()
    {
        return maximumHealth; 
    }

    void Start()
    {
        playerscript = GameObject.Find("Player").GetComponent<Health>();
        Transform kraken = transform.Find("kraken");
        Transform kraken_ausholen = transform.Find("kraken_ausholen"); 
        Transform tentacle_1 = kraken.Find("Tentacle_1_mesh");
        Transform tentacle_2 = kraken.Find("Tentacle_2_mesh");
        Transform tentacle_3 = kraken.Find("Tentacle_3_mesh");
        Transform tentacle_4 = kraken.Find("Tentacle_4_mesh");
        Transform tentacle_5 = kraken.Find("Tentacle_5_mesh");
        Transform tentacle_6 = kraken.Find("Tentacle_6_mesh");
        Transform tentacle_7 = kraken.Find("Tentacle_7_mesh");
        Transform tentacle_8 = kraken.Find("Tentacle_8_mesh");
        Transform tentacle_9 = kraken.Find("Tentacle_9_mesh");
        t1_skinned_mesh_renderer = tentacle_1.GetComponent<SkinnedMeshRenderer>();
        t2_skinned_mesh_renderer = tentacle_2.GetComponent<SkinnedMeshRenderer>();
        t3_skinned_mesh_renderer = tentacle_3.GetComponent<SkinnedMeshRenderer>();
        t4_skinned_mesh_renderer = tentacle_4.GetComponent<SkinnedMeshRenderer>();
        t5_skinned_mesh_renderer = tentacle_5.GetComponent<SkinnedMeshRenderer>();
        t6_skinned_mesh_renderer = tentacle_6.GetComponent<SkinnedMeshRenderer>();
        t7_skinned_mesh_renderer = tentacle_7.GetComponent<SkinnedMeshRenderer>();
        t8_skinned_mesh_renderer = tentacle_8.GetComponent<SkinnedMeshRenderer>();
        t9_skinned_mesh_renderer = tentacle_9.GetComponent<SkinnedMeshRenderer>();
        Transform ausholen_t1 = kraken_ausholen.Find("T1");
        Transform ausholen_t2 = kraken_ausholen.Find("T2");
        Transform ausholen_t3 = kraken_ausholen.Find("T3");
        Transform ausholen_t4 = kraken_ausholen.Find("T4");
        ausholen_t1_rend = ausholen_t1.GetComponent<SkinnedMeshRenderer>();
        ausholen_t2_rend = ausholen_t2.GetComponent<SkinnedMeshRenderer>();
        ausholen_t3_rend = ausholen_t3.GetComponent<SkinnedMeshRenderer>();
        ausholen_t4_rend = ausholen_t4.GetComponent<SkinnedMeshRenderer>();
        //Debug.Log(getHealth()); 
        SetHealth(maximumHealth);
        //Debug.Log(getHealth());
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        _target = _player;
        kraken_obj = GameObject.Find("kraken");
        kraken_ausholen_obj = GameObject.Find("kraken_ausholen");
        bulletSpawnPoint = transform.Find("bulletSpawnPoint").transform;
        bulletSpawnPoint2 = transform.Find("bulletSpawnPoint2").transform;
        hitpoint1 = transform.Find("hitpoint1").transform;
        hitpoint2 = transform.Find("hitpoint2").transform;
        hitpoint3 = transform.Find("hitpoint3").transform;
        hitpoint4 = transform.Find("hitpoint4").transform;
        
    }

    public void orient_player()
    {
        if (_target != null && !isFreezed)
        {
            Vector3 dir = _target.position - transform.position;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
     
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
      
        }
    }

    public void waterattack()
    {
        Vector3 startPoint = bulletSpawnPoint.position;
        Vector3 direction = _target.position - startPoint;
        float distance = direction.magnitude;
        Vector3 normalizedDirection = direction.normalized;
        Vector3 velocity = normalizedDirection * 20;
        kraken_obj.SetActive(false);
        Vector3 t3_offset = new Vector3(0f, 0f, 4f);
        var waterBullet = Instantiate(waterAmmunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        waterBullet.GetComponent<WeaponTemplate>().setOpposingFraction(new[] { "Player" });
        waterBullet.GetComponent<Rigidbody>().velocity = velocity;
        startPoint = bulletSpawnPoint2.position;
        direction = _target.position - startPoint;
        distance = direction.magnitude;
        normalizedDirection = direction.normalized;
        velocity = normalizedDirection * 20;
        var waterBullet2 = Instantiate(waterAmmunition, bulletSpawnPoint2.position, bulletSpawnPoint2.rotation);
        waterBullet2.GetComponent<WeaponTemplate>().setOpposingFraction(new[] { "Player" });
        waterBullet2.GetComponent<Rigidbody>().velocity = velocity;
    }


    public void follow_attack(int frame, int water)
    {
        if(water == 1)
        {
            waterattack();
            return; 
        } 
        if(Vector3.Distance(transform.position, _target.position) > 4.0f && attack_mode == 1)
        {
            _agent.destination = _target.position;
        }
        else if (Vector3.Distance(transform.position, _target.position) < 4.0f && attack_mode == 1)
        {
            attack_animation = 1;
            attack_mode = 0;
        }
        else
        { 
            Vector3 direction = _target.position - transform.position;
            direction.Normalize();
            _agent.destination = _target.position - 7 * direction;
            if ((Vector3.Distance(transform.position, _target.position)-old_distance) < -0.1f)
            {
                attack_mode = 1; 
            }
        }
        old_distance = Vector3.Distance(transform.position, _target.position);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(getHealth()); 

        if (attack_animation != old_attack_animation)
        {
            frame = 0; 
        }
        
        if (frame == 100)
        {
            count_up = 0;
        }
        else if (frame == 0)
        {
            count_up = 1;
        }

        if (count_up == 1)
        {
            frame = frame + 5; 
        }
        else
        {
            frame = frame -20; 
        }
        old_attack_animation = attack_animation;
        if (attack_animation == 0 && frame == 0)
        {
            float randomNumber = UnityEngine.Random.value;
            if (randomNumber > 0.5)
            {
                water_attack = 1; 
            }
        }
        if(water_attack == 1 && water_frames < 10)
        {
            water_frames++; 
            kraken_obj.SetActive(false);
            kraken_ausholen_obj.SetActive(true);
            //ausholen_t2_rend.SetBlendShapeWeight(0, 29);
            //ausholen_t3_rend.SetBlendShapeWeight(0, 27);
            //ausholen_t1_rend.SetBlendShapeWeight(0, 100);
            ausholen_t2_rend.SetBlendShapeWeight(0, 40);
            ausholen_t3_rend.SetBlendShapeWeight(0, 40);
            //ausholen_t4_rend.SetBlendShapeWeight(0, 100);
            follow_attack(100, 1);
            frame = 0; 
        }
        else
        {
            water_frames = 0;
            water_attack = 0; 
            if (attack_animation == 0)
            {
                 
                kraken_obj.SetActive(true);
                kraken_ausholen_obj.SetActive(false);
                t1_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                t2_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                t3_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                t4_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                t5_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                t6_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                t7_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                t8_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                t9_skinned_mesh_renderer.SetBlendShapeWeight(0, frame);
                follow_attack(100, 0);
            }
            else
            {
                
                kraken_obj.SetActive(false);
                kraken_ausholen_obj.SetActive(true);
                ausholen_t1_rend.SetBlendShapeWeight(0, frame);
                ausholen_t2_rend.SetBlendShapeWeight(0, frame);
                ausholen_t3_rend.SetBlendShapeWeight(0, frame);
                ausholen_t4_rend.SetBlendShapeWeight(0, frame);
                playerscript.TakeDamage(hitdamage);

                if (count_up == 0 && frame == 0)
                {
                    Instantiate(WaterDust, hitpoint1.position, Quaternion.identity);
                    Instantiate(WaterDust, hitpoint2.position, Quaternion.identity);
                    Instantiate(WaterDust, hitpoint3.position, Quaternion.identity);
                    Instantiate(WaterDust, hitpoint4.position, Quaternion.identity);
                    Instantiate(WaterDust, hitpoint1.position, Quaternion.identity);
                    Instantiate(WaterDust, hitpoint2.position, Quaternion.identity);
                    Instantiate(WaterDust, hitpoint3.position, Quaternion.identity);
                    Instantiate(WaterDust, hitpoint4.position, Quaternion.identity);
                    old_distance = Vector3.Distance(transform.position, _target.position);
                    attack_animation = 0;
                }
                else
                {
                    follow_attack(100, 0);

                }
            }
        }
           
        orient_player();
       
    }
}
