using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum Skills
{
    Zeus,       //제우스        1 A
    Poseidon,   //포세이돈      2 A
    Hades,      //하데스        3 A
    Hera,       //헤라          4 B
    Apollo,     //아폴론        5 B
    Athena,     //아테나        6 B
    Aphrodite,  //아프로디테    7 B
    Hermes,     //헤르메스      8 E
    Hestia,     //헤스티아      9 E
    Dionysus,   //디오니소스    10 E
    Demeter,     //데메테르     11 E
    Hephaestus, //헤파이토스    12 P
    Artemis,    //아르테미스    13 P
    Ares       //아레스         14 P
}

public class Skill : MonoBehaviour
{
    //public static Skill instance;
    public Skill_Set skill_Set;

    float originalDamage; // 능력치 변경 이전의 데미지
    float buffDuration = 5f; // 스킬 지속 시간
    bool isBuffActive = false; // 스킬 활성화 여부
    int buffLimit = 3; // 스킬 횟수 제한

    public GameObject skillRangePrefab; // 범위 표시용 프리팹
    private GameObject skillRangeInstance; // 범위 표시용 인스턴스

    private float skillRangeHeight = 0.1f; // 스킬 범위 높이

    private bool isSkillReady = true; // 스킬 사용 가능한지 여부
    private bool isShowSkillRange = false; //범위 표시여부 

    bool isSkillReady_1 = false;
    bool isSkillReady_2 = false;
    bool isSkillReady_3 = false;

    float skillCooldown_1 = 5.0f; //쿨타임
    float skillCooldown_2 = 10.0f; //쿨타임
    float currentCooldown_1 = 0.0f; //현재 쿨타임
    float currentCooldown_2 = 0.0f; //현재 쿨타임

    public GameObject skillNum_1; //제우스 액티브
    private GameObject ZeusSkill;

    public GameObject skillNum_5; //아폴론 액티브  
    private GameObject ApolloSkill;

    public GameObject skillNum_10; //디오니소스 공버프 소모
    private GameObject DionysusSkill;

    public GameObject skills { get; set; }

    void Awake()
    {
        //instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
        SkillCoolDown();

        // 마우스 위치에 스킬 범위를 따라다니도록 업데이트
        if (skillRangeInstance != null)
        {
            UpdateSkillRangePosition();
        }
        //마우스 클릭
        if (Input.GetMouseButtonDown(0) && skillRangeInstance != null)
        {
            if (isSkillReady_1 && skillRangeInstance != null && isShowSkillRange)
                if (skill_Set.Zeus_S)
                    UseZeusSkill();
                else if (skill_Set.Poseidon_S)
                    UsePoseidonSkill();
                else if (skill_Set.Hades_S)
                    UseHadesSkill();
                else { }
            if (isSkillReady_2 && skillRangeInstance != null && isShowSkillRange)
                if (skill_Set.Hera_S)
                    UseHeraSkill();
                else if (skill_Set.Apollo_S)
                    UseApolloSkill();
                else if (skill_Set.Athena_S)
                    UseAthenaSkill();
                else if (skill_Set.Aphrodite_S)
                    UseAphroditeSkill();
                else { }
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
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isBuffActive && buffLimit > 0)
        {
            if (skill_Set.Hermes_S)
                UseHermesSkill();
            else if (skill_Set.Hestia_S)
                UseHestiaSkill();
            else if (skill_Set.Dionysus_S)
                UseDionysusSkill();
            else if (skill_Set.Demeter_S)
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
        skillRangeInstance = Instantiate(skillRangePrefab);
    }

    //스킬 취소
    void CancelSkill()
    {
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
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
            skillRangeInstance.transform.position = newPosition;
        }
    }

    //스킬 쿨다운
    void SkillCoolDown()
    {
        if (currentCooldown_1 >= 0.0f)
        {
            currentCooldown_1 -= Time.deltaTime;
        }

        if (currentCooldown_2 >= 0.0f)
        {
            currentCooldown_2 -= Time.deltaTime;
        }
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

            ZeusSkill = Instantiate(skillNum_1, spawnPosition, Quaternion.identity);

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

        currentCooldown_1 = skillCooldown_1; //쿨타임 적용
        isSkillReady_1 = false;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
        Destroy(ZeusSkill, 7f);
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

            ApolloSkill = Instantiate(skillNum_5, spawnPosition, Quaternion.identity);

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

        currentCooldown_2 = skillCooldown_2; //쿨타임 적용
        isSkillReady_2 = false;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
        Destroy(ApolloSkill, 1.8f);
    }
    void UseAthenaSkill()
    {

    }
    void UseAphroditeSkill()
    {

    }
    //소모스킬-------------------------------------------------------------------------------------------
    void UseHermesSkill()
    {

    }

    void UseHestiaSkill()
    {

    }
    //디오니소스 공격력 버프 소모 형식
    void UseDionysusSkill()
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //리스트안에 있는 유닛 전부에게 적용
            if (IsUnitInList(unit))
            {
                originalDamage = unit.uattackPower;
                unit.uattackPower += 5;

                Vector3 unitPosition = unit.transform.position;
                Transform unitChild = unit.transform.GetChild(0);
                Vector3 spawnPosition = unitChild.position;
                DionysusSkill = Instantiate(skillNum_10, spawnPosition, Quaternion.identity);
                DionysusSkill.transform.parent = unitChild;

                StartCoroutine(RevertVariableAfterDelay(unit, originalDamage, 5.0f));
                Destroy(DionysusSkill, 5f);
            }
        }

        buffLimit--;
        isBuffActive = true;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    void UseDemeterSkill()
    {

    }

    private IEnumerator RevertVariableAfterDelay(UnitController unit, float originalValue, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // 원래 값으로 되돌립니다.
        unit.uattackPower = originalValue;
        isBuffActive = false;
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
