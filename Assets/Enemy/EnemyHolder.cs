using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour, IDamageable
{
    [SerializeField] private float enemyHealth; // Исправлено название переменной
    [SerializeField] private float enemyDamage;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private float enemySpeedOfAttack;

    private float currentEnemyHealth;
    [SerializeField] private Transform pointAttack;
    [SerializeField] private LayerMask playerMask;
    private Rigidbody _rb;

    private Experience _exp;
    private bool _isAttacking = false;

    private void Start()
    {
        currentEnemyHealth = enemyHealth; // Устанавливаем текущее здоровье
        _rb = GetComponent<Rigidbody>();
        _exp = FindObjectOfType<Experience>();
    }

    private void Update()
    {
        if (!_isAttacking) // Проверяем, не идёт ли уже атака
        {
            Attack();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} погиб от рук");
        Destroy(gameObject);
        _exp.GetXP(50); // Передаём опыт игроку
    }

    private void Attack()
    {
        StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyAttack()
    {
        _isAttacking = true; // Устанавливаем флаг начала атаки

        Collider[] hitPlayers = Physics.OverlapSphere(pointAttack.position, 2f, playerMask);
        foreach (Collider player in hitPlayers)
        {
            if (player.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(enemyDamage);
                Debug.Log($"{player.gameObject.name} получил урон");
            }
        }

        yield return new WaitForSeconds(enemySpeedOfAttack); // Ожидание перед следующей атакой
        _isAttacking = false; // Сбрасываем флаг
    }

    public void TakeDamage(float damage)
    {
        currentEnemyHealth -= damage; // Вычитаем урон из текущего здоровья
        Debug.Log($"Противник получил {damage} урона. Осталось здоровья: {currentEnemyHealth}");

        if (currentEnemyHealth <= 0)
        {
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointAttack.position, 2f); // Визуализация радиуса атаки
        }
    }
}