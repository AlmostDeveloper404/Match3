using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    public Dictionary<Vector2Int, Tile> TilesDictionary = new Dictionary<Vector2Int, Tile>();
    public int XNumber;
    public int YNumber;

    public GameObject[] Fruits;
    public Tile[] AllTiles;

    private void Start()
    {
        // Массив всех тайлов
        AllTiles = GetComponentsInChildren<Tile>();
        SetupDictionary();
        CreateAllFruits();
        Go();
    }

    // Заполняем словарь и назначаем X и Y тайлам
    public void SetupDictionary()
    {
        for (int x = 0; x < XNumber; x++)
        {
            for (int y = 0; y < YNumber; y++)
            {
                Vector2Int xy = new Vector2Int(x, y);
                TilesDictionary.Add(xy, null);
            }
        }

        for (int i = 0; i < AllTiles.Length; i++)
        {
            int tileX = Mathf.RoundToInt(AllTiles[i].transform.position.x);
            int tileY = Mathf.RoundToInt(AllTiles[i].transform.position.y);
            AllTiles[i].X = tileX;
            AllTiles[i].Y = tileY;
            Vector2Int xy = new Vector2Int(tileX, tileY);
            TilesDictionary[xy] = AllTiles[i];
        }

    }

    // Создать все фрукты в пустых тайлах
    public void CreateAllFruits()
    {
        for (int i = 0; i < AllTiles.Length; i++)
        {
            if (AllTiles[i].Fruit == null)
            {
                int fruitIndex = Random.Range(0, Fruits.Length);
                Vector3 position = new Vector3(AllTiles[i].X, AllTiles[i].Y, 0);
                GameObject newFruitObject = Instantiate(Fruits[fruitIndex], position + new Vector3(0f, 3f, 0f), Quaternion.identity);
                Fruit newFruit = newFruitObject.GetComponent<Fruit>();
                newFruit.SetTile(AllTiles[i]);
            }
        }
    }

    public void Go()
    {
        StartCoroutine("GoCorutine");
    }

    public IEnumerator GoCorutine()
    {
        Check();
        yield return new WaitForSeconds(0.25f);
        DestroyFruits();
        yield return new WaitForSeconds(0.25f);
        MoveDown();
        yield return new WaitForSeconds(0.25f);
        CreateAllFruits();
        yield return new WaitForSeconds(0.25f);
        if (Check())
        {
            Go();
        }

    }

    // Уничтожить все фрукты, у которых DestroyFlag равен true
    public void DestroyFruits()
    {
        foreach (KeyValuePair<Vector2Int, Tile> kvp in TilesDictionary)
        {
            if (kvp.Value)
            {
                kvp.Value.CheckAndDestroyFruit();
            }
        }
    }

    public bool Check()
    {
        bool result = false;
        List<Tile> identicalHorizontalTiles = new List<Tile>();
        for (int y = 0; y < YNumber; y++)
        {
            for (int x = 0; x < XNumber; x++)
            {
                if (CheckByXY(x, y, identicalHorizontalTiles))
                {
                    result = true;
                }
            }
            identicalHorizontalTiles.Clear();
        }

        List<Tile> identicalVerticalTiles = new List<Tile>();
        for (int x = 0; x < XNumber; x++)
        {
            for (int y = 0; y < YNumber; y++)
            {
                if (CheckByXY(x, y, identicalVerticalTiles))
                {
                    result = true;
                }
            }
            identicalVerticalTiles.Clear();
        }

        return result;
    }

    public bool CheckByXY(int x, int y, List<Tile> identicalTilesList)
    {

        Vector2Int xy = new Vector2Int(x, y);
        Tile tile = TilesDictionary[xy];

        if (tile == null)
        {
            identicalTilesList.Clear();
            return false;
        }

        if (identicalTilesList.Count > 0)
        {
            Tile preveouseTile = identicalTilesList[identicalTilesList.Count - 1];
            if (tile.Fruit.FruitTypeIndex != preveouseTile.Fruit.FruitTypeIndex)
            {
                identicalTilesList?.Clear();
            }
        }

        identicalTilesList.Add(tile);

        // Если число одинаковых фруктов подряд больше 3 — отмечаем, что их нужно уничтожить
        if (identicalTilesList.Count >= 3)
        {
            foreach (var item in identicalTilesList)
            {
                item.DestroyFlag = true;
            }
            return true;
        }
        return false;
    }

    public void MoveDown()
    {

        for (int x = 0; x < XNumber; x++)
        {
            int n = 0;
            for (int y = 0; y < YNumber; y++)
            {
                Vector2Int xy = new Vector2Int(x, y);
                if (TilesDictionary[xy] == null)
                {
                    n = 0;
                    continue;
                }
                if (TilesDictionary[xy].Fruit == null)
                {
                    n++;
                }
                else
                {
                    Vector2Int tileIndex = xy - new Vector2Int(0, n);
                    TilesDictionary[xy].Fruit.SetTile(TilesDictionary[tileIndex]);
                }
            }
        }
    }
}
