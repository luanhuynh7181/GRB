using Unity.Cinemachine;
using UnityEngine;

public class Boss_Entrance_Transition : MonoBehaviour
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public CinemachineCamera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    [SerializeField]
    private GameObject boss;

    private bool isTriggered = false;
    private float offset = 0;
    private float sizeTo = 10;

    [SerializeField]
    private float time = 2;

    private void Awake()
    {
        boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        GetComponent<BoxCollider2D>().enabled = false;
        isTriggered = true;
        offset = (sizeTo - camera.Lens.OrthographicSize) / time;
    }

    private void Update()
    {
        if (!isTriggered)
            return;
        camera.Lens.OrthographicSize += offset * Time.deltaTime;
        if (camera.Lens.OrthographicSize >= sizeTo)
        {
            camera.Lens.OrthographicSize = sizeTo;
            isTriggered = false;
            Destroy(this);
            boss.SetActive(true);
        }
    }
}
