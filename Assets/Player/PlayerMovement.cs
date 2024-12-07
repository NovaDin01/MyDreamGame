using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField] private float playerSpeed = 5f;
    private CharacterController _ch;
    private Vector3 direction;
    

    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    private void Start() 
    {
        _ch = GetComponent<CharacterController>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Если камера не назначена в инспекторе, используем основную камеру.
        }
    }
    
    private void InputSystem()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 inputDirection = new Vector3(moveHorizontal, 0, moveVertical);

        // Преобразуем направление в локальные координаты камеры.
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Убираем влияние наклона камеры (если она наклонена вверх/вниз).
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Получаем глобальное направление движения.
        direction = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;
    }

    private void Update() 
    {
        InputSystem();
        _ch.Move(direction * playerSpeed * Time.deltaTime);
    }
}




