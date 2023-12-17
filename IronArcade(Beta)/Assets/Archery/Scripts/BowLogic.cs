using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowLogic : MonoBehaviour
{
    [SerializeField]
    private float RotSpeed;

    public GameObject ArrowPrefab;
    public GameObject EndTarget;
    public GameObject EndTargetWrong;
    public GameObject BowBody; // Reference for bow to rotate towards target

    public bool IsCorrect = false;

    [SerializeField]
    private float ShootPower;

    Vector2 targetDir;

    void Update()
    {
        if (IsCorrect)
        {
            targetDir = EndTarget.transform.position - transform.position;

        }
        else
        {
            targetDir = EndTargetWrong.transform.position - transform.position;

        }


        if (EndTarget != null)
            RotateBowTowardsTarget();


    }

    void RotateBowTowardsTarget()
    {

        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.AngleAxis(angle - 180f, Vector3.forward); // Adjusts angle as needed

        BowBody.transform.rotation = Quaternion.Slerp(BowBody.transform.rotation, rot, Time.deltaTime * RotSpeed);
    }

    public void Shoot()
    {
        GameObject GO = Instantiate(ArrowPrefab, GameObject.Find("Arrow").transform.position, BowBody.transform.rotation);
        GO.AddComponent(typeof(Rigidbody2D));
        GO.GetComponent<Rigidbody2D>().gravityScale = 0f;
        if (IsCorrect)
        {
            GO.GetComponent<Rigidbody2D>().velocity = (targetDir).normalized * ShootPower;

        }
        else
        {
            GO.GetComponent<Rigidbody2D>().velocity = (targetDir).normalized * ShootPower;
        }

    }
}
