using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponNumber
{
    firstWeapon,
    secondWeapon
}

public class WeaponSystem : MonoBehaviour
{
    public WeaponNumber currentWeapon;
    public WeaponNumber[] weaponList;
    public int currentWeaponIndex;

    [SerializeField] public GameObject firstWeapon;
    [SerializeField] public GameObject secondWeapon;

    public bool isSwitching = false; // Флаг, чтобы избежать частых переключений

    public void Start()
    {
        weaponList = (WeaponNumber[])System.Enum.GetValues(typeof(WeaponNumber)); // Получаем все значения перечисления
        currentWeaponIndex = 0;
        currentWeapon = weaponList[currentWeaponIndex];
        SetWeapon(); // Устанавливаем начальное оружие
    }

    public void Update()
    {
        ChangeWeapon(); // Изменение оружия при прокрутке колесика мыши
        SetWeapon(); // Устанавливаем оружие согласно индексу
    }

    public void ChangeWeapon()
    {
        if (isSwitching) return; // Если уже в процессе переключения, не переключаем оружие

        if (Input.GetAxis("Mouse ScrollWheel") > 0) // Прокрутка вверх
        {
            StartCoroutine(SwitchWeapon(1)); // Запускаем корутину для переключения на следующее оружие
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // Прокрутка вниз
        {
            StartCoroutine(SwitchWeapon(-1)); // Запускаем корутину для переключения на предыдущее оружие
        }
    }

    // Корутину для переключения оружия с задержкой
    public IEnumerator SwitchWeapon(int direction)
    {
        isSwitching = true; // Устанавливаем флаг, что оружие переключается

        // Обновляем индекс оружия
        currentWeaponIndex = (currentWeaponIndex + direction + weaponList.Length) % weaponList.Length;

        currentWeapon = weaponList[currentWeaponIndex]; // Обновляем текущее оружие

        yield return new WaitForSeconds(0.2f); // Задержка перед следующим переключением (например, 0.2 секунды)

        isSwitching = false; // Снимаем флаг, разрешая следующее переключение
    }

    public void SetWeapon()
    {
        // Управляем активностью оружий в зависимости от текущего индекса
        if (currentWeaponIndex == 0)
        {
            firstWeapon.SetActive(true);
            secondWeapon.SetActive(false);
        }
        else if (currentWeaponIndex == 1)
        {
            firstWeapon.SetActive(false);
            secondWeapon.SetActive(true);
        }
    }
}
