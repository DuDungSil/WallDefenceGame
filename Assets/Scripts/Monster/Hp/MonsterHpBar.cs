using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MonsterHpBar : MonoBehaviour
{
    public Transform greenBar; // The transform of the green bar
    public Transform redBar; // The transform of the red bar

    private float maxHealth; // The maximum health value
    private MonsterManager monsterManager;
    private Vector3 originalGreenBarScale; // The original scale of the green bar
    private Vector3 originalRedBarScale; // The original scale of the red bar

    void Start()
    {
        monsterManager = GetComponentInParent<MonsterManager>();

        maxHealth = monsterManager.Hp;

        originalGreenBarScale = greenBar.localScale;
        originalRedBarScale = redBar.localScale;

        monsterManager.onStatusChanged += UpdateHealthBar;
        UpdateHealthBar();
    }


    private void UpdateHealthBar()
    {
        // Calculate the health percentage
        float healthPercentage = monsterManager.Hp / maxHealth;
        Debug.Log(healthPercentage);

        // Update the scale of the green bar
        greenBar.localScale = new Vector3(healthPercentage * originalGreenBarScale.x, originalGreenBarScale.y, originalGreenBarScale.z);

        // Update the position of the green bar to the left
        greenBar.localPosition = new Vector3(-(1 - healthPercentage) / 2, greenBar.localPosition.y, greenBar.localPosition.z);

        // Update the scale of the red bar
        redBar.localScale = new Vector3((1 - healthPercentage) * originalRedBarScale.x, originalRedBarScale.y, originalRedBarScale.z);

        // Update the position of the red bar to the right
        redBar.localPosition = new Vector3(healthPercentage / 2, redBar.localPosition.y, redBar.localPosition.z);
    }
}
