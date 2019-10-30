using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Breakable : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _powerUpThreshold;
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _lightPrefab;
    [SerializeField] private AudioClip _breakClip;
    private static readonly int Explode1 = Animator.StringToHash("Explode");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Explode()
    {
        SfxPlayer.PlayAudio(_breakClip);
        _animator.SetTrigger(Explode1);
        Instantiate(_lightPrefab, transform.position, Quaternion.identity);
    }

    public void Destroy()
    {
        var powerUpChance = Random.Range(0f, 1);
        if (powerUpChance <= _powerUpThreshold)
        {
            var powerUpIndex = Random.Range(0, _powerUps.Length);
            Instantiate(_powerUps[powerUpIndex], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
