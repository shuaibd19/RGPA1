using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject catapultArm;
    public float windingSpeed;

    float tension;

    bool unloading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            catapultArm.transform.Rotate(new Vector3(1, 0, 0), -windingSpeed * Time.deltaTime);
            tension += 10 * Time.deltaTime;
            print(catapultArm.transform.rotation.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            unloading = true;
        }

        //why won't it stop
        if (unloading)
            catapultArm.transform.Rotate(new Vector3(1, 0, 0), tension * Time.deltaTime);

        if (catapultArm.transform.rotation.y >= 0.96)
            //tension = 0; //for some reason this line breaks the whole thing
            unloading = false;
    }
}
