using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Tile
    //public int X;
    //public int Y;

    //public Fruit Fruit;

    //public bool DestroyFlag;

    //public void CheckAndDestroyFruit() {
    //    if (DestroyFlag) {
    //        Fruit.Die();
    //        Fruit = null;
    //        DestroyFlag = false;
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    FindObjectOfType<PlayerInput>().OnClick(this);
    //}
    //#endregion

    #endregion

    public int X;
    public int Y;

    PlayerInput playerInput;
    public Fruit Fruit;

    public bool DestroyFlag;

    private void Start()
    {
        playerInput = PlayerInput.instance;
    }

    public void DestroyFruit()
    {
        if (DestroyFlag)
        {
            Fruit.Die();
            Fruit = null;
            DestroyFlag = false;
        }
    }

    private void OnMouseDown()
    {
        if (playerInput.isDoneChecking)
        {
            playerInput.ChangeFruitsPositions(this);
        }       
    }
}

