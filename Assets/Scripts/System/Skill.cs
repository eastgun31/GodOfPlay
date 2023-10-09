using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Skills
{
    Zeus,       //제우스        1 A  o
    Poseidon,   //포세이돈      2 A
    Hades,      //하데스        3 A
    Hera,       //헤라          4 B
    Apollo,     //아폴론        5 B  o
    Athena,     //아테나        6 B
    Aphrodite,  //아프로디테    7 B
    Hermes,     //헤르메스      8 I
    Hestia,     //헤스티아      9 I
    Dionysus,   //디오니소스    10 I  o
    Demeter,     //데메테르     11 I  o
    Hephaestus, //헤파이토스    12 P  o
    Artemis,    //아르테미스    13 P  o
    Ares       //아레스         14 P  o
}

public class Skill : MonoBehaviour
{
    public static Skill instance;

    float originalDamage; // 능력치 변경 이전의 데미지
    float originalSpeed; // 능력치 변경 이전의 속도

    float buffDuration = 5f; // 스킬 지속 시간
    bool isBuffActive = false; // 스킬 활성화 여부
    public int itemLimit = 3; // 스킬 횟수 제한

    public GameObject skillRangePrefab; // 범위 표시용 프리팹
    private GameObject skillRangeInstance; // 범위 표시용 인스턴스

    private float skillRangeHeight = 0.1f; // 스킬 범위 높이
    private bool isShowSkillRange = false; //범위 표시여부 

    bool isSkillReady_1 = false;
    bool isSkillReady_2 = false;

    public float currentCooldown_1 = 0.0f; //현재 쿨타임
    public float currentCooldown_2 = 0.0f; //현재 쿨타임

    public GameObject ZeusSkill; //제우스 액티브

    public GameObject ApolloSkill; //아폴론 액티브

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // 마우스 위치에 스킬 범위를 따라다니도록 업데이트
        if (isShowSkillRange)
        {
            UpdateSkillRangePosition();
        }

        //마우스 클릭
        if (Input.GetMouseButtonDown(0))
        {
            if (isSkillReady_1 && isShowSkillRange)
            {
                if (Skill_Set.instance.Zeus_S)
                    UseZeusSkill();
                else if (Skill_Set.instance.Poseidon_S)
                    UsePoseidonSkill();
                else if (Skill_Set.instance.Hades_S)
                    UseHadesSkill();
                else { }
            }

            if (isSkillReady_2 && isShowSkillRange)
            {
                if (Skill_Set.instance.Hera_S)
                    UseHeraSkill();
                else if (Skill_Set.instance.Apollo_S)
                    UseApolloSkill();
                else if (Skill_Set.instance.Athena_S)
                    UseAthenaSkill();
                else if (Skill_Set.instance.Aphrodite_S)
                    UseAphroditeSkill();
                else { }
            }
        }

