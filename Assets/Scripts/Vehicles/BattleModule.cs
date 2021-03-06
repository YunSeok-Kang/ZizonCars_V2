﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleModule : MonoBehaviour
{
    //public int rayDist = 10;

    public VehicleControl vehicleControl = null;

    public LayerMask rayingLayer;
    public float rayDist = 1;

    public GameObject rayPoint = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (vehicleControl.vehicleID == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.GetComponent<SkillSystemRoot>().UseSkill();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                gameObject.GetComponent<SkillSystemRoot>().UseSkill();
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit[] hits;
        Vector3 localForward = rayPoint.transform.forward;
        //localForward += Vector3.up;

        Debug.DrawRay(rayPoint.transform.position, localForward, Color.blue, rayDist);
        //Physics.Raycast(transform.position, localForward, rayDist, LayerMask.GetMask("Obstacle"));
        //if (Physics.Raycast(transform.position, localForward, out hit, rayDist, rayingLayer))//LayerMask.GetMask("Player")))
        hits = Physics.RaycastAll(transform.position, localForward, rayDist, rayingLayer);
        //if (Physics.RaycastAll(transform.position, localForward, out hits, rayDist, rayingLayer))
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                //Debug.Log(LayerMask. hit.collider.gameObject.layer);

                // 발사한 레이가 차량 자기 자신의 콜리더에 맞으면 안되기에 처리.
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("VehicleFront"))
                {
                    if (!hit.collider.gameObject.Equals(vehicleControl.vehicleFront))
                    {
                        Debug.Log("정면추돌!");
                        break;
                    }
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Players"))
                {
                    if (!hit.collider.gameObject.Equals(vehicleControl.gameObject))
                    {
                        Debug.Log("추돌!");

                        if (vehicleControl.vehicleID == 0) // 첫번째 차
                        {
                            GameManager.Instance().p1WinCount = 1;
                        }
                        else
                        {
                            GameManager.Instance().p2WinCount = 1;
                        }
                    }
                }
            } 
        }

        //Debug.DrawRay(transform.position, localForward * rayDist, Color.blue, 3f);
        //Physics.Raycast(transform.position, localForward, out hit, 100, 0);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Vector3 destination = transform.position + this.transform.forward * 10;

        

    //    Gizmos.DrawLine(transform.position, destination);
    //}
}
