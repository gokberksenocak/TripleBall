using System.Collections.Generic;
using UnityEngine;
public class Opponent : MonoBehaviour
{
    [SerializeField] private Transform[] opponents;
    private List<Transform> locations = new List<Transform>();
    private int location_index;
    private int count_opponent = 1;
    void Start()
    {
        RandomLocation();
    }
    void Update()
    {

    }
    void RandomLocation()
    {
        while (count_opponent < 6)
        {
            location_index = Random.Range(0, 9);
            if (!locations.Contains(opponents[location_index]))
            {
                locations.Add(opponents[location_index]);
                count_opponent++;
            }
            if (locations.Count == 5)
            {
                break;
            }
        }
        locations[0].gameObject.SetActive(true);
        locations[1].gameObject.SetActive(true);
        locations[2].gameObject.SetActive(true);
        locations[3].gameObject.SetActive(true);
        locations[4].gameObject.SetActive(true);
        locations.Clear();
    }
}