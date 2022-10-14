using UnityEngine;

public class BonusItem : MonoBehaviour
{
    [SerializeField] private Transform[] SpawnPoints;
    private int index;
    void Start()
    {
        RandomPoint();
        Invoke("BonusDisappear", 7f);
    }

    void RandomPoint()
    {
        index = Random.Range(0, 5);
        transform.position = SpawnPoints[index].position;
    }
    void BonusDisappear()
    {
        this.gameObject.SetActive(false);
    }
}