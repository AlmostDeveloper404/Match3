using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region PlayerInput
    //public Tile TileA;
    //public Tile TileB;

    //public Material MaterialDafault;
    //public Material MaterialSelected;

    //public TileManager TileManager;

    //public void OnClick(Tile tile) {

    //    if (TileA == null) {
    //        TileA = tile;
    //        TileA.GetComponent<Renderer>().material = MaterialSelected;
    //    }
    //    else if (TileB == null)
    //    {
    //        TileB = tile;

    //        Fruit fruitA = TileA.Fruit;
    //        Fruit fruitB = TileB.Fruit;

    //        fruitA.SetTile(TileB);
    //        fruitB.SetTile(TileA);

    //        TileA.GetComponent<Renderer>().material = MaterialDafault;
    //        TileA = null;
    //        TileB = null;

    //        TileManager.Go();
    //    }

    //}
    #endregion

    #region Singleton
    public static PlayerInput instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    Manager manager;

    Tile tileA;
    Tile tileB;

    public bool isDoneChecking = false;


    private void Start()
    {
        manager = Manager.instance;
    }

    public void ChangeFruitsPositions(Tile tile)
    {

        if (tileA == null)
        {
            tileA = tile;
            Select();
        }
        else if (tileB == null)
        {
            tileB = tile;

            float distance = Vector3.Distance(tileA.transform.position, tileB.transform.position);
            if (distance == 1)
            {
                Fruit ATile = tileA.Fruit;
                Fruit BTile = tileB.Fruit;

                ATile.SetTile(tileB);
                BTile.SetTile(tileA);

                manager.StartSearchingLinesOfFruits();

                DiselectAll();
            }
            else
            {
                DiselectAll();
            }
        }

    }

    private void Select()
    {
        tileA.Fruit.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void DiselectAll()
    {
        tileA.Fruit.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        tileB.Fruit.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        tileA = null;
        tileB = null;
    }

}
