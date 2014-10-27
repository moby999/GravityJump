using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{

    public float forceStrength = 50;
    private Vector3 startVel;

    // Use this for initialization
    void Start()
    {
        forceStrength = forceStrength * rigidbody.mass;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider obj)
    {
        float step = forceStrength * Time.deltaTime;
        //obj.transform.position = Vector3.MoveTowards(obj.transform.position, this.transform.position, step);
        Vector3 thisPos = this.transform.position;
        Vector3 objPos = obj.transform.position;
        float planetRadius = this.transform.FindChild("Sphere").GetComponent<MeshRenderer>().bounds.size.x / 2;

        if (Vector3.Distance(thisPos, objPos) > planetRadius /*+ obj.GetComponent<MeshRenderer>().bounds.size.y*/)
        {
            Vector3 offset = (transform.position - obj.transform.position).normalized;
            //obj.rigidbody.AddForce(offset / offset.sqrMagnitude * rigidbody.mass);
            obj.rigidbody.velocity += forceStrength * Time.deltaTime * offset;
        }
        else
        {
            Vector3 offset = transform.position - obj.transform.position;
            obj.rigidbody.velocity = Vector3.zero;
            obj.rigidbody.velocity += forceStrength * Time.deltaTime * offset;
            if (obj.GetComponent<Move>() != null)
            {
                //Debug.Log("Landed");
                Move controller = obj.GetComponent<Move>();
                if (controller.onSurface == null)
                    controller.onSurface = this.gameObject;
            }
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        Vector3 thisPos = this.transform.position;
        Vector3 objPos = obj.transform.position;
        obj.rigidbody.drag = 0.05f;
        Debug.Log("enter1");
        if (obj.GetComponent<Move>() != null)
        {
            //obj.GetComponent<Move>().onSurface = this.gameObject;
            //obj.transform.parent = transform;
        }
        if (obj.GetComponent<MovingObject>() != null)
        {
            obj.GetComponent<MovingObject>().surface = this.gameObject;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        obj.rigidbody.drag = 0.01f;
        obj.rigidbody.velocity = obj.rigidbody.velocity * 0.1f;
        if (obj.GetComponent<Move>() != null)
        {
            obj.GetComponent<Move>().onSurface = null;
            //obj.transform.parent = null;
        }
        if (obj.GetComponent<MovingObject>() != null)
        {
            obj.GetComponent<MovingObject>().surface = null;
        }
    }
}
