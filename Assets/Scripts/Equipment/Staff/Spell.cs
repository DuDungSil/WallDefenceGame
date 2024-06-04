using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ProjectileControl
{
    public GameObject hitbox;
    public float tickCycle = 1f;
    public float duration;
    public float maxDamage;
    private int tickCount;

    void Start()
    {
        tickCount = (int)(duration / tickCycle);
        damage = maxDamage / tickCount;
        setLifeTime(duration + 1);
        
        StartCoroutine(TickDelay());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    protected IEnumerator TickDelay()
    {
        for(int i = 0; i < tickCount; i++)
        {
            hitbox.SetActive(true);

            yield return new WaitForSeconds(0.1f);

            hitbox.SetActive(false);
            
            yield return new WaitForSeconds(tickCycle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }
}
