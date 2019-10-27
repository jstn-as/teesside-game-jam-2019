using System;
using System.Collections;
using System.Collections.Generic;
using PaletteSwap;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivePalette : MonoBehaviour
{
    [SerializeField] private int _activePalette;
    [SerializeField] private Texture[] _palettes;

    public void SetActivePalette(int paletteIndex)
    {
        _activePalette = paletteIndex;
        FindObjectOfType<PaletteSwapLookup>().SetActivePalette(_palettes[_activePalette]);
    }

    private void Awake()
    {
        SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        SetActivePalette(_activePalette);
    }

    private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1)
    {
        SetActivePalette(_activePalette);
    }
}
