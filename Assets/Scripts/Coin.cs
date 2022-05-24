using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _speedRotations = 40.0f;

    void Update()
    {
        transform.Rotate(0, _speedRotations * Time.deltaTime , 0);//вращение монет по оси у на 40 градусов каждый кадр в сцене МЕНЮ
    }
}
