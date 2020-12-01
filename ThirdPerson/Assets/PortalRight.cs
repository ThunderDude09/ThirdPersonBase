using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRight : MonoBehaviour
{
    Vector3 newPosition;

    public GameObject FoundObject;
    public string RaycastReturn;

    bool floor = true;
    bool wall1 = true;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                transform.position = newPosition;
                RaycastReturn = hit.collider.gameObject.name;
                if (floor == true)
                {
                    if (hit.collider.gameObject.name == "floor")
                    {
                        transform.rotation = Quaternion.Euler(90, 0, 0);
                        floor = false;
                        wall1 = true;
                    }
                }
                if (wall1 == true)
                {
                    if (hit.collider.gameObject.name == "Wall1")
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        wall1 = false;
                        floor = true;
                    }
                }
                
                //FoundObject = GameObject.Find(RaycastReturn);
                //transform.forward = new Vector3(FoundObject.transform.forward.x, transform.forward.y, FoundObject.transform.forward.z);
            }
        }
    }
}
