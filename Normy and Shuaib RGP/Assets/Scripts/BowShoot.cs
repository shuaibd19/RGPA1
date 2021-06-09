using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// script to control bow shooting arrows
/// </summary>
public class BowShoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform ejectionPoint;
    [SerializeField] private Text forceGuage;

    [SerializeField] private float testForce = 0.3f;

    //[SerializeField] private float shootForce = 60f;

    [SerializeField] private float maxForce = 50f;
    [SerializeField] private float minForce = 0f;

    private float vertical = 0f;

    //the guage meter for the amount of force to exert on arrow based on mouse y axis movement
    float guage = 0f;

    private void Update()
    {
        //make this an absolute value
        //var vertical = Mathf.Abs(Input.GetAxis("Mouse Y") * shootForce);

        //vertical += Mathf.Abs(Input.GetAxis("Mouse Y") * testForce);
        vertical += (Input.GetAxis("Mouse Y") * -1f) * testForce;
        //clamp the result between the maxforce and min force and assign it to the guage
        guage = Mathf.Clamp(vertical, minForce, maxForce);

        forceGuage.text = "Force: " + guage.ToString();

        //Debug.Log(vertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject arr = Instantiate(arrow, ejectionPoint.position, Quaternion.identity);
            Rigidbody rb = arr.GetComponent<Rigidbody>();
            rb.velocity = cam.transform.forward * guage;
            guage = 0f;
        }


    }
}
