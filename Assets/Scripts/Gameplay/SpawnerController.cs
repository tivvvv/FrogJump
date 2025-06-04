using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    // 生成器朝向
    public int direction;

    public List<GameObject> spawnObjects;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.5f, Random.Range(5f, 10f));
    }

    private void Spawn()
    {
        int index = Random.Range(0, spawnObjects.Count);
        GameObject car = Instantiate(spawnObjects[index], transform.position, Quaternion.identity, transform);
        // 设置汽车朝向
        car.GetComponent<MoveForwardController>().dir = direction;
    }

}
