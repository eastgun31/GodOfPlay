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

    public int itemLimit = 3;
    public float currentCooldown_1 = 0.0f; //현재 쿨타임
    public float currentCooldown_2 = 0.0f; //현재 쿨타임


    public GameObject ZeusSkill; //제우스 액티브

    public GameObject ApolloSkill; //아폴론 액티브


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void E_UseSkill()
    {
        int random;

        if (itemLimit > 0)
            random = Random.Range(0, 3);
        else
            random = Random.Range(0, 2);

        if (random == 0 && currentCooldown_1 == 0f)
        {
            switch (e_active_skillnum)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
        else if (random == 1 && currentCooldown_2 == 0f)
        {
            switch(e_buff_skillnum)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
        else if(random == 2)
        {
            switch(e_item_skillnum)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
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
            yield return null;
        }

        currentCooldown_2 = 0f;
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

        StartCoroutine(Num1_Skill_Cooldown(5f));
        StartCoroutine(DeactiveSkill(ZeusSkill));
    }
    void UsePoseidonSkill()
    {

    }
    void UseHadesSkill()
    {

    }
    //2번 액티브 스킬-------------------------------------------------------------------------------------------
    void UseHeraSkill()
    {

    }
    //아폴론 스킬
    void UseApolloSkill()
    {

    }
    void UseAthenaSkill()
    {

    }
    void UseAphroditeSkill()
    {

    }

    //소모스킬-------------------------------------------------------------------------------------------
    void UseHermesSkill() //이동속도
    {

    }

    void UseHestiaSkill() //회복
    {

    }

    void UseDionysusSkill() //공격력
    {

    }

    void UseDemeterSkill() //재화
    {

    }
}
