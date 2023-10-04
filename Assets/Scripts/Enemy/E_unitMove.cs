using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class E_unitMove : MonoBehaviour
{
    public GameObject[] points;
    public int unitNum;

    float speed = 7f;
    public float health;
    float time = 3f;
    Rigidbody rigid;

    NavMeshAgent moving;
    public Vector3 lastDesti;
    public UnitController targetUnit;

    public float ehealth;
    public float eattackPower;
    public float edefense;
    public float emoveSpeed;

    float attackspeed;

    public Slider Eslider;
    public float maxhp;
    public Points point;

    private Animator enemyAnim;

    enum E_UnitState
    {
        Battle, Idle, find_target
    }

    E_UnitState e_unitBattle;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        moving = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();

        maxhp = ehealth;
       StartCoroutine(Pcheck());
    }

    private void FixedUpdate()
    {
        //if (ehealth <= 0)
        //{
        //    Invoke("E_Die", 3f);
        //}

        Eslider.value = ehealth / maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void MovePoint(Vector3 i)
    {
        lastDesti = i;
        moving.speed = emoveSpeed;
        moving.SetDestination(i);

        enemyAnim.SetFloat("run", Vector3.Distance(transform.position,i));

        transform.SetParent(null);
    }

    public void Attakc(Vector3 dir, UnitController p_unit)
    {
        if (ehealth <= 0)
            return;
        
        //moving.isStopped = true;
        //moving.velocity = Vector3.zero;

        if(p_unit.uhealth > 0)
        {
            targetUnit = p_unit;
            Find_Target(dir, p_unit);
        }

        //Find_Target(dir, p_unit);

        //moving.SetDestination(dir);
        //moving.stoppingDistance = 1f;

        //if (unitNum == 2 || unitNum == 6 || unitNum == 10)
        //{
        //    moving.stoppingDistance = 4f;
        //}

        //enemyAnim.SetFloat("run", emoveSpeed);
        //transform.position = Vector3.MoveTowards(transform.position, dir, attackspeed * Time.deltaTime);

        //if (Vector3.Distance(transform.position, dir) <= 3f && p_unit.uhealth > 0)
        //{
        //    transform.LookAt(dir);
        //    attackspeed = 0;
        //    //rigid.velocity = Vector3.zero;

        //    StartCoroutine(Damage(p_unit));

            //if(time > 1f)
            //{
            //    time = 0;
            //    Debug.Log("공격");
            //    enemyAnim.SetTrigger("attack");
            //    p_unit.uhealth -= eattackPower;
            //}
            //rigid.velocity = Vector3.zero;
            //rigid.angularVelocity = Vector3.zero;
        //}
        //else if(Vector3.Distance(transform.position, dir) > 3f && p_unit.uhealth > 0)
        //{
        //    attackspeed = emoveSpeed;
        //    transform.position = Vector3.MoveTowards(transform.position, dir, attackspeed * Time.deltaTime);
        //    enemyAnim.SetFloat("run", attackspeed);
        //}
        //else if (p_unit.uhealth <= 0)
        //{
        //    targetUnit = null;
        //}

        //if ( targetUnit == null)
        //{
        //    Debug.Log("다시 출발");
        //    moving.isStopped = false;
        //    enemyAnim.SetFloat("run", Vector3.Distance(transform.position, lastDesti));
        //    moving.SetDestination(lastDesti);
        //}
    }

    void Find_Target(Vector3 dir, UnitController p_unit)
    {
        //attackspeed = emoveSpeed;
        //transform.position = Vector3.MoveTowards(transform.position, dir, attackspeed * Time.deltaTime);

        //moving.isStopped = false;
        moving.SetDestination(dir);
        moving.stoppingDistance = 2f;

        if (p_unit.uhealth <= 0)
        {
            targetUnit = null;
        }

        if (Vector3.Distance(transform.position, dir) <= 3f && p_unit.uhealth > 0)
        {
            moving.isStopped = true;
            moving.velocity = Vector3.zero;

            transform.LookAt(dir);
            
            StartCoroutine(Damage(p_unit));
        }
        else if(Vector3.Distance(transform.position, dir) > 3f)
        {
            moving.isStopped = false;
            moving.SetDestination(dir);
            moving.stoppingDistance = 2f;
            enemyAnim.SetFloat("run", attackspeed);
        }
    }

    IEnumerator Damage(UnitController p_unit)
    {
        if (p_unit.uhealth > 0 && time > 2f)
        {
            time = 0;
            enemyAnim.SetTrigger("attack");
            p_unit.uhealth -= 10f;
            Debug.Log("공격");

            yield return new WaitForSeconds(1f);

            StartCoroutine(Damage(p_unit));
        }

        if (p_unit.uhealth <= 0)
        {
            targetUnit = null;
            Debug.Log("적 죽음");

            if (targetUnit == null && ehealth > 0)
            {
                Debug.Log("다시 출발");
                moving.isStopped = false;
                enemyAnim.SetFloat("run", Vector3.Distance(transform.position, lastDesti));
                moving.SetDestination(lastDesti);
            }

            StopCoroutine("Damage");
        }
    }

    void E_Die()
    {
        moving.isStopped = true;
        moving.velocity = Vector3.zero;

        GameManager.instance.e_population--;
        GameManager.instance.gold += 2;

        if (point)
        {
            point.e_distance = 100f;
        }

        //enemyAnim.SetTrigger("death");

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (unitNum == 0)
        {
            ehealth = GameManager.instance.health;
            eattackPower = GameManager.instance.attackPower;
            edefense = GameManager.instance.defense;
            emoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitNum == 1)
        {
            ehealth = GameManager.instance.health + 50;
            eattackPower = GameManager.instance.attackPower - 2;
            edefense = GameManager.instance.defense + 2;
            emoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitNum == 2)
        {
            ehealth = GameManager.instance.health - 20;
            eattackPower = GameManager.instance.attackPower + 3;
            edefense = GameManager.instance.defense - 1;
            emoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitNum == 3)
        {
            ehealth = GameManager.instance.health + 100;
            eattackPower = GameManager.instance.attackPower + 5;
            edefense = GameManager.instance.defense + 5;
            emoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        if (unitNum == 4)
        {
            ehealth = GameManager.instance.health + 50;
            eattackPower = GameManager.instance.attackPower + 5;
            edefense = GameManager.instance.defense + 5;
            emoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitNum == 5)
        {
            ehealth = GameManager.instance.health + 100;
            eattackPower = GameManager.instance.attackPower + 3;
            edefense = GameManager.instance.defense + 7;
            emoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitNum == 6)
        {
            ehealth = GameManager.instance.health + 30;
            eattackPower = GameManager.instance.attackPower + 8;
            edefense = GameManager.instance.defense + 4;
            emoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitNum == 7)
        {
            ehealth = GameManager.instance.health + 150;
            eattackPower = GameManager.instance.attackPower + 10;
            edefense = GameManager.instance.defense + 10;
            emoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        if (unitNum == 8)
        {
            ehealth = GameManager.instance.health + 100;
            eattackPower = GameManager.instance.attackPower + 10;
            edefense = GameManager.instance.defense + 10;
            emoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitNum == 9)
        {
            ehealth = GameManager.instance.health + 150;
            eattackPower = GameManager.instance.attackPower + 8;
            edefense = GameManager.instance.defense + 12;
            emoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitNum == 10)
        {
            ehealth = GameManager.instance.health + 80;
            eattackPower = GameManager.instance.attackPower + 13;
            edefense = GameManager.instance.defense + 9;
            emoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitNum == 11)
        {
            ehealth = GameManager.instance.health + 200;
            eattackPower = GameManager.instance.attackPower + 15;
            edefense = GameManager.instance.defense + 15;
            emoveSpeed = GameManager.instance.moveSpeed + 3;
        }
    }

    IEnumerator Pcheck()
    {
        if (ehealth <= 0)
        {
            moving.isStopped = true;
            moving.velocity = Vector3.zero;

            GameManager.instance.e_population--;
            GameManager.instance.gold += 2;

            if (point)
            {
                point.e_distance = 100f;
            }

            enemyAnim.SetTrigger("death");

            yield return new WaitForSeconds(3f);

            Destroy(gameObject);

            StopCoroutine(Pcheck());

            //GameManager.instance.e_population--;
            //if (point)
            //{
            //    point.e_distance = 100f;
            //}

            //enemyAnim.SetTrigger("death");

            //yield return new WaitForSeconds(4f);

            //Destroy(gameObject);
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(Pcheck());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            point = other.GetComponent<Points>();
        }
    }

    public void ZuesDamage(float damage)
    {
        ehealth -= damage;
    }
}
