using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    // 生成器朝向
    public int direction;

    public List<GameObject> spawnObjects;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.2f, Random.Range(3.5f, 8f));
    }

    private void Spawn()
    {
        int index = Random.Range(0, spawnObjects.Count);
        GameObject movingObject = Instantiate(spawnObjects[index], transform.position, Quaternion.identity, transform);
        // 设置物体朝向
        movingObject.GetComponent<MoveForwardController>().dir = direction;
    }

}
