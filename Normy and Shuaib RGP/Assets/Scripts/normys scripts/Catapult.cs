using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    //reference to the arm of the catapult
    [SerializeField] private GameObject catapultArm;
    //reference to the projectile prefab
    [SerializeField] public GameObject projectile;

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

    void Update()
    {
        ////if the user has pulled back the mouse
        //if (Input.mousePosition.y < mouseStartY)
        //{
        //    //if the arm of the catapult is greater than or equal to the maxium angle
        //    if (catapultArm.transform.rotation.y >= maxAngle)
        //    {
        //        //if the catapult is not currently unloading a projectile
        //        if (!unloading)
        //        {
        //            //find the difference in y axis from the start position to the new mouse position
        //            var difference = Input.mousePosition.y - mouseStartY;
        //            //rotate the arm of the catapult by the difference in the x axis of rotation
        //            catapultArm.transform.Rotate(new Vector3(1, 0, 0), difference * Mathf.Abs(difference) / windingSpeed);
        //        }
        //    }
        //}
        ////assign the new vertical axis value to the mouseStartY 
        //mouseStartY = Input.mousePosition.y;

        if (Input.GetAxis("Mouse Y") < 0)
        {
            if (catapultArm.transform.rotation.y >= maxAngle)
            {
                float diff = Input.GetAxis("Mouse Y");
                catapultArm.transform.Rotate(new Vector3(1, 0, 0), diff);
                //tension += 24 * diff / windingSpeed;
            }
        }

        //if the user presses the spacebar button
        if (Input.GetMouseButtonDown(2))
        {
            //set the unloading to true
            unloading = true;
            //evauluate the speed of unwinding
            unwindSpeed = minAngle / Mathf.Pow((catapultArm.transform.rotation.y / 2), 2);
        }

        //if the catapult arm is unloading
        if (unloading)
        {
            //rotate the catapult arm by the unwind speed and unwind coefficiant along the x axis
            catapultArm.transform.Rotate(new Vector3(1, 0, 0), unwindSpeed * unwindCoefficient * Time.deltaTime);
        }

        //if the catapults arm exceeds the minimum angle
        if (catapultArm.transform.rotation.y > minAngle)
        {
            //set unloading to false
            unloading = false;
            //the catapult can fire again
            canFire = true;
        }

        //if the catapult can fire and the projectil is not null
        if (canFire && projectile != null)
        {
            //launch the projectile
            LaunchObj();
            projectile = null;
        }
    }

    //logic for launching a projectile
    void LaunchObj()
    {
        //min wind = 0.96, max wind = 0.79

        //get the rigidbody component from the projectile
        Rigidbody projRB = projectile.GetComponent<Rigidbody>();
        projectile.transform.SetParent(null);
        projRB.isKinematic = false;
        projRB.useGravity = true;
        projRB.AddRelativeForce(Vector3.forward * unwindSpeed * unwindCoefficient * 40);
        projectile.GetComponent<DespawnTimer>().StartTimer();
    }

   
}
