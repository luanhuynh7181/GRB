using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar_Follower : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    private float curHealth;
    private float maxHealth;
    private Transform parent;

    void Awake()
    {
        SetActive(false);
        GetComponent<Canvas>().worldCamera = Camera.main;
        parent = transform.parent;
    }

    public void SetActive(bool b)
    {
        hpBar.gameObject.SetActive(b);
    }

    private void Update()
    {
        if (parent == null)
            return;
        var scale = hpBar.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (parent.localScale.x < 0 ? -1 : 1);
        hpBar.transform.localScale = scale;
    }

    public void setMaxHP(int max)
    {
        maxHealth = max;
        curHealth = maxHealth;
        UpdateBar();
    }

    public void ChangeHealth(float amount)
    {
        float curValue = hpBar.value;
        curHealth += amount;
        if (curHealth <= 0)
        {
            return;
        }

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        UpdateBar();
        RunPercent(curValue, hpBar.value, 0.1f);
    }

    public void UpdateBar()
    {
        hpBar.value = curHealth / maxHealth;
    }

    public void addHP(float percent)
    {
        float HP = maxHealth * percent;
        ChangeHealth(HP);
    }

    public bool isDead()
    {
        return curHealth <= 0;
    }

    public void RunPercent(float start, float targetValue, float duration)
    {
        StartCoroutine(UpdateHealthBar(start, targetValue, duration));
    }

    private IEnumerator UpdateHealthBar(float startValue, float targetValue, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            hpBar.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            yield return null; // Wait until the next frame
        }
        hpBar.value = targetValue; // Ensure the exact final value
    }
}
