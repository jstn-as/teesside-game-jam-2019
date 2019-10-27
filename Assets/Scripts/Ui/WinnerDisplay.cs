using System;
using Player;
using Spawners;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class WinnerDisplay : MonoBehaviour
    {
        [SerializeField] private string _displayFormat = "Player {0} Won!";
        private TextMeshProUGUI _text;
        private WhoWon _whoWon;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            // Wait for the winner's number to update, then display it.
            if (_whoWon) return;
            _whoWon = FindObjectOfType<WhoWon>();
            var winner = _whoWon.GetWinnerNumber();
            _text.text = string.Format(_displayFormat, winner);
        }
    }
}
