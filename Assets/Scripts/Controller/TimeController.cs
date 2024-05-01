using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : Singleton<TimeController>
{
    public TextMeshProUGUI[] m_text_time;
    
    public float m_dayTime = 600;
    public float m_nightTime = 600;
    public bool IsNight = false;
    float time;
    public Light sunLight;
    
    // Start is called before the first frame update
    void Start()
    {
        time = m_dayTime;
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
            if(IsNight)
            {
                time = m_dayTime;
                IsNight = false;
                sunLight.transform.rotation = Quaternion.Euler(50.0f,-30.0f,0);
            }
            else{
                time = m_nightTime;
                IsNight = true;
                sunLight.transform.rotation = Quaternion.Euler(270.0f,-30.0f,0);
            }
        }
    }
}
