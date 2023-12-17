using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLogic : MonoBehaviour
{
    public GameObject endTarget;
    public GameObject Point1;
    public GameObject Point2;

    public float changetime;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(changetime);
            SetRandomEndTargetPosition();
        }
    }

    public void SetRandomEndTargetPosition()
    {
        if (endTarget != null && Point1 != null && Point2 != null)
        {
            Vector3 randomPosition = Vector3.Lerp(Point1.transform.position, Point2.transform.position, Random.value);
            endTarget.transform.position = randomPosition;
        }
        else
        {
            Debug.LogWarning("Ensure endTarget, Point1, and Point2 are assigned!");
        }
    }
}
