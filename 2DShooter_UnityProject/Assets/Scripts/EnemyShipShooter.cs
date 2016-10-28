using UnityEngine;
using System.Collections;

/*
 * Source: EnemyShipShooter.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Defines the EnemyShipShooter enemy behavior
 * Revision History:
 */

public class EnemyShipShooter : Enemy {
    public float curveMovementHeight = 1f;
    public float shootDelay;
    public Beam beam;

    protected override void Start() {
        base.Start();
        beam.speed *= -1;
        StartCoroutine("ShootRoutine");
    }

    public override void Move() {
        rb2d.velocity = new Vector2(-speed, curveMovementHeight * Mathf.Cos(Time.time));
    }

    /// <summary>
    /// Shoots a beam each shootDelay times
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootRoutine() {
        while(true) {
            yield return new WaitForSeconds(shootDelay);
            GameObject beamInstance = Instantiate(beam.gameObject);
            beamInstance.transform.position = beam.transform.position;
            beamInstance.SetActive(true);
        }        
    }
}
