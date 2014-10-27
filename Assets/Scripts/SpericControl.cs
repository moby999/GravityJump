using UnityEngine;
using System.Collections;

public class SpericControl : MonoBehaviour {

    public GameObject onSurface;
    public bool isJump = false;

    public float radius = 0.6f;
    public float translateSpeed = 180.0f;
    public float rotateSpeed = 360.0f;
    public float fireInterval = 0.05f;

    float angle = 0.0f;
    Vector3 direction = Vector3.one;
    Quaternion rotation = Quaternion.identity;


    void Update()
    {
        if (onSurface != null)
        {
            Debug.Log("Onground");
            direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));


            // Rotate with left/right arrows
            if (Input.GetKey(KeyCode.LeftArrow)) Rotate(rotateSpeed);
            if (Input.GetKey(KeyCode.RightArrow)) Rotate(-rotateSpeed);

            // Translate forward/backward with up/down arrows
            if (Input.GetKey(KeyCode.UpArrow)) Translate(0, translateSpeed);
            if (Input.GetKey(KeyCode.DownArrow)) Translate(0, -translateSpeed);

            // Translate left/right with A/D. Bad keys but quick test.
            if (Input.GetKey(KeyCode.A)) Translate(translateSpeed, 0);
            if (Input.GetKey(KeyCode.D)) Translate(-translateSpeed, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(transform.up * 100, ForceMode.Impulse);
                isJump = true;
                //StartCoroutine(waitFoJump(1));
            }
            
            
                UpdatePositionRotation();
        }
        else
        {
            //Debug.Log("InAir");
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.rigidbody.AddForce(this.transform.forward * 10, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Rotate(-10 * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Rotate(0, -10 * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Rotate(0, 10 * Time.deltaTime, 0);
            }
        }
    }

    void Rotate(float amount)
    {
        angle += amount * Mathf.Deg2Rad * Time.deltaTime;
    }

    void Translate(float x, float y)
    {
        var perpendicular = new Vector3(-direction.y, direction.x);
        var verticalRotation = Quaternion.AngleAxis(y * Time.deltaTime, perpendicular);
        var horizontalRotation = Quaternion.AngleAxis(x * Time.deltaTime, direction);
        rotation *= horizontalRotation * verticalRotation;
    }

    void UpdatePositionRotation()
    {
        radius = Vector3.Distance(onSurface.transform.position, transform.position);

        transform.localPosition = rotation * Vector3.forward * radius;
        transform.rotation = rotation * Quaternion.LookRotation(direction, Vector3.forward);
    }

    IEnumerator waitFoJump(float sec)
    {
        yield return new WaitForSeconds(sec);
        isJump = false;
    }
}
