using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public Text _turnCount = null;
    [SerializeField] GameObject _playerActionMenu = null;

    [Header("Brave Options")]
    [SerializeField] Text _bpCounter = null;
    [SerializeField] Button _braveButton = null;
    [SerializeField] Button _braveButtonDisabled = null;

    private int _currentTurn;
    public int _currentBP = 0;
    private int _numberPlayerBraves = 0;
    private int _maxBP = 3;

    // turn state vars
    private bool isPlayerTurn;

    private void Start()
    {
        _currentTurn = 1;
        isPlayerTurn = true;
    }

    private void Update()
    {
        // cinnebt iyt kater, testing turn count
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseTurn();
        }

        if (isPlayerTurn)
        {
            PlayerPhase();
        } else if (!isPlayerTurn)
        {
            _playerActionMenu.SetActive(false);
        }
    }

    public void PlayerPhase()
    {
        // enable action menu, re-enabled brave
        _playerActionMenu.SetActive(true);
        _braveButton.enabled = true;
        _braveButtonDisabled.enabled = false;

        if (Input.GetKeyDown(KeyCode.Q)) // player braves with key
        {
            // cap num braves to 3 **TODO FIX SO THAT Q STOPS INC SCORE
            _numberPlayerBraves += 1;
            if (_numberPlayerBraves >= 3)
            {
                _braveButton.enabled = false;
                _braveButtonDisabled.enabled = true;
            }

            // update brave counter
            _currentBP -= 1;
            _bpCounter.text =
                _currentBP.ToString();

            // update turn count
        }

        if (Input.GetKeyDown(KeyCode.E)) // player defaults with key
        {
            // end turn, add 1 BP, update BP counter
            isPlayerTurn = false;
            _currentBP += 1;
            _bpCounter.text =
                _currentBP.ToString();

            // update turn count
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