        // 1번 키를 누를 때 스킬 범위를 표시
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentCooldown_1 <= 0f)
        {
            if (isShowSkillRange)
            {
                isSkillReady_1 = false;
                CancelSkill();
            }
            else
            {
                isSkillReady_1 = true;
                isSkillReady_2 = false;
                ShowSkillRange();
            }
        }
        // 2번 키를 누를 때 스킬 범위를 표시
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentCooldown_2 <= 0f)
        {
            if (isShowSkillRange)
            {
                isSkillReady_2 = false;
                CancelSkill();
            }
            else
            {
                isSkillReady_2 = true;
                isSkillReady_1 = false;
                ShowSkillRange();
            }
        }
        // 3번 키를 누르면 3번 스킬 사용
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isBuffActive && itemLimit > 0)
        {
            if (Skill_Set.instance.Hermes_S)
                UseHermesSkill();
            else if (Skill_Set.instance.Hestia_S)
                UseHestiaSkill();
            else if (Skill_Set.instance.Dionysus_S)
                UseDionysusSkill();
            else if (Skill_Set.instance.Demeter_S)
                UseDemeterSkill();
            else { }
        }

    }
    //------------------------------------------------------------------

    //스킬 범위 표시
    void ShowSkillRange()
    {
        isShowSkillRange = true;

        // 스킬 범위 표시용 프리팹을 인스턴스화
        //skillRangeInstance = Instantiate(skillRangePrefab);
        skillRangePrefab.SetActive(true);
    }

    //스킬 취소, 스킬을 사용해서 표시되는 범위를 삭제할때도 사용
    void CancelSkill()
    {
        isShowSkillRange = false;
        //Destroy(skillRangeInstance);
        skillRangePrefab.SetActive(false);
    }

    //스킬사용 위치
    void UpdateSkillRangePosition()
    {
        // 마우스 위치를 기반으로 스킬 범위 인스턴스 이동
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 바닥에만 충돌했을 때 스킬 범위 이동
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 newPosition = hit.point;
            newPosition.y += skillRangeHeight; // Y 좌표 조절
            //skillRangeInstance.transform.position = newPosition;
            skillRangePrefab.transform.position = newPosition;
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
        Debug.Log(cooldown + " 스킬 쿨타임 종료!");
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
        Debug.Log(cooldown + " 스킬 쿨타임 종료!");
    }

    IEnumerator DeactiveSkill(GameObject skill)
    {
        yield return new WaitForSeconds(3f); // 비활성화까지의 대기 시간(3초)
        skill.SetActive(false);
    }

    //1번 액티브 스킬-------------------------------------------------------------------------------------------
    //제우스 스킬
    void UseZeusSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            ZeusSkill.SetActive(true);
            ZeusSkill.transform.position = spawnPosition;
            //ZeusSkill = Instantiate(skillNum_1, spawnPosition, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("E_Unit"));
            foreach (Collider collider in colliders)
            {
                E_unitMove E_unit = collider.GetComponent<E_unitMove>();
                if (E_unit != null)
                {
                    // 스킬로 인한 데미지 적용
                    E_unit.ZuesDamage(20);
                }
            }
        }
        isSkillReady_1 = false;

        CancelSkill();
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            ApolloSkill.SetActive(true);
            ApolloSkill.transform.position = spawnPosition;
            //ApolloSkill = Instantiate(skillNum_5, spawnPosition, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("Unit"));
            foreach (Collider collider in colliders)
            {
                UnitController unit = collider.GetComponent<UnitController>();
                if (unit != null)
                {
                    //힐량 조정
                    unit.ApolloHeal(50);
                }
            }
        }
        isSkillReady_2 = false;

        CancelSkill();
        StartCoroutine(Num2_Skill_Cooldown(3f)); //쿨타임 적용
        StartCoroutine(DeactiveSkill(ApolloSkill));
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
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //리스트안에 있는 유닛 전부에게 적용
            if (IsUnitInList(unit))
            {
                originalSpeed = unit.umoveSpeed;
                unit.umoveSpeed += 3;

                Transform hermesSkill = unit.transform.GetChild(0);

                hermesSkill.gameObject.SetActive(true);

                StartCoroutine(BuffDelay(unit, originalSpeed, 5.0f, hermesSkill.gameObject));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }

    void UseHestiaSkill() //회복
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //리스트안에 있는 유닛 전부에게 적용
            if (IsUnitInList(unit))
            {
                unit.uhealth += 20;

                Transform hestiaSkill = unit.transform.GetChild(1);

                hestiaSkill.gameObject.SetActive(true);

                StartCoroutine(BuffDelay(unit, 0, 2.0f, hestiaSkill.gameObject));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }

    void UseDionysusSkill() //공격력
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //리스트안에 있는 유닛 전부에게 적용
            if (IsUnitInList(unit))
            {
                originalDamage = unit.uattackPower;
                unit.uattackPower += 5;

                Transform dionysusSkill = unit.transform.GetChild(2);

                dionysusSkill.gameObject.SetActive(true);

                StartCoroutine(BuffDelay(unit, originalDamage, 5.0f, dionysusSkill.gameObject));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }

    void UseDemeterSkill() //재화
    {
        GameManager.instance.gold += 100;

        itemLimit--;
    }

    private IEnumerator BuffDelay(UnitController unit, float originalValue, float delayInSeconds, GameObject effectObject)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // 원래 값으로 되돌립니다.
        if (Skill_Set.instance.Hermes_S)
        {
            unit.umoveSpeed = originalValue;
            isBuffActive = false;
        }

        if (Skill_Set.instance.Ares_S)
        {
            unit.uattackPower = originalValue;
            isBuffActive = false;
        }

        effectObject.SetActive(false);
    }


    //유닛 확인------------------------------------------------------------------------------------------
    //유닛들이 리스트에 있는지 확인
    private bool IsUnitInList(UnitController unit)
    {
        return RTSUnitController.instance.UnitList.Contains(unit);
    }


    // 다른 이벤트 또는 조건에서 스킬을 사용 가능하게 하려면 isSkillReady를 true로 설정하십시오.
    //public void SetSkillReady(bool skillReady)
    //{
    //    isSkillReady = skillReady;
    //}
}
