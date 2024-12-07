using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int maxAmmoCount;
    [SerializeField] private int damageCount;
    [SerializeField] private int fireRange;
    [SerializeField] private float reloadTime;
    [SerializeField] private float fireRate; 
    
    [SerializeField] private KeyCode reloadKey = KeyCode.R;
    [SerializeField] private KeyCode shotKey = KeyCode.Mouse0;

    [SerializeField] private LayerMask enemyMask;
    
    private int currentAmmo;
    private bool isReloading = false;
    private bool isFiring = false; 
    

    public void Start()
    {
        currentAmmo = maxAmmoCount;
    }

    public void Update()
    {
        ReloadAmmo();
        Shoot();
    }

    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Input.GetKeyDown(shotKey) && !isReloading && currentAmmo > 0 && !isFiring)
        {
            StartCoroutine(ShootWithDelay());
        }
        else if (Input.GetKeyDown(shotKey) && !isReloading && currentAmmo <= 0)
        {
            StartCoroutine(ReloadAmmoCoroutine());
        }
    }

    public void ReloadAmmo()
    {
        if (Input.GetKey(reloadKey) && !isReloading)
        {
            StartCoroutine(ReloadAmmoCoroutine());
        }
    }

    IEnumerator ReloadAmmoCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmoCount;
        isReloading = false; 
    }

    IEnumerator ShootWithDelay()
    {
        isFiring = true;
        currentAmmo--;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, fireRange, enemyMask))
        {
            if (hit.collider != null)
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageCount);
                    Debug.Log($"Игрок нанес {damageCount} урона");
                }
            }
        }
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }
}