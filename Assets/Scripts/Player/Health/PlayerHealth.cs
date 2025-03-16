using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float maxHealth = 100f;

    private float lerpTimer;
    private float chipSpeed = 2f;

    [Header("Health Bar")]
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField] public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    [Header("Damage Overlay")]
    [SerializeField] private Image overlay;
    [SerializeField] private float effectDuration;
    [SerializeField] private float fadeSpeed;
    private float effectDurationTimer;


    private void Start()
    {
        health = MaxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, MaxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0f)
        {
            if (health < 30)
                return;

            effectDurationTimer += Time.deltaTime;
            if (effectDurationTimer > effectDuration)
            {
                var tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        var fillAmountFront = frontHealthBar.fillAmount;
        var fillAmountBack = backHealthBar.fillAmount;

        var healthFraction = health / MaxHealth;

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
        effectDurationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
