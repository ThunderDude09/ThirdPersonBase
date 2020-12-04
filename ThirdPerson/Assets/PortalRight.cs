using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRight : MonoBehaviour
{
    Vector3 newPosition;

    public GameObject FoundObject;
    public string RaycastReturn;

    bool floor = true;
    bool roof = true;
    bool wall1 = true;
    bool wall2 = true;
    bool wall3 = true;
    bool wall4 = true;

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
                        roof = true;
                        wall1 = true;
                        wall2 = true;
                        wall3 = true;
                        wall4 = true;
                    }
                }
                if (wall1 == true)
                {
                    if (hit.collider.gameObject.name == "Wall1")
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        wall1 = false;
                        wall2 = true;
                        wall3 = true;
                        wall4 = true;
                        floor = true;
                        roof = true;

                    }
                }
                if (wall2 == true)
                {
                    if (hit.collider.gameObject.name == "Wall2")
                    {
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                        wall2 = false;
                        wall3 = true;
                        wall4 = true;
                        floor = true;
                        roof = true;
                        wall1 = true;
                    }
                }
                if (wall3 == true)
                {
                    if (hit.collider.gameObject.name == "Wall3")
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        wall3 = false;
                        wall4 = true;
                        floor = true;
                        roof = true;
                        wall1 = true;
                        wall2 = true;
                    }
                }
                if (wall4 == true)
                {
                    if (hit.collider.gameObject.name == "Wall4")
                    {
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                        wall4 = false;
                        floor = true;
                        roof = true;
                        wall1 = true;
                        wall2 = true;
                        wall3 = true;
                    }
                }
                if (roof == true)
                {
                    if (hit.collider.gameObject.name == "roof")
                    {
                        transform.rotation = Quaternion.Euler(-90, 0, 0);
                        roof = false;
                        floor = true;
                        wall1 = true;
                        wall2 = true;
                        wall3 = true;
                        wall4 = true;
                    }
                }
                //FoundObject = GameObject.Find(RaycastReturn);
                //transform.forward = new Vector3(FoundObject.transform.forward.x, transform.forward.y, FoundObject.transform.forward.z);
            }
        }
    }
}
