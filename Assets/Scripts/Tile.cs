using UnityEngine;
using Zenject;
using Main;

public class Tile : MonoBehaviour
{
    public int X;
    public int Y;

    public Fruit Fruit;

    public bool DestroyFlag;

    private SoundManager _soundManager;

    [SerializeField] private AudioClip _destroyFruitsSound;

    [SerializeField] private int _scoreForCell = 10;

    [Inject]
    private void Construct(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }


    public void CheckAndDestroyFruit()
    {
        if (DestroyFlag)
        {
            _soundManager.PlaySound(_destroyFruitsSound);

            PlayerResources.AddLevelMoney(_scoreForCell);

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

