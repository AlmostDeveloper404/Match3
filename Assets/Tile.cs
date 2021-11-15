using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int X;
    public int Y;

    public Fruit Fruit;

    public bool DestroyFlag;

    public void CheckAndDestroyFruit() {
        if (DestroyFlag) {
            Fruit.Die();
            Fruit = null;
            DestroyFlag = false;
        }
    }

    private void OnMouseDown()
    {
        FindObjectOfType<PlayerInput>().OnClick(this);
    }

}

