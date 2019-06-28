// 1st 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    //Vector2 RPrevPos;

    float timer;
    List<Vector2> arr;

    int idx;
    public float speed;
    public float lastTime;
    public Vector3 velocity = Vector3.zero;
    Vector3 direction;
    
    // Use this for initialization
    void Start()
    {
        idx = 0;
        speed = 0;
        Input.location.Start(0.5f);
        Input.compass.enabled = true;
        timer = 0;
        lastTime = 0;
        arr = new List<Vector2>();
        /*arr.Add(new Vector2(30.094706f, 31.341033f));
        arr.Add(new Vector2(30.095203f, 31.341893f));
        arr.Add(new Vector2(30.096214f, 31.343709f));
        arr.Add(new Vector2(30.096786f, 31.344733f));
        arr.Add(new Vector2(30.097578f, 31.345933f));
        arr.Add(new Vector2(30.098107f, 31.346758f));
        arr.Add(new Vector2(30.098738f, 31.347927f));
        arr.Add(new Vector2(30.099815f, 31.349778f));
        arr.Add(new Vector2(30.100752f, 31.351505f));
        arr.Add(new Vector2(30.1009388f, 31.3513468f));
        arr.Add(new Vector2(30.1012799f, 31.3519369f));
        arr.Add(new Vector2(30.1022475f, 31.3535811f));
        arr.Add(new Vector2(30.1026629f, 31.354383f));
        arr.Add(new Vector2(30.1034518f, 31.3556329f));
        arr.Add(new Vector2(30.1038495f, 31.3565556f));*/
        arr.Add(new Vector2(30.1045636f, 31.3582012f));
        arr.Add(new Vector2(30.1048374f, 31.3585767f));
        arr.Add(new Vector2(30.1051007f, 31.3590635f));
        arr.Add(new Vector2(30.1052643f, 31.359372f));
        arr.Add(new Vector2(30.1049391f, 31.3594488f));
        arr.Add(new Vector2(30.1049391f, 31.3594488f));
        arr.Add(new Vector2(30.1049391f, 31.3594488f));

        /* for (int i = 0; i < 8; i++)
         {
             Debug.Log(curval);
             arr.Add(new Vector2(curval + Random.Range(0f, 0.0000024953f) * 100000, curval));

             curval += 0.0000024953f * 100000;
         }*/
    }

    IEnumerator UpdatePosition()
    {
        if (Input.location.isEnabledByUser == false)
        {
            Debug.Log("user did not enable location");
        }
        int maxwait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxwait > 0)
        {
            yield return new WaitForSeconds(1);
            maxwait--;
        }
        if (maxwait < 1)
        {
            Debug.Log("initializing failed , try again");

            yield return null;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("location service status failed !");
            yield return null;
        }
        else
        {
            //cur_lon = Input.location.lastData.longitude;
            //cur_lat = Input.location.lastData.latitude;


        }
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            func();
        }
        timer += Time.deltaTime;
        transform.position += velocity;

    }
    // Update is called once per frame

    public void func()
    {

        if (idx + 1 < arr.Count)
        {
            idx++;
            Vector2 mynew = arr[idx];
            Vector2 myold = arr[idx - 1];

            //transform.position = new Vector3(mynew.x, 3f, mynew.y);
            direction = new Vector3(mynew.x - myold.x, 0f, mynew.y - myold.y) * 1000;
            lastTime = timer;
            velocity = GetSpeed(myold.y, myold.x, mynew.y, mynew.x) * direction.normalized;
            timer = 0;
            // transform.position = Vector3.SmoothDamp(transform.position, newpos, ref velocity, 0.3f);
        }
    }
    float GetSpeed(float firstLon, float firstLat, float secondLon, float secondLat)
    {
        float lastDistance;
        float dlon = Radians(secondLon - firstLon);
        float dlat = Radians(secondLat - firstLat);

        float distance = Mathf.Pow(Mathf.Sin(dlat / 2), 2) + Mathf.Cos(Radians(firstLat)) * Mathf.Cos(Radians(secondLat)) * Mathf.Pow(Mathf.Sin(dlon / 2), 2);

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(distance), Mathf.Sqrt(1 - distance));

        lastDistance = 6371 * c ;
        speed = lastDistance / lastTime * 3.6f;
        Debug.Log("Last Distance: " + lastDistance + ", LastTime: " + lastTime);
        return speed;

    }
    float Radians(float x)
    {
        return x * Mathf.PI / 180;
    }
}
