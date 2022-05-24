using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;// ссылка на персонажа для расчета новой позиции камеры

    public Vector3 offset; //расстаяние между игроком и камерой

    void Start()
    {
        offset = transform.position - player.position; // изменение позиции камеры  
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z); //новое место для перемещения камеры
        transform.position = newPosition; //перемещение камеры на новую позицию
        
    }
}
