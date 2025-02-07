using System.Collections.Generic;
using GameConstants;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject barrelBoomPrefab;

    [SerializeField]
    private GameObject splashObject;

    [SerializeField]
    private GameObject dynamite;

    private Dictionary<POOL_TYPE, GameObject> gameObjects = new Dictionary<POOL_TYPE, GameObject>();

    private Dictionary<POOL_TYPE, Queue<GameObject>> pools =
        new Dictionary<POOL_TYPE, Queue<GameObject>>();

    public static ObjectPoolManager Instance { get; private set; }

    public void AddToPool(POOL_TYPE type, GameObject obj)
    {
        if (!pools.ContainsKey(type))
        {
            Debug.LogError($"Pool for POOL_TYPE '{type}' does not exist.");
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pools[type].Enqueue(obj);
    }

    public GameObject GetFromPool(POOL_TYPE type)
    {
        if (!pools.ContainsKey(type))
        {
            pools.Add(type, new Queue<GameObject>());
        }

        Queue<GameObject> pool = pools[type];

        if (pool.Count > 0)
        {
            GameObject first = pool.Dequeue();
            first.SetActive(true);
            return first;
        }

        GameObject prefab = GetPrefabByType(type);
        if (prefab == null)
        {
            Debug.LogError($"Prefab for POOL_TYPE '{type}' does not exist.");
        }
        GameObject obj = Instantiate(prefab);
        obj.SetActive(true);
        obj.transform.SetParent(transform);
        return obj;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private GameObject GetPrefabByType(POOL_TYPE type)
    {
        if (gameObjects.ContainsKey(type))
        {
            return gameObjects[type];
        }
        switch (type)
        {
            case POOL_TYPE.ARROW:
                gameObjects.Add(type, arrowPrefab);
                break;
            case POOL_TYPE.BARREL_BOOM:
                gameObjects.Add(type, barrelBoomPrefab);
                break;
            case POOL_TYPE.SPLASH:
                gameObjects.Add(type, splashObject);
                break;
            case POOL_TYPE.DYNAMITE:
                gameObjects.Add(type, dynamite);
                break;
        }
        return gameObjects[type];
    }
}
