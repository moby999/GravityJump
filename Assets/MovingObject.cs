using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour
{

    public GameObject surface;
    public float flyForce = 50f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (surface != null)
        {
            Vector3 thisPos = this.transform.position;
            Vector3 planetPos = surface.transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, (thisPos - planetPos).normalized);
            rigidbody.AddForce(transform.forward.normalized * Time.deltaTime * flyForce, ForceMode.Force);
            Debug.DrawRay(transform.position, transform.forward.normalized * Time.deltaTime * flyForce);
        }
    }
}
