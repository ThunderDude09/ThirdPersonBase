﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRight : MonoBehaviour
{
    Vector3 newPosition;

    public GameObject FoundObject;
    public string RaycastReturn;

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
                FoundObject = GameObject.Find(RaycastReturn);
                transform.forward = new Vector3(FoundObject.transform.forward.x, transform.forward.y, FoundObject.transform.forward.z);
            }
        }
    }
}
