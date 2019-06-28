using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawning : MonoBehaviour
{
    public GameObject prefabs;
    public GameObject generate;
    Vector3 myvec;
    // Use this for initialization
    void Start()
    {
        myvec = transform.position;
        instantiategameobjects();

    }

    // Update is called once per frame
    void Update()
    {
        //myvec = transform.position;

    }
    void instantiategameobjects()
    {
        if (transform.position.z != myvec.z )
        {
            myvec = transform.position;
            StartCoroutine("wait1sec");
            return;
        }
       
        generate = Instantiate(prefabs, transform.position + (transform.forward * 5), transform.rotation);
        // generate.transform.SetParent( GameObject.FindGameObjectWithTag("floor").transform );
        //generate.transform.parent = transform;
        myvec = transform.position;
        Destroy(generate, 10f);
        StartCoroutine("wait1sec");
    }
    public IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(1f);
        instantiategameobjects();


    }
}
