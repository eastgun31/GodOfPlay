using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillManager : MonoBehaviour
{
    public static EnemySkillManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public GameObject enemySkills;

    public bool e_Zeus_S ;
    public bool e_Poseidon_S;
    public bool e_Hades_S;

    public bool e_Hephaestus_S;
    public bool e_Artemis_S;
    public bool e_Ares_S;

    public bool e_Hera_S;
    public bool e_Apollo_S;
    public bool e_Athena_S;
    public bool e_Aphrodite_S;

    public bool e_Hermes_S;
    public bool e_Hestia_S;
    public bool e_Dionysus_S;
    public bool e_Demeter_S;

    public int e_active_skillnum;
    public int e_buff_skillnum;
    public int e_item_skillnum;
    public int e_passiveNow;

    public int itemLimit;
    public float currentCooldown_1 = 0.0f; //현재 쿨타임
    public float currentCooldown_2 = 0.0f; //현재 쿨타임
    public float currentCooldown_3 = 0.0f; //현재 쿨타임
    public bool useSkill;
    public bool usingItem;
    public int random;


    public GameObject ZeusSkill; //제우스 액티브

    public GameObject ApolloSkill; //아폴론 액티브


    // Start is called before the first frame update
    void Start()
    {
        useSkill = true;
        itemLimit = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void E_UseSkill(Vector3 dir, Vector3 dir2)
    {
        //int random;

        if (itemLimit > 0)
            random = Random.Range(0, 3);
        else
            random = Random.Range(0, 2);

        if (random == 0 && currentCooldown_1 == 0f && useSkill)
        {
            switch (e_active_skillnum)
            {
                case 1:
                    UseZeusSkill(dir);
                    break;
                case 2:
                    UsePoseidonSkill();
                    break;
                case 3:
                    UseHadesSkill();
                    break;
            }
        }

        if (random == 1 && currentCooldown_2 == 0f && useSkill)
        {
            switch(e_buff_skillnum)
            {
                case 1:
                    UseHeraSkill();
                    break;
                case 2:
                    UseApolloSkill(dir2);
                    break;
                case 3:
                    UseAthenaSkill();
                    break;
                case 4:
                    UseAphroditeSkill();
                    break;
            }
        }

        if(random == 2 && currentCooldown_3 == 0f && useSkill)
        {
            switch(e_item_skillnum)
            {
                case 1:
                    UseHermesSkill();
                    break;
                case 2:
                    UseHestiaSkill();
                    break;
                case 3:
                    UseDionysusSkill();
                    break;
                case 4:
                    UseDemeterSkill();
                    break;
            }
        }
    }

    //스킬 쿨다운
    private IEnumerator Num1_Skill_Cooldown(float cooldown)
    {
        currentCooldown_1 = cooldown; //쿨타임 설정

        while (currentCooldown_1 >= 0.0f)
        {
            currentCooldown_1 -= Time.deltaTime;
            useSkill = true;
            yield return null;
        }

        currentCooldown_1 = 0f;
        //Debug.Log(cooldown + " 스킬 쿨타임 종료!");
    }

    private IEnumerator Num2_Skill_Cooldown(float cooldown)
    {
        currentCooldown_2 = cooldown; //쿨타임 설정

        while (currentCooldown_2 >= 0.0f)
        {
            currentCooldown_2 -= Time.deltaTime;
            useSkill = true;
            yield return null;
        }

        currentCooldown_2 = 0f;
        //Debug.Log(cooldown + " 스킬 쿨타임 종료!");
    }

    private IEnumerator Num3_Skill_Cooldown(float cooldown)
    {
        currentCooldown_3 = cooldown; //쿨타임 설정

        while (currentCooldown_3 >= 0.0f)
        {
            currentCooldown_3 -= Time.deltaTime;
            useSkill = true;
            yield return null;
        }

        currentCooldown_3 = 0f;
        //Debug.Log(cooldown + " 스킬 쿨타임 종료!");
    }

    IEnumerator DeactiveSkill(GameObject skill)
    {
        yield return new WaitForSeconds(3f); // 비활성화까지의 대기 시간(3초)
        skill.SetActive(false);
    }


    //1번 액티브 스킬-------------------------------------------------------------------------------------------
    //제우스 스킬
    public void UseZeusSkill(Vector3 dir)
    {
        ZeusSkill.SetActive(true);
        ZeusSkill.transform.position = dir;

        Collider[] colliders = Physics.OverlapSphere(dir, 4.5f, LayerMask.GetMask("Unit"));
        foreach (Collider collider in colliders)
        {
            UnitController unit = collider.GetComponent<UnitController>();
            if (unit != null)
            {
                // 스킬로 인한 데미지 적용
                unit.ZuesDamage(20);
            }
        }

        useSkill = false;

        StartCoroutine(Num1_Skill_Cooldown(5f));
        StartCoroutine(DeactiveSkill(ZeusSkill));
    }
    void UsePoseidonSkill()
    {
        useSkill = false;
        StartCoroutine(Num1_Skill_Cooldown(5f));
    }
    void UseHadesSkill()
    {
        useSkill = false;
        StartCoroutine(Num1_Skill_Cooldown(5f));
    }
    //2번 액티브 스킬-------------------------------------------------------------------------------------------
    void UseHeraSkill()
    {
        useSkill = false;
        StartCoroutine(Num2_Skill_Cooldown(3f));
    }
    //아폴론 스킬
    void UseApolloSkill(Vector3 dir)
    {
        ApolloSkill.SetActive(true);
        ApolloSkill.transform.position = dir;

        Collider[] colliders = Physics.OverlapSphere(dir, 4.5f, LayerMask.GetMask("E_Unit"));
        foreach (Collider collider in colliders)
        {
            E_unitMove e_unit = collider.GetComponent<E_unitMove>();
            if (e_unit != null)
            {
                //힐량 조정
                e_unit.ApolloHeal(50);
            }
        }

        useSkill = false;
        StartCoroutine(Num2_Skill_Cooldown(3f));
        StartCoroutine(DeactiveSkill(ApolloSkill));
    }
    void UseAthenaSkill()
    {
        useSkill = false;
        StartCoroutine(Num2_Skill_Cooldown(3f));
    }
    void UseAphroditeSkill()
    {
        useSkill = false;
        StartCoroutine(Num2_Skill_Cooldown(3f));
    }

    //소모스킬-------------------------------------------------------------------------------------------
    void UseHermesSkill() //이동속도
    {
        useSkill = false;
        usingItem = true;
        StartCoroutine(Num3_Skill_Cooldown(3f));
        itemLimit--;
    }

    void UseHestiaSkill() //회복
    {
        useSkill = false;
        usingItem = true;
        StartCoroutine(Num3_Skill_Cooldown(3f));
        itemLimit--;
    }

    void UseDionysusSkill() //공격력
    {
        useSkill = false;
        usingItem = true;
        StartCoroutine(Num3_Skill_Cooldown(3f));
        itemLimit--;
    }

    void UseDemeterSkill() //재화
    {
        useSkill = false;

        EnemySpawn.instance.gold += 100;

        StartCoroutine(Num3_Skill_Cooldown(3f));
        itemLimit--;
    }
}
