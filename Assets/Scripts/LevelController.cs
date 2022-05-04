using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Text _turnCount = null;
    [SerializeField] GameObject _playerActionMenu = null;
    [SerializeField] GameObject _enemyMenu = null;

    private int _currentTurn;

    private void Start()
    {
        _currentTurn = 1;
    }

    private void Update()
    {
        // DELETE OUT LATER, testing 
        if (Input.GetKeyDown(KeyCode.P))
        {
           
        }

        // BACKSPACE = PLAYER INPUT MENU, DONT RESET 
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _playerActionMenu.SetActive(true);
            _enemyMenu.SetActive(false);
        }

        // RELOAD LEVEL WHEN R PRESSED 
        if (Input.GetKeyDown(KeyCode.R)) {
            
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
