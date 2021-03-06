using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public Tile TileA;
    public Tile TileB;

    public Material MaterialDafault;
    public Material MaterialSelected;

    public TileManager TileManager;

    public void OnClick(Tile tile) {

        if (TileA == null) {
            TileA = tile;
            TileA.GetComponent<Renderer>().material = MaterialSelected;
        }
        else if (TileB == null)
        {
            TileB = tile;

            Fruit fruitA = TileA.Fruit;
            Fruit fruitB = TileB.Fruit;

            fruitA.SetTile(TileB);
            fruitB.SetTile(TileA);

            TileA.GetComponent<Renderer>().material = MaterialDafault;
            TileA = null;
            TileB = null;

            TileManager.Go();
        }

    }

}
