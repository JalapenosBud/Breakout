using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour{

    public BrickTypes brickTypes;

    public void SetBrick(UnityEngine.GameObject go, BrickTypes _brickTypes)
    {
        this.brickTypes = _brickTypes;
        UtilityThings.ChooseColor(go, _brickTypes);
    }

}
