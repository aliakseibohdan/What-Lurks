using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float maxHealth = 100f;

    private float lerpTimer;
    private float chipSpeed = 2f;

    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (Input.GetKeyUp(KeyCode.T))
        {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }

    public void UpdateHealthUI()
    {
        var fillAmountFront = frontHealthBar.fillAmount;
        var fillAmountBack = backHealthBar.fillAmount;

        var healthFraction = health / maxHealth;

        if (fillAmountBack > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;

            lerpTimer += Time.deltaTime;
            var lerpCompletePercent = lerpTimer / chipSpeed;

            lerpCompletePercent = lerpCompletePercent * lerpCompletePercent;
            backHealthBar.fillAmount = Mathf.Lerp(fillAmountBack, healthFraction, lerpCompletePercent);
        }
        if (fillAmountFront < healthFraction)
        {
            backHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.green;

            lerpTimer += Time.deltaTime;
            var lerpCompletePercent = lerpTimer / chipSpeed;

            lerpCompletePercent = lerpCompletePercent * lerpCompletePercent;
            frontHealthBar.fillAmount = Mathf.Lerp(fillAmountFront, fillAmountBack, lerpCompletePercent);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
