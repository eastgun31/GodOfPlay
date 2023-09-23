using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public Points point;

    private Animator enemyAnim;

    enum EState
    {
        idle, go, attack, die
    }

    EState eunits;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        moving = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();

        //StartCoroutine(Pcheck());
    }

    private void FixedUpdate()
    {
        if (ehealth <= 0)
        {
            //eunits = EState.die;
            Invoke("E_Die", 2f);
        }

    }


    // Update is called once per frame
    void Update()
    {
        //if (ehealth <= 0)
        //{
        //    Invoke("E_Die", 4f);
        //}

        //switch(eunits)
        //{
        //    case EState.idle:
        //        Euidle();
        //        break;
        //    case EState.go:
        //        EuGo();
        //        break;
        //    case EState.attack:
        //        EuAttack();
        //        break;
        //    case EState.die:
        //        EuDie();
        //        break;
        //}
    }

    void Euidle()
    {
        moving.isStopped = true;
        moving.velocity = Vector3.zero;
    }
    void EuGo()
    {
        enemyAnim.SetTrigger("run");
    }
    void EuAttack()
    {
        enemyAnim.SetTrigger("attack");
    }
    void EuDie()
    {
        enemyAnim.SetTrigger("death");
    }

    public void MovePoint(Vector3 i)
    {
        if(targetUnit == null)
        {
            lastDesti = i;
            moving.speed = emoveSpeed;
            moving.SetDestination(i);

            //eunits = EState.go;
            enemyAnim.SetFloat("run", moving.remainingDistance);
        }

        transform.SetParent(null);
    }

    public void Attakc(Vector3 dir, UnitController p_unit)
    {
        time += Time.deltaTime;

        targetUnit = p_unit;
        moving.SetDestination(dir);
        moving.stoppingDistance = 2f;

        if (unitNum == 2 || unitNum == 6 || unitNum == 10)
        {
            moving.stoppingDistance = 4f;
        }

        if (time > 1f && p_unit.uhealth > 0)
        {
            //eunits = EState.idle;
            moving.isStopped = true;
            moving.velocity = Vector3.zero;
            Debug.Log("АјАн");
            //eunits = EState.attack;
            enemyAnim.SetTrigger("attack");
            p_unit.uhealth -= eattackPower;
            time = 0;
        }

        if (p_unit.uhealth <= 0)
        {
            eunits = EState.idle;
            targetUnit = null;
        }

        if (targetUnit == null)
        {
            moving.isStopped = false;
            moving.SetDestination(lastDesti);
        }
    }


    void E_Die()
    {
        GameManager.instance.e_population--;
        if(point)
        {
            point.e_distance = 100f;
        }

        enemyAnim.SetTrigger("death");

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
            GameManager.instance.e_population--;
            if (point)
            {
                point.e_distance = 100f;
            }

            enemyAnim.SetTrigger("death");

            yield return new WaitForSeconds(4f);

            Destroy(gameObject);
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
