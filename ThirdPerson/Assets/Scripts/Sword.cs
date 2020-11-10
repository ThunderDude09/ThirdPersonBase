using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static int moveSpeed = 4;
    public Vector3 userDirection = Vector3.right;
    bool playerHasThrown = false;
    bool playerHasSword = true;

    Rigidbody sword;

    float timeSinceButtonHeld = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        sword = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {

            if (!playerHasSword)
            {

            }
            //Debug.Log("Left Click");
            if (Input.GetMouseButtonUp(0) && playerHasSword == true)
            {
                //Debug.Log("Right Click");
                transform.parent = null;
                //sword.isKinematic = false;
                playerHasThrown = true;
                playerHasSword = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            timeSinceButtonHeld = 0;
        }
        //timeSinceButtonHeld += Time.deltaTime;
        if (playerHasThrown == true)
        {
            transform.Translate(1 * Time.deltaTime,0,0);
        }
    }

    /*void throwSword()
    {
        transform.Translate(userDirection * moveSpeed * Time.deltaTime);
        //transform.Translate(1, 0, 0);
        //sword.AddForce(Vector3.forward * 500);
        //sword.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
    }*/

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Environment")
        {
            Debug.Log("triggered");
            transform.Translate(0, 0, 0);
            playerHasThrown = false;
        }
    }
}
