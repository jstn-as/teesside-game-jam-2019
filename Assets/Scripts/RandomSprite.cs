using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    private void Awake()
    {
        var randomIndex = Random.Range(0, _sprites.Length - 1);
        GetComponent<SpriteRenderer>().sprite = _sprites[randomIndex];
    }
}
