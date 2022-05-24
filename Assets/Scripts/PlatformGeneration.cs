using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] private Transform player;

    public GameObject[] tilePrefabs;//масив с тайлами

    private List<GameObject> activeTiles = new List<GameObject>();//список с активными тайлами
    private float spawnPos = -5;
    private float tileLength = 100;
    private int startTiles = 10; //количевство изначало генерируемых тайлов

    void Start()
    {
        for (int i = 0; i < startTiles; i++)
        {
            if (i == 0)
                SpawnPlatforms(10);
            SpawnPlatforms(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        if (player.position.z - 30 > spawnPos - (startTiles * tileLength)) //если положение по z больше чем разница стартовой позиции и длины дороги то
        {                                                                  //спавним новую дорогу и удаляем ту что сзади нас
            SpawnPlatforms(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnPlatforms(int tileIndex) {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation); //переменная для записи только что созданой платформы
        activeTiles.Add(nextTile); //запись этой дороги в список
        spawnPos += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);//удаление обькта из масива и списка
        activeTiles.RemoveAt(0);
    }
}
