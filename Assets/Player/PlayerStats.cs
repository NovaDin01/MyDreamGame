using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private float playerHealth = 100f;

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log($"Игрок получил {damage} урона. Осталось здоровья: {playerHealth}");

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Игрок погиб!");
        // Логика смерти
    }
}