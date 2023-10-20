using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private GameObject unitMarker;
    private NavMeshAgent navMeshAgent;
    private Animator playerAnim;
    private Rigidbody rigid;

    public int unitnumber = 0;

    public float uhealth;
    public float uattackPower;
    public float udefense;
    public float umoveSpeed;

    float attackspeed;

    float time = 3f;    //공격 쿨타임
    public E_unitMove targetUnit;   //공격할 유닛
    public Points point; // 점령중인 거점

    public int unitType; //유닛병종

    public Slider Uslider;
    public float maxhp;

    //최적화 변수들
    string run = "run";
    string attack = "attack";
    string _point = "Point";

    //하데스 변수
    public bool isHades = false;
    public GameObject attackRange;


    public enum unitState //유닛상태머신
    {
        Battle, Idle, goPoint
    }

    public unitState u_State;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(Pcheck());
        playerAnim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        maxhp = uhealth;
    }

    private void FixedUpdate()
    {
        navMeshAgent.speed = umoveSpeed;

        Uslider.value = uhealth / maxhp;

        //if (uhealth <= 0)
        //{
        //    Invoke("P_Die", 3f);
        //}

    }

    public void SelectUnit()
    {
        unitMarker.SetActive(true);
    }

    public void DeselectUnit()
    {
        unitMarker.SetActive(false);
    }

    public void MoveTo(Vector3 end)
    {
        if(uhealth > 0)
        {
            attackRange.SetActive(false);
            navMeshAgent.SetDestination(end);
            Invoke("ReAttack", 2f);
        }
    }

    void ReAttack()
    {
        attackRange.SetActive(true);
    }

    void Update()
    {
        time += Time.deltaTime;

        switch (u_State)
        {
            case unitState.Idle:
                U_Idle();
                break;
            case unitState.goPoint:
                U_GoPoint();
                break;
        }

        playerAnim.SetFloat(run, navMeshAgent.velocity.magnitude);
    }

    void U_Idle()
    {
        time = 0;

        targetUnit = null;
        navMeshAgent.isStopped = false;

        if(time > 1)
        {
            time = 0;
            StopAllCoroutines();
        }
    }

    void U_GoPoint()
    {

    }

    void RemoveList()
    {
        RTSUnitController.instance.UnitList.Remove(this);
        RTSUnitController.instance.selectedUnitList.Remove(this);

        //Destroy(gameObject);
    }

    public void Attack(Vector3 dir, E_unitMove e_unit)  //플레이어유닛 공격
    {
        if (uhealth <= 0)
            return;

        //time += Time.deltaTime;
        //float attackspeed = umoveSpeed;

        
        //navMeshAgent.isStopped = true;
        //navMeshAgent.velocity = Vector3.zero;

        //if (Input.GetMouseButtonDown(1))
        //{
        //    u_State = unitState.Idle;
        //}

        if(e_unit.ehealth>0)
        {
            targetUnit = e_unit;
            Find_Target(dir, e_unit);
        }

        //if (unitnumber == 2 || unitnumber == 6 || unitnumber == 10)
        //{
        //    navMeshAgent.stoppingDistance = 4f;
        //}

        //transform.position = Vector3.MoveTowards(transform.position, dir, attackspeed * Time.deltaTime);
        //playerAnim.SetFloat("run", attackspeed);

        //if (Vector3.Distance(transform.position, dir) <= 3f && e_unit.ehealth > 0)
        //{
        //    transform.LookAt(dir);
        //    attackspeed = 0;

        //    StartCoroutine(Damage(e_unit));
        //    //rigid.velocity = Vector3.zero;
        //    //if(time > 1f)
        //    //{
        //    //    time = 0;
        //    //    Debug.Log("적공격");
        //    //    playerAnim.SetTrigger("attack");
        //    //    e_unit.ehealth -= uattackPower;
        //    //}
        //}
        //else if(Vector3.Distance(transform.position, dir) > 3f)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, dir, attackspeed * Time.deltaTime);
        //    attackspeed = umoveSpeed;
        //    playerAnim.SetFloat("run", attackspeed);
        //}
        //else if (e_unit.ehealth <= 0)
        //{
        //    targetUnit = null;
        //}
    }

    void Find_Target(Vector3 dir, E_unitMove e_unit)
    {
        //attackspeed = umoveSpeed;
        //transform.position = Vector3.MoveTowards(transform.position, dir, attackspeed * Time.deltaTime);

        //navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(dir);
        navMeshAgent.stoppingDistance = 2f;

        if(e_unit.ehealth <= 0)
        {
            targetUnit = null;
        }


        if (unitType == 1 && Vector3.Distance(transform.position, dir) <= 10f && e_unit.ehealth > 0)
        {
            //navMeshAgent.stoppingDistance = 6f;
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;

            StartCoroutine(Damage(dir, e_unit));
        }
        else if (Vector3.Distance(transform.position, dir) <= 3f && e_unit.ehealth > 0)
        {
            //navMeshAgent.stoppingDistance = 2f;
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;

            //transform.LookAt(dir);
            //time = 0;
            StartCoroutine(Damage(dir, e_unit));
        }
        else if (Vector3.Distance(transform.position, dir) > 3f)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(dir);
            navMeshAgent.stoppingDistance = 2f;

            //playerAnim.SetFloat("run", attackspeed);
        }
    }


    IEnumerator Damage(Vector3 dir, E_unitMove e_unit)
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        //u_State = unitState.Battle;

        if (e_unit.ehealth > 0 && time > 2f && u_State == unitState.Battle)
        {
            time = 0;
            transform.LookAt(dir);
            playerAnim.SetTrigger(attack);
            e_unit.ehealth -= 10f;
            Debug.Log("적 공격");

            yield return wait;

            StartCoroutine(Damage(dir, e_unit));
        }

        if (e_unit.ehealth <= 0)
        {
            u_State = unitState.Idle;
            targetUnit = null;
            Debug.Log("적들 죽음");
            navMeshAgent.isStopped = false;
            StopCoroutine("Damage");
        }
    }


    void P_Die()    //플레이어 유닛 죽음
    {
        RTSUnitController.instance.UnitList.Remove(this);
        RTSUnitController.instance.selectedUnitList.Remove(this);

        GameManager.instance.All_Obj--;
        GameManager.instance.Aobj();
        EnemySpawn.instance.gold += 2; //아군 유닛 죽였을 때 적 재화 획득

        if (point)
        {
            point.p_distance = 100f;
        }

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (unitnumber == 0)
        {
            unitType = 0;
            uhealth = GameManager.instance.health;
            uattackPower = GameManager.instance.attackPower;
            udefense = GameManager.instance.defense;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 1)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower - 2;
            udefense = GameManager.instance.defense + 2;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 2)
        {
            unitType = 1;
            uhealth = GameManager.instance.health - 20;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense - 1;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 3)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        if (unitnumber == 4)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 5)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense + 7;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 6)
        {
            unitType = 1;
            uhealth = GameManager.instance.health + 30;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 4;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 7)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }


        if (unitnumber == 8)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 9)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 12;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 10)
        {
            unitType = 0;
            unitType = 1;
            uhealth = GameManager.instance.health + 80;
            uattackPower = GameManager.instance.attackPower + 13;
            udefense = GameManager.instance.defense + 9;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 11)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 200;
            uattackPower = GameManager.instance.attackPower + 15;
            udefense = GameManager.instance.defense + 15;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        //패시브--------------------------------------------------------------------
        //검사 패시브가 켜지면   
        if (Skill_Set.instance.Ares_S)
        {
            if (unitnumber == 0 || unitnumber == 4 || unitnumber == 8)
            {
                uhealth += 30;
                uattackPower += 3;
                udefense += 3;
            }
        }
        //방패병
        if (Skill_Set.instance.Hephaestus_S)
        {
            if (unitnumber == 1 || unitnumber == 5 || unitnumber == 9)
            {
                uhealth += 30;
                uattackPower += 3;
                udefense += 3;
            }
        }
        //궁수
        if (Skill_Set.instance.Artemis_S)
        {
            if (unitnumber == 2 || unitnumber == 6 || unitnumber == 10)
            {
                uhealth += 30;
                uattackPower += 3;
                udefense += 3;
            }
        }   
    }

    //IEnumerator Pcheck()
    //{
    //    if (uhealth <= 0)
    //    {

    //        //GameManager.instance.All_Obj--;
    //        //GameManager.instance.Aobj();
    //        //Destroy(gameObject, 4f);
    //        //StopCoroutine(Pcheck());
    //    }

    //    yield return new WaitForSeconds(1f);
    //    StartCoroutine(Pcheck());
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_point))
        {
            point = other.GetComponent<Points>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_point))
        {
            point.p_distance = 100f;
        }
    }

    IEnumerator Pcheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        if (uhealth <= 0 && isHades)
        {
            uhealth = maxhp / 2;
            isHades = false;
        }

        if (uhealth <= 0)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;

            RTSUnitController.instance.UnitList.Remove(this);
            RTSUnitController.instance.selectedUnitList.Remove(this);

            GameManager.instance.All_Obj--;
            GameManager.instance.Aobj();
            EnemySpawn.instance.gold += 2; //아군 유닛 죽였을 때 적 재화 획득


            playerAnim.SetTrigger("death");
            yield return new WaitForSeconds(3f);

            if (point)
            {
                point.p_distance = 100f;
            }

            Destroy(gameObject);
            StopCoroutine(Pcheck());
        }

        yield return wait;
        StartCoroutine(Pcheck());
    }

    public void ZuesDamage(float damage)
    {
        uhealth -= damage;
    }

    public void ApolloHeal(float heal)
    {
        uhealth += heal;
    }

    public void AphroditeChange(Vector3 spawnPoint, Vector3 pointPosition)
    {
        StartCoroutine(_AphroditeChange(spawnPoint, pointPosition));
    }

    public IEnumerator _AphroditeChange(Vector3 spawnPoint, Vector3 pointPosition)
    {
        if (unitnumber == 0 || unitnumber == 4 || unitnumber == 8)
            EnemySpawn.instance.Aphrodite_Warrior(spawnPoint, pointPosition);
        else if (unitnumber == 1 || unitnumber == 5 || unitnumber == 9)
            EnemySpawn.instance.Aphrodite_Shield(spawnPoint, pointPosition);
        else if (unitnumber == 2 || unitnumber == 6 || unitnumber == 10)
            EnemySpawn.instance.Aphrodite_Archer(spawnPoint, pointPosition);
        else
            EnemySpawn.instance.Aphrodite_HorseMan(spawnPoint, pointPosition);

        uhealth = 0;

        yield return new WaitForSeconds(0.2f);

        uhealth = 0;

        if (point)
        {
            point.p_distance = 100f;
        }

        RTSUnitController.instance.UnitList.Remove(this);
        RTSUnitController.instance.selectedUnitList.Remove(this);

        GameManager.instance.All_Obj--;


        Destroy(gameObject);
    }

}

