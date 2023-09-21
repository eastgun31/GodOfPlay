using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private GameObject unitMarker;
    private NavMeshAgent navMeshAgent;

    public int unitnumber = 0;

    public float uhealth;
    public float uattackPower;
    public float udefense;
    public float umoveSpeed;

    float time = 3f;    //공격 쿨타임
    public E_unitMove targetUnit;   //공격할 유닛
    public Points point; // 점령중인 거점

    public Slider Uslider;
    public float maxhp;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //StartCoroutine(Pcheck());
        maxhp = uhealth;
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
        navMeshAgent.SetDestination(end);
    }

    private void FixedUpdate()
    {
        navMeshAgent.speed = umoveSpeed;

        Uslider.value = uhealth / maxhp;

        if (uhealth <= 0)
        {
            Invoke("P_Die", 4f);
        }

    }

    void Update()
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
        if (Input.GetMouseButtonDown(1))
            return;

        time += Time.deltaTime;

        targetUnit = e_unit;
        navMeshAgent.SetDestination(dir);
        navMeshAgent.stoppingDistance = 2f;


        if (unitnumber == 2 || unitnumber == 6 || unitnumber == 10)
        {
            navMeshAgent.stoppingDistance = 4f;
        }

        if (time > 1f && e_unit.ehealth > 0)
        {
            Debug.Log("적공격");
            e_unit.ehealth -= uattackPower;
            time = 0;
        }
        else if (e_unit.ehealth <= 0)
        {
            targetUnit = null;
        }
    }

    void P_Die()    //플레이어 유닛 죽음
    {
        RTSUnitController.instance.UnitList.Remove(this);
        RTSUnitController.instance.selectedUnitList.Remove(this);

        GameManager.instance.All_Obj--;
        GameManager.instance.Aobj();
        EnemySpawn.instance.gold += 2; //아군 유닛 죽였을 때 적 재화 획득

        if(point)
        {
            point.p_distance = 100f;
        }

        Destroy(gameObject);
    }

    public void ApolloHeal(float heal)
    {
        uhealth += heal;
    }

    private void OnEnable()
    {
        if (unitnumber == 0)
        {
            uhealth = GameManager.instance.health;
            uattackPower = GameManager.instance.attackPower;
            udefense = GameManager.instance.defense;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 1)
        {
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower - 2;
            udefense = GameManager.instance.defense + 2;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 2)
        {
            uhealth = GameManager.instance.health - 20;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense - 1;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 3)
        {
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        if (unitnumber == 4)
        {
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 5)
        {
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense + 7;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 6)
        {
            uhealth = GameManager.instance.health + 30;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 4;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 7)
        {
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }


        if (unitnumber == 8)
        {
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 9)
        {
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 12;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 10)
        {
            uhealth = GameManager.instance.health + 80;
            uattackPower = GameManager.instance.attackPower + 13;
            udefense = GameManager.instance.defense + 9;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 11)
        {
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

    //        GameManager.instance.All_Obj--;
    //        GameManager.instance.Aobj();
    //        Destroy(gameObject, 4f);
    //        StopCoroutine(Pcheck());
    //    }

    //    yield return new WaitForSeconds(1f);
    //    StartCoroutine(Pcheck());
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Point"))
        {
            point = other.GetComponent<Points>();
        }
    }
}

