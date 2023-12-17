using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject ballPrefab;
    public float power;

    public GameObject ShotPoint;
    public GameObject TargetPoint;
    public GameObject TargetPointWrong;

    public bool IsCorrect=false;

    public float height = 3f; // Adjust height of shot curve

    void Start()
    {
        // ShootFromPosition(ShotPoint.transform.position);
    }

    public void ShootFromPosition(Vector3 shootPosition)
    {
        GameObject ball = Instantiate(ballPrefab, shootPosition, Quaternion.identity);
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();

        if(IsCorrect){
            StartCoroutine(MoveToTarget(ballRb, ball.transform, shootPosition, TargetPoint.transform.position));

        }else{
            StartCoroutine(MoveToTarget(ballRb, ball.transform, shootPosition, TargetPointWrong.transform.position));
        }


        
    }

    IEnumerator MoveToTarget(Rigidbody rb, Transform ballTransform, Vector3 start, Vector3 end)
    {
        float time = 0f;
        float duration = Vector3.Distance(start, end) / power; // Adjust speed of shot curve

        while (time < duration)
        {
            Vector3 arcPoint = Vector3.Lerp(start, end, time / duration) + Vector3.up * Mathf.Sin(Mathf.PI * time / duration) * height;
            Vector3 direction = arcPoint - ballTransform.position;
            rb.velocity = direction.normalized * power;
            time += Time.deltaTime;

            yield return null;
        }

        rb.velocity = Vector3.zero;
    }

    public void Shoot()
    {
        ShootFromPosition(ShotPoint.transform.position);
    }
}
