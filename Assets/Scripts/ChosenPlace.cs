using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenPlace : MonoBehaviour
{
    public Transform place;

    public void Awake()
    {
        for (int i = 0; i < place.childCount; i++)
            place.GetChild(i).gameObject.SetActive(false);

        place.GetChild(PlayerPrefs.GetInt("chosenSkin")).gameObject.SetActive(true);
    }
}
