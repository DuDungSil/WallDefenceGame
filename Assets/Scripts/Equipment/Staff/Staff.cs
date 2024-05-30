using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Staff : RangedWeaponControl
{
    public GameObject Ray;
    public float duration;

    public GameObject spell;
    public GameObject spellRange;

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
        _spellRange = Instantiate(spellRange, gameObject.transform);
        _spellRange.SetActive(false);
        canCastSpell = false;
        isCasting = false;
    }

    void Update()
    {
        // 딜레이 끝나면
        if(shootActivate == true)
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
            shootActivate = false;
            _spellRange.SetActive(false);
            canCastSpell = false;

            // castPoint에 소환
            _spell = Instantiate(spell, castPoint, Quaternion.identity);
            _endMagicAfterDuration = StartCoroutine(EndMagicAfterDuration());

            StartCoroutine(ShootDelay(shootDelay));
        }     
    }

    public void CastingCancel()
    {
        if(isCasting)
        {
            StopCoroutine(_endMagicAfterDuration);
            Destroy(_spell);
            isCasting = false;
        }
    }

    protected IEnumerator EndMagicAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(_spell);
        isCasting = false;
    }
}
