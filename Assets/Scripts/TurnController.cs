using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public Text _turnCounterText = null;
    [SerializeField] GameObject _playerActionMenu = null;

    [Header("Brave Options")]
    [SerializeField] Text _bpCounter = null;
    [SerializeField] Button _braveButton = null;
    [SerializeField] ButtonColor _braveButtonColor = null;
    public int _numberPlayerBraves = 0;

    [Header("Default Options")]
    [SerializeField] Button _defaultButton = null;
    [SerializeField] ButtonColor _defaultButtonColor = null;
    [SerializeField] bool _playerDefaulted = false;

    [Header("BP")]
    public int _currentBP = 0;

    [Header("Attack Options")]
    [SerializeField] GameObject _enemyMenu = null;
    [SerializeField] EnemyHealth _enemy01 = null;
    [SerializeField] EnemyHealth _enemy02 = null;
    [SerializeField] Button _enemy01Button = null;
    [SerializeField] Button _enemy02Button = null;

    [Header("Player Options")]
    [SerializeField] int _playerDamage = 100;
    [SerializeField] GameObject _endTurnPnl = null;
    //[SerializeField] PlayerController _player = null;

    [Header("Feedback/UI")]
    [SerializeField] Button _endTurnButton = null;

    // save start of phase variables
    private int _startingBP = 0;
    private bool _bpIsSaved = false;

    private bool _playerPhaseActive = true;
    private bool _enemyMenuIsActive = false;
    private int _currentTurn;
    private int _maxBP = 3;

    // start of attack phase vars
    private int _numAttacks = 1;
    private int _currentAttack = 0;
    private bool _maxAttacksReached = false;
    private ButtonColor _enemy01BC;
    private ButtonColor _enemy02BC;
    private ButtonColor _endTurnBC;

    // turn state vars
    private bool turnCanEnd = false;
    public bool isPlayerTurn;

    private void Start()
    {
        _endTurnPnl.SetActive(true);
        _braveButton.GetComponent<ButtonColor>();
        _defaultButton.GetComponent<ButtonColor>();
        _enemy01.GetComponent<EnemyHealth>();
        _enemy01.GetComponent<EnemyHealth>();

        // feedback
        _enemy01BC =
            _enemy01Button.GetComponent<ButtonColor>();
        _enemy02BC =
            _enemy02Button.GetComponent<ButtonColor>();
        _endTurnBC =
            _endTurnButton.GetComponent<ButtonColor>();

        _playerPhaseActive = true;
        _enemyMenuIsActive = false;
        _bpIsSaved = false;
        _startingBP = 0;
        _numberPlayerBraves = 0;
        _currentTurn = 1;
        isPlayerTurn = true;
    }

    private void Update()
    {
        // comment out later, testing turn count
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseTurn();
        }

        if (isPlayerTurn)
        {
            RunEndTurnButton();

            if (!_enemyMenuIsActive)
            {
                PlayerPhase();
            } else
            {
                AttackEnemyPhase();
            }
        } 

        if (Input.GetKeyDown(KeyCode.Return) && turnCanEnd)
        {
            EndPlayerTurn();
        }

        // RESET TURN
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetPlayerPhase();
        }
    }

    public void PlayerPhase()
    {
        StartPlayerTurn();

        // BRAVE
        if (!_playerDefaulted && _numberPlayerBraves < _maxBP &&
            !_enemyMenuIsActive)        // caps player braves at 3
        {
            _braveButton.enabled = true;

            if (Input.GetKeyDown(KeyCode.Q) && (!_enemyMenuIsActive) && !_maxAttacksReached) // player braves with key
            {
                Brave();
            }
        }
        else { //disable button when player brave at max 
            _braveButtonColor.DisableButtonColor();
            _braveButton.interactable = false;
        }

        // DEFAULT
        if (Input.GetKeyDown(KeyCode.E) && (!_enemyMenuIsActive) && 
            !_maxAttacksReached) // player defaults with key
        {
            Default();
        }

        // OPEN ENEMY MENU
        if (Input.GetKeyDown(KeyCode.W))
        {
            AttackEnemyPhase();
        }
        
    }

    public void EndPlayerTurn()
    {
        IncreaseTurn();
        _numberPlayerBraves = 0;
        _playerPhaseActive = false;
        isPlayerTurn = false;

        _playerActionMenu.SetActive(false);
        _enemyMenu.SetActive(false);
        _endTurnPnl.SetActive(false);
    }

    public void StartPlayerTurn()
    {
        _enemyMenuIsActive = false;

        if (!_bpIsSaved)
        {
            // enable action menu, re-enabled brave
            _playerActionMenu.SetActive(true);
            _braveButton.enabled = true;

            _startingBP = _currentBP;
            Debug.Log("starting bp: " + _startingBP);

            _bpIsSaved = true;
        }
    }

    private void Brave()
    {
        _numberPlayerBraves++;

        _currentBP--;
        _bpCounter.text =
            _currentBP.ToString();
    }

    private void Default()
    {
        if (!_playerDefaulted)
        {
            // disable brave button
            _braveButton.interactable = false;
            _braveButtonColor.DisableButtonColor();

            // disable default buttons
            _defaultButton.interactable = false;
            _defaultButtonColor.DisableButtonColor();

            // disable attack buttons
            _enemy01BC.DisableButtonColor();
            _enemy02BC.DisableButtonColor();

            if (_currentBP < _maxBP) // cap bp at 3
            {
                // end turn, add 1 BP, update BP counter
                _currentBP += 1;
                _bpCounter.text =
                    _currentBP.ToString();
            }
            else
            {
                _currentBP = _maxBP;
            }
        }
        _playerDefaulted = true;
    }

    private void AttackEnemyPhase()
    {
        StartAttackEnemyPhase();

        if (!_maxAttacksReached && !_playerDefaulted)
        {
            if (Input.GetKeyDown(KeyCode.A)) {
                Debug.Log("enemy1 attacked");
                _currentAttack += 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("enemy2 attacked");
                _currentAttack += 1;
            }

            if (_currentAttack == _numAttacks)
            {
                _maxAttacksReached = true;
                Debug.Log("max attacks reached");
                turnCanEnd = true;

                _enemy01BC.DisableButtonColor();
                _enemy02BC.DisableButtonColor();
                _braveButtonColor.DisableButtonColor();
                _defaultButtonColor.DisableButtonColor();  
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            _enemyMenuIsActive = false;
            _enemyMenu.SetActive(false);

            _playerActionMenu.SetActive(true);
        }
    }
    
    public void StartAttackEnemyPhase()
    {
        _enemyMenuIsActive = true;
        _playerActionMenu.SetActive(false);
        _enemyMenu.SetActive(true);

        _numAttacks = 1 + _numberPlayerBraves;
    }

    public void ResetPlayerPhase()
    {
        // re-eneable action menu
        _enemyMenuIsActive = false;
        _playerActionMenu.SetActive(true);
        _enemyMenu.SetActive(false);

        // re-enable buttons, reset player braves and defaults and attacks
        _braveButton.interactable = true;
        _braveButtonColor.ResetButtonColor();
        _defaultButton.interactable = true;
        _defaultButtonColor.ResetButtonColor();
        _enemy01BC.ResetButtonColor();
        _enemy02BC.ResetButtonColor();
        _endTurnBC.DisableButtonColor();

        // reset bp vars 
        _currentBP = _startingBP;
        _numberPlayerBraves = 0;
        _playerDefaulted = false;
        _currentAttack = 0;
        _maxAttacksReached = false;
        turnCanEnd = false;

        // update canvas
        _bpCounter.text =
            _currentBP.ToString();
    } // resets entire turn

    public void IncreaseTurn()
    {
        _currentTurn++;

        // update turn count text
        _turnCounterText.text =
            _currentTurn.ToString();
    }

    private void RunEndTurnButton()
    {
        if (turnCanEnd)
        {
            _endTurnBC.ResetButtonColor();
        } else
        {
            _endTurnBC.DisableButtonColor();
        }
    }

}
