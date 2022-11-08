using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    public float turretRotationSpeed = 150;

    public void Aim(Vector2 inputPoinerPosition){
        var turretDir = (Vector3)inputPoinerPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDir.y, turretDir.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), rotationStep);
    }
}
