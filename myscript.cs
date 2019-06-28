using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myscript : MonoBehaviour {
    float timer;
    public float speed;
    public float lastTime , longitude , latitude , cur_long , cur_lat;
    public Vector3 velocity = Vector3.zero;
    Vector3 direction;
    bool flag;
    //public Text uiText;
    void Awake()
    {
        flag = false;
        timer = 0;
        lastTime = 0;
       
    }
    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("user has not enabled gps");
            yield break;
        }
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;

        }
        if (maxWait <= 0)
        {
            print("Timed out");
            yield break;

        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        flag = true;
        latitude = Input.location.lastData.latitude*1000f;
        longitude = Input.location.lastData.longitude*1000f;
        yield break;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(StartLocationService());
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.position += (velocity/100);
        float cur_long = Input.location.lastData.longitude*1000f;
        float cur_lat = Input.location.lastData.latitude*1000f;

        //uiText.text = "Lon: " + cur_long + "\nLat: " + cur_lat;

        if (flag &&(longitude !=cur_long  || latitude != cur_lat))
        {
            Vector2 mynew = new Vector2 (cur_lat , cur_long);
            Vector2 myold = new Vector2(latitude , longitude);
            direction = new Vector3(mynew.x - myold.x, 0f, mynew.y - myold.y);
            lastTime = timer;
            velocity = GetSpeed(myold.y, myold.x, mynew.y, mynew.x) * direction.normalized;
            timer = 0;
        }
        latitude = cur_lat;
        longitude = cur_long;

    }
    float GetSpeed(float firstLon, float firstLat, float secondLon, float secondLat)
    {
        float lastDistance;
        float dlon = Radians(secondLon - firstLon);
        float dlat = Radians(secondLat - firstLat);

        float distance = Mathf.Pow(Mathf.Sin(dlat / 2), 2) + Mathf.Cos(Radians(firstLat)) * Mathf.Cos(Radians(secondLat)) * Mathf.Pow(Mathf.Sin(dlon / 2), 2);

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(distance), Mathf.Sqrt(1 - distance));

        lastDistance = 6371 * c;
        speed = lastDistance / lastTime * 3.6f;
        return speed;

    }
    float Radians(float x)
    {
        return x * Mathf.PI / 180;
    }
}
