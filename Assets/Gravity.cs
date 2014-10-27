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
            //Debug.Log(planetRadius + obj.GetComponent<MeshRenderer>().bounds.size.y + ":" + Vector3.Distance(thisPos, objPos));

            Vector3 offset = (transform.position - obj.transform.position).normalized;
            //obj.rigidbody.AddForce(offset / offset.sqrMagnitude * rigidbody.mass);
            obj.rigidbody.velocity += forceStrength * Time.deltaTime * offset;

            //obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, (objPos - thisPos).normalized);
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
                controller.onSurface = this.gameObject;
                //controller.onSurface = this.gameObject;

                //obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, (objPos - thisPos).normalized);

                //sc.radius = planetRadius;
                //obj.transform.parent = transform;

                /*
                //new stuff
                ConfigurableJoint joint = obj.GetComponent<ConfigurableJoint>();
                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;
                Vector3 toCenter = -transform.localPosition;

                // center position relative to object 
                joint.connectedAnchor = this.transform.position ;
                //joint.anchor = transform.InverseTransformDirection(toCenter);

                // map velocity to sphere surface 
                //rigidbody.velocity = Quaternion.LookRotation(toCenter) * Vector3.one; // *startVel; 
                 */
                
            }
            //obj.rigidbody.velocity = Vector3.zero;
        }
        //Debug.Log(obj.rigidbody.velocity);
            
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
