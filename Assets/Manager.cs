using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    #region Singleton
    public static Manager instance;

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

    public int Height = 12;
    public int Width = 9;

    Tile[] allTiles;
    public Fruit[] Fruits;

    PlayerInput playerInput;

    Dictionary<Vector2Int, Tile> dictionaryLots = new Dictionary<Vector2Int, Tile>();

    private void Start()
    {
        playerInput = PlayerInput.instance;
        allTiles = GetComponentsInChildren<Tile>();
        FillDictionary();
        CreateFruit();
        StartSearchingLinesOfFruits();
    }

    void FillDictionary()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector2Int key = new Vector2Int(x, y);
                dictionaryLots.Add(key, null);
            }
        }

        for (int i = 0; i < allTiles.Length; i++)
        {
            int x = Mathf.RoundToInt(allTiles[i].transform.position.x);
            int y = Mathf.RoundToInt(allTiles[i].transform.position.y);

            allTiles[i].X = x;
            allTiles[i].Y = y;

            Vector2Int tilePos = new Vector2Int(x, y);
            dictionaryLots[tilePos] = allTiles[i];
        }
    }

    void CreateFruit()
    {
        for (int i = 0; i < allTiles.Length; i++)
        {
            if (allTiles[i].Fruit == null)
            {
                Tile tile = allTiles[i];
                int randomIndex = Random.Range(0, Fruits.Length);
                Fruit fruit = Instantiate(Fruits[randomIndex], tile.transform.position+ new Vector3(0,3,0), Quaternion.identity);
                fruit.SetTile(tile);
            }

        }
    }

    public void StartSearchingLinesOfFruits()
    {
        playerInput.isDoneChecking = false;
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        CanFuze();
        yield return new WaitForSeconds(0.2f);
        DestroyFruits();
        yield return new WaitForSeconds(0.2f);
        MoveFruitsDown();
        yield return new WaitForSeconds(0.2f);
        CreateFruit();
        yield return new WaitForSeconds(0.2f);
        if (CanFuze())
        {
            StartSearchingLinesOfFruits();
        }
        else
        {
            playerInput.isDoneChecking = true;
        }
    }

    bool CanFuze()
    {
        bool result = false;
        for (int y = 0; y < Height; y++)
        {
            List<Tile> horizontalList = new List<Tile>();

            for (int x = 0; x < Width; x++)
            {
                if (HasSimilarLines(x, y, horizontalList))
                {
                    result = true;
                }

            }
            horizontalList.Clear();
        }
        for (int x = 0; x < Width; x++)
        {
            List<Tile> verticalList = new List<Tile>();

            for (int y = 0; y < Height; y++)
            {
                if (HasSimilarLines(x, y, verticalList))
                {
                    result = true;
                }

            }
            verticalList.Clear();
        }


        return result;
    }

    bool HasSimilarLines(int x, int y, List<Tile> listToCheck)
    {
        Vector2Int key = new Vector2Int(x, y);
        Tile tile = dictionaryLots[key];

        if (tile == null)
        {
            listToCheck.Clear();
            return false;
        }
        if (listToCheck.Count > 0)
        {
            Tile previousTile = listToCheck[listToCheck.Count - 1];
            if (previousTile.Fruit.Index != tile.Fruit.Index)
            {
                listToCheck.Clear();
            }
        }

        listToCheck.Add(tile);
        if (listToCheck.Count >= 3)
        {
            for (int i = 0; i < listToCheck.Count; i++)
            {
                listToCheck[i].DestroyFlag = true;
            }
            return true;
        }
        return false;
    }

    void DestroyFruits()
    {
        foreach (var item in allTiles)
        {
            item.DestroyFruit();
        }
    }

    void MoveFruitsDown()
    {
        
        for (int x = 0; x < Width; x++)
        {
            int column = 0;
            for (int y = 0; y < Height; y++)
            {
                Vector2Int key = new Vector2Int(x,y);
                Tile tile = dictionaryLots[key];
                if (tile==null)
                {
                    column = 0;
                    continue;
                }
                if (tile.Fruit==null)
                {
                    column++;
                }
                else
                {
                    Vector2Int desiredPos = key - new Vector2Int(0,column);
                    tile.Fruit.SetTile(dictionaryLots[desiredPos]);
                }
            }
        }
    }
}
