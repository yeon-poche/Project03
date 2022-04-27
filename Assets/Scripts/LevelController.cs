using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Text _turnCount = null;

    private int _currentTurn;

    private void Start()
    {
        _currentTurn = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseTurn();
        }
    }

    public void IncreaseTurn()
    {
        _currentTurn++;

        // update turn count text
        _turnCount.text =
            _currentTurn.ToString();
    }
}
