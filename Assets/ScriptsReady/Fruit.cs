using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    #region Fruit
    //public int FruitTypeIndex;
    //public Vector3 TargetPosition;
    //public Tile Tile;

    //private void Update()
    //{
    //    transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * 10f);
    //}

    //public void SetTile(Tile tile)
    //{
    //    TargetPosition = tile.transform.position;
    //    if (Tile && Tile.Fruit == this) {
    //        Tile.Fruit = null;
    //    }
    //    tile.Fruit = this;
    //    Tile = tile;
    //}

    //public void Die() {
    //    StartCoroutine(DieCorutine());
    //}

    //public IEnumerator DieCorutine() {
    //    for (float f = 1; f > 0f; f-= Time.deltaTime * 4f)
    //    {
    //        transform.localScale = Vector3.one * f;
    //        yield return null;
    //    }
    //    Destroy(gameObject);
    //}
    #endregion

    public Tile CurrentTile;
    public Vector3 TargetPosition;
    public int Index;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,TargetPosition,Time.deltaTime*20f);
    }
    public void SetTile(Tile tile)
    {
        TargetPosition = tile.transform.position;
        if (CurrentTile && CurrentTile.Fruit==this)
        {
            CurrentTile.Fruit = null;
        }
        tile.Fruit = this;
        CurrentTile = tile;
    }

    public void Die()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        for (float i = 1; i > 0; i-=Time.deltaTime * 10f )
        {
            transform.localScale = new Vector3(i,i,i);
            yield return null;
        }
        Destroy(gameObject);
    }
}
