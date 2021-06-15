using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject catapultArm;
    public GameObject projectile;

    public float windingSpeed;
    public float unwindCoefficient;

    float tension;
    //float mouseStartY;
    float unwindSpeed;

    float minAngle;
    float maxAngle;

    bool unloading;
    bool canFire = false;
    // Start is called before the first frame update
    void Start()
    {
       // mouseStartY = Input.mousePosition.y;
        minAngle = 0.97f;
        maxAngle = 0.79f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.mousePosition.y < mouseStartY && catapultArm.transform.rotation.y >= maxAngle && !unloading)
        //{
        //    float diff = Mathf.Abs(Input.mousePosition.y - mouseStartY);
        //    catapultArm.transform.Rotate(new Vector3(1, 0, 0), (Input.mousePosition.y - mouseStartY) * diff / windingSpeed);
        //    //tension += 24 * diff / windingSpeed;
        //}
        //mouseStartY = Input.mousePosition.y;
        if (Input.GetAxis("MouseVerticalAxis") < 0)
        {
            float diff = Input.GetAxis("MouseVerticalAxis");
            catapultArm.transform.Rotate(new Vector3(1, 0, 0), diff);
            tension += 24 * diff / windingSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            unloading = true;
            unwindSpeed = minAngle / Mathf.Pow((catapultArm.transform.rotation.y / 2), 2);
        }

        if (unloading)
        {
            catapultArm.transform.Rotate(new Vector3(1, 0, 0), unwindSpeed * unwindCoefficient * Time.deltaTime);
        }

        if (catapultArm.transform.rotation.y > minAngle)
        {
            unloading = false;
            canFire = true;
        }

        if (canFire && projectile != null)
        {
            LaunchObj();
            projectile = null;
        }
    }

    void LaunchObj()
    {
        //min wind = 0.96, max wind = 0.79
        Rigidbody projRB = projectile.GetComponent<Rigidbody>();
        projectile.transform.SetParent(null);
        projRB.isKinematic = false;
        projRB.useGravity = true;
        projRB.AddRelativeForce(Vector3.forward * unwindSpeed * unwindCoefficient * 2);
        projectile.GetComponent<despawnTimer>().StartTimer();
    }
}
