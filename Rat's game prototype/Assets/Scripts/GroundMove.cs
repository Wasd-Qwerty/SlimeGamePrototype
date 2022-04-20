using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public float speed;
    public float lowerXValue = -8;
    private float destroyXvalue = -30;
    private float upperXValue = 17;
    public GameObject pipe;
    private bool Instant = false;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        pipe.transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, 0,0);
        if (pipe.transform.position.x <= lowerXValue && !Instant)
        {
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = new Vector3(upperXValue, 0, 0);
            Instant = true;
        }
        if (pipe.transform.position.x <= lowerXValue && !Instant)
        {
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = new Vector3(upperXValue, 0, 0);
            Instant = true;
        }
        if (pipe.transform.position.x < destroyXvalue)
        {
            Destroy(pipe);
        }
    }
}
