using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimer : MonoBehaviour
{
    public float timeToDespawn;

    float timer;
    bool countdown = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = timeToDespawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
            timer -= Time.deltaTime;

        if (timer <= 0.0f)
            gameObject.SetActive(false);

    }

    public void StartTimer()
    {
        countdown = true;
        timer = timeToDespawn;
    }
}
