using UnityEngine;
using System.Collections;

/*
 * Source: EnemyShip.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Defines the EnemyShip enemy behavior 
 */
 
public class EnemyShip : Enemy {
    public float curveMovementHeight = 1f;

    public override void Move() {
        rb2d.velocity = new Vector2(-speed, curveMovementHeight * Mathf.Cos(Time.time));
    }
}
