using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Phyllotaxic : MonoBehaviour
{
    List<GameObject> spheres = new List<GameObject>();
    public float degreeOffset = 1300f;
    public float distance = 200f;
    double randVal = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        spheres.Add(gameObject);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = CalculatePhyllotaxisPosition(spheres.Count);
        sphere.tag = "sphere";
        spheres.Add(sphere);
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //increase the num of spheres
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = CalculatePhyllotaxisPosition(spheres.Count);
            sphere.tag = "sphere";
            spheres.Add(sphere);
        }
        //decrease the num of spheres
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(spheres.Count > 1)
            {
                var destroySphere = spheres.Last();
                spheres.Remove(destroySphere);
                Destroy(destroySphere);
            }
        }
    }

    //this gets the position based on phylotaxis
    Vector3 CalculatePhyllotaxisPosition(int n)
    {
        //float r = distance * Mathf.Sqrt(n) +
        float r = (spheres.Last().transform.position.magnitude - spheres.First().transform.position.magnitude) + .5f;
        float theta = n * degreeOffset * Mathf.Deg2Rad + (float)randVal;
        float x = r * Mathf.Cos(theta);
        float z = r * Mathf.Sin(theta);
        float y = 0;
        return new Vector3(x, y, z);
    }

    public void Randomize()
    {    
        System.Random rand = new System.Random();
        double maxVal = Math.PI / 12.0;
        randVal = rand.NextDouble() * maxVal;
    }

    public void Reset()
    {
        randVal = 0;
        spheres.RemoveRange(1, spheres.Count-1);
        foreach (var sphere in GameObject.FindGameObjectsWithTag("sphere"))
            Destroy(sphere);
        
    }
}
