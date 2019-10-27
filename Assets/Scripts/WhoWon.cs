using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoWon : MonoBehaviour
{
    [SerializeField] private int _winnerNumber;

    public void SetWinnerNumber(int newNumber)
    {
        _winnerNumber = newNumber;
    }

    public int GetWinnerNumber()
    {
        return _winnerNumber;
    }
}
