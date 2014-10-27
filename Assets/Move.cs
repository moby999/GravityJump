using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    public GameObject onSurface;
    public float speedFactor = 2;
    public float jumpFactor = 2;
    public  float rotateSpeed;

    private Quaternion curRotation;

    // Use this for initialization
    void Start()
    {
        rotateSpeed = 5;
        curRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, transform.up, Color.green);
        Vector3 thisPos = this.transform.position;
        Vector3 planetPos;
        Quaternion rotation = new Quaternion();
        if (onSurface != null)
        {
            //Debug.DrawRay(transform.position, Vector3.Cross(onSurface.transform.position, transform.position), Color.yellow);
            //transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(transform.up), Quaternion.LookRotation(onSurface.transform.position - transform.position), 2f);
            planetPos = onSurface.transform.position;
            rotation = Quaternion.FromToRotation(Vector3.up, (thisPos - planetPos).normalized) * curRotation;
        }
        else
        {
            rotation = this.transform.rotation;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (onSurface == null)
            {
                this.rigidbody.AddForce(this.transform.forward * speedFactor, ForceMode.Acceleration);
            }
            else
            {
                Debug.Log("Move on ground!!");
                Vector3 dir = transform.forward.normalized * Time.deltaTime;// Vector3.Cross(onSurface.transform.position, transform.position).normalized; 
                //this.rigidbody.AddForce(dir, ForceMode.Impulse);
                this.transform.position += dir * speedFactor;

            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (onSurface == null)
                rotation *= Quaternion.Euler(rotateSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (onSurface == null)
                rotation *= Quaternion.Euler(-rotateSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (onSurface != null)
                curRotation *= Quaternion.Euler(0, -rotateSpeed, 0);
            else
                rotation *= Quaternion.Euler(0, -rotateSpeed, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (onSurface != null)
                curRotation *= Quaternion.Euler(0, rotateSpeed, 0);
            else
                rotation *= Quaternion.Euler(0, rotateSpeed, 0);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onSurface != null)
                rigidbody.AddForce(transform.up * jumpFactor, ForceMode.Impulse);
        }

        transform.rotation = rotation;
    }
}
