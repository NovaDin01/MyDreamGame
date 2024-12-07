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
    [SerializeField] private float attackRadius;
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
            StartCoroutine(EnemyAttack());
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} погиб от рук врага:)");
        Destroy(gameObject);
        _exp.GetXP(50); // Передаём опыт игроку
    }

    private IEnumerator EnemyAttack()
    {
        _isAttacking = true;

        Collider[] hitPlayers = Physics.OverlapSphere(pointAttack.position, attackRadius, playerMask);
        foreach (Collider target in hitPlayers)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(enemyDamage);
                Debug.Log($"{target.gameObject.name} получил {enemyDamage} урона.");
            }
        }

        yield return new WaitForSeconds(enemySpeedOfAttack);
        _isAttacking = false;
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
            Gizmos.DrawWireSphere(pointAttack.position, attackRadius); // Визуализация радиуса атаки
        }
    }
}