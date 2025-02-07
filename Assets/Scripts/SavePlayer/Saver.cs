using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Saver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int pointRespawn = 0;

    private Save_Point[] savePoints;

    [SerializeField]
    private GameObject[] waves;

    [SerializeField]
    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        savePoints = GetComponentsInChildren<Save_Point>();
    }

    private void OnEnable()
    {
        GlobalEventManager.SavePoint += SaveMap;
    }

    private void OnDisable()
    {
        GlobalEventManager.SavePoint -= SaveMap;
    }

    private void SaveMap(Save_Point point)
    {
        for (int i = 0; i < savePoints.Length; i++)
        {
            if (savePoints[i] != null && savePoints[i] == point)
            {
                if (pointRespawn < i)
                {
                    pointRespawn = i;
                    GlobalEventManager.TriggerNotiSaveMap(savePoints[i].map);
                }
                return;
            }
        }
    }

    void Start()
    {
        Save_Point script = savePoints[pointRespawn];
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        UpdateSavePoints();
        player.transform.position = script.GetPointRespawn();
        GlobalEventManager.TriggerOnChangeMap(script.map);
        confiner.gameObject.SetActive(false);
        foreach (var wave in waves)
        {
            wave.SetActive(false);
        }
        var mapTo = waves[(int)script.map];
        mapTo.SetActive(true);
        confiner.BoundingShape2D = mapTo.GetComponent<Collider2D>();
        confiner.InvalidateBoundingShapeCache();
        StartCoroutine(FixBugCamera());
    }

    IEnumerator FixBugCamera()
    {
        yield return new WaitForSeconds(0.1f);
        confiner.gameObject.SetActive(true);
    }

    private void UpdateSavePoints()
    {
        for (int i = 0; i <= pointRespawn; i++)
        {
            savePoints[i].Passed();
        }

        for (int i = pointRespawn + 1; i < savePoints.Length; i++)
        {
            savePoints[i].InActive();
        }
    }
}
