using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Staff : DistanceWeanponControl
{
    [Header("스태프 설정")]
    [Space(5)]

    public GameObject Ray;

    public GameObject spell;
    public GameObject spellRange;

    public float duration;

    private GameObject _spell;
    private GameObject _spellRange;
    private Vector3 castPoint;
    private bool canCastSpell;
    private bool isCasting;
    private Coroutine _endMagicAfterDuration;

    // 실제 생성 프리팹
    // 범위 프리팹

    void Start()
    {
        _spellRange = Instantiate(spellRange, gameObject.transform.position, Quaternion.identity);
        _spellRange.SetActive(false);
        canCastSpell = false;
        isCasting = false;
        Ray.GetComponent<RayControl>().rayDistance = range;
    }

    void Update()
    {
        // 딜레이 끝나면
        if(isCoolTime == false)
        {
            // 선택 중인 인터렉터들의 목록
            var selectingInteractors = GetComponent<XRGrabInteractable>().interactorsSelecting;

            // 선택 중인 인터렉터의 수
            int selectCount = selectingInteractors.Count;

            if(selectCount >= 2)
            {
                Ray.SetActive(true);
                castPoint = Ray.GetComponent<RayControl>().getRayHitPoint();
                if(castPoint != Vector3.zero)
                {
                    _spellRange.transform.position = castPoint;
                    _spellRange.SetActive(true);
                    canCastSpell = true;
                }
                else
                {
                    _spellRange.SetActive(false);
                }
            }
            else
            {
                Ray.SetActive(false);
                _spellRange.SetActive(false);
            }

        }
        else
        {
            Ray.SetActive(false);
            _spellRange.SetActive(false);
        }
    }

    public void Casting()
    {
        if(canCastSpell)
        {
            isCasting = true;
            isCoolTime = true;
            _spellRange.SetActive(false);
            canCastSpell = false;

            // castPoint에 소환
            _spell = Instantiate(spell, castPoint, Quaternion.identity);

            _spell.GetComponent<Spell>().duration = duration;
            _spell.GetComponent<Spell>().maxDamage = damage;

            _endMagicAfterDuration = StartCoroutine(EndMagicAfterDuration());

        }     
    }

    public void CastingCancel()
    {
        if(isCasting)
        {
            StopCoroutine(_endMagicAfterDuration);
            Destroy(_spell);
            isCasting = true;

            StartCoroutine(ActivateCooldown(coolTime));
        }
    }

    protected IEnumerator EndMagicAfterDuration()
    {
        yield return new WaitForSeconds(duration + 0.2f);
        Destroy(_spell);
        isCasting = false;

        StartCoroutine(ActivateCooldown(coolTime));
    }

    void OnDestroy()
    {
        if(_spellRange != null) Destroy(_spellRange);
        if(_spell != null) Destroy(_spell);
    }
}
