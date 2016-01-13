﻿using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    public Weapon weapon;

    [SerializeField]
    Camera cam;

    [SerializeField]
    private LayerMask mask;

    private void Awake()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera reference");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string _ID)
    {
        Debug.Log(_ID + "has been shot");
        //Destroy(GameObject.Find(_ID));
    }
}
