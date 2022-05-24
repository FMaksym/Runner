using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] private Transform player;

    public GameObject[] tilePrefabs;//����� � �������

    private List<GameObject> activeTiles = new List<GameObject>();//������ � ��������� �������
    private float spawnPos = -5;
    private float tileLength = 100;
    private int startTiles = 10; //����������� �������� ������������ ������

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
        if (player.position.z - 30 > spawnPos - (startTiles * tileLength)) //���� ��������� �� z ������ ��� ������� ��������� ������� � ����� ������ ��
        {                                                                  //������� ����� ������ � ������� �� ��� ����� ���
            SpawnPlatforms(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnPlatforms(int tileIndex) {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation); //���������� ��� ������ ������ ��� �������� ���������
        activeTiles.Add(nextTile); //������ ���� ������ � ������
        spawnPos += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);//�������� ������ �� ������ � ������
        activeTiles.RemoveAt(0);
    }
}
