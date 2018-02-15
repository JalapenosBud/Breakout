using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * layers:
 * 8 pad
 * 9 ball
 * 10 brick
 */

public class UtilityThings : MonoBehaviour {

    public static Color ChooseColor(GameObject temp, BrickTypes brickTypes)
    {
        if(brickTypes == BrickTypes.GREEN)
        {
            return temp.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (brickTypes == BrickTypes.YELLOW)
        {
            return temp.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (brickTypes == BrickTypes.ORANGE)
        {
            return temp.GetComponent<SpriteRenderer>().color = new Color(252f, 150f, 17f);
        }
        if (brickTypes == BrickTypes.RED)
        {
            return temp.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (brickTypes == BrickTypes.PURPLE)
        {
            return temp.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        if(brickTypes == BrickTypes.BLUE)
        {
            return temp.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        return temp.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public static float GetCollisionAngle(Transform hitObjectTransform, CircleCollider2D collider, Vector2 contactPoint)
    {
        Vector2 collidertWorldPosition = new Vector2(hitObjectTransform.position.x, hitObjectTransform.position.y);
        Vector3 pointB = contactPoint - collidertWorldPosition;

        float theta = Mathf.Atan2(pointB.x, pointB.y);
        float angle = (360 - ((theta * 180) / Mathf.PI)) % 360;
        print(angle);
        return angle;
    }

}
