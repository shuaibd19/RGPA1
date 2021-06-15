using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    //reference to the arm of the catapult
    [SerializeField] private GameObject catapultArm;
    //reference to the projectile prefab
    [SerializeField] private GameObject projectile;

    //used to control the speed at which the catapult is wound
    [SerializeField] private float windingSpeed;
    //used to increase/decrease unwinding motion
    [SerializeField] private float unwindCoefficient;

    //tensile force
    float tension;
    //starting poistion of the mouse on the vertical axis
    float mouseStartY;
    //speed of unwinding 
    float unwindSpeed;

    //minimum angle that the catapult arm can reach
    float minAngle;
    //maximum angle that the catapult arm can reach
    float maxAngle;

    //catapult firing logic
    bool unloading;
    bool canFire = false;
    // Start is called before the first frame update
    void Start()
    {
        //setting the start poisiton of the mouse vertical axis
        mouseStartY = Input.mousePosition.y;
        //setting up the minimum and maximum angles
        minAngle = 0.97f;
        maxAngle = 0.79f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.y < mouseStartY && catapultArm.transform.rotation.y >= maxAngle && !unloading)
        {
            float diff = Mathf.Abs(Input.mousePosition.y - mouseStartY);
            catapultArm.transform.Rotate(new Vector3(1, 0, 0), (Input.mousePosition.y - mouseStartY) * diff / windingSpeed);
            //tension += 24 * diff / windingSpeed;
        }
        mouseStartY = Input.mousePosition.y;

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
        projectile.GetComponent<DespawnTimer>().StartTimer();
    }
}
