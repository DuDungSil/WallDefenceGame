using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TimeController : Singleton<TimeController>
{
    public delegate IEnumerator NightStart(); //밤에 할게 몬스터 생성밖에 없으니 IEnumerator 형식으로 선언해도 상관 없을듯
    public event NightStart nightActionDelegate;
    public Material daySkybox;
    public Material nightSkybox;
    public void NightActivate()
    {
        if(nightActionDelegate != null)
        {
            StartCoroutine(nightActionDelegate());
        }
    }
    public TextMeshProUGUI[] m_text_time;
    public Sprite[] m_sprites;
    public Image m_dayNightImage;
    public int currentRound = 1;
    public bool IsNight = false;
    float time;
    public Light sunLight;
    public Round[] rounds;
    
    // Start is called before the first frame update
    void Start()
    {
        time = rounds[0].Data.RoundDayTime;
        SoundController.Instance.PlaySound2D("daySound",0,true,SoundType.BGM);
        RenderSettings.skybox = daySkybox;
        DynamicGI.UpdateEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            m_text_time[0].text = ((int)time / 60%60).ToString(); //minute calculation
            m_text_time[1].text = ((int)time % 60).ToString(); //second calculation
        }
        else
        {
            UpdateDayNight();
        }
    }
    private void UpdateDayNight()
    {
        if(IsNight)
        {
            currentRound++;
            if(currentRound > rounds.Length)
            {
                UIController.Instance.OpenGameover();
            }
            else
            {
                RenderSettings.skybox = daySkybox;
                DynamicGI.UpdateEnvironment();
                SoundController.Instance.StopLoopSound("nightSound");
                SoundController.Instance.PlaySound2D("daySound",0,true,SoundType.BGM);
                time = rounds[currentRound - 1].Data.RoundDayTime;
                IsNight = false;
                sunLight.transform.rotation = Quaternion.Euler(50.0f,-30.0f,0);
                m_dayNightImage.sprite = m_sprites[0];
            }
        }
        else
        {
            if(currentRound == rounds.Length)
            {
                UIController.Instance.OpenGameover();
            }
            else
            {
                RenderSettings.skybox = nightSkybox;
                DynamicGI.UpdateEnvironment();
                SoundController.Instance.StopLoopSound("daySound");
                SoundController.Instance.PlaySound2D("nightSound",0,true,SoundType.BGM);
                time = rounds[currentRound - 1].Data.RoundNightTime;
                NightActivate();
                IsNight = true;
                sunLight.transform.rotation = Quaternion.Euler(150.0f,-30.0f,0);
                m_dayNightImage.sprite = m_sprites[1];
            }
        }
    }
}