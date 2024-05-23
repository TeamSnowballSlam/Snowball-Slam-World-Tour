using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

[System.Serializable]
public class TutorialEvent // class that holds tutorial event data
{
    public UnityEvent _eventAction = new UnityEvent();
    public inputType _inputType;
}
//public class TutorialEvent // class that holds tutorial event data
//{
//    public UnityEvent _eventAction = new UnityEvent();
//    public bool _doSlide;
//    public KeyCode[] _requiredActionPlayerOne;
//    public KeyCode[] _requiredActionPlayerTwo;
//}

public enum inputType
{
    move,
    slide,
    throwing,
    interact,
    none
}

public enum playerNumber
{
    playerOne,
    playerTwo
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public List<TutorialEvent> _tutorialEvents = new List<TutorialEvent>();
    int _eventIndex;
    bool _eventsLeft = true;
    bool _doingNext;

    float _lastTimePlayerOne, _lastTimePlayerTwo;
    float _doubleInputDelay = .25f;
    private KeyCode _secondLastInputPlayerOne, _secondLastInputPlayerTwo;
    private KeyCode _lastInputPlayerOne, _lastInputPlayerTwo;
    private bool _doublePress;

    public TextMeshProUGUI _scoreText;
    public TextMeshProUGUI _timerText;

    public float _shakedownDuration = 30;

    private int _score;
    public UnityEvent _tutorialEnd = new UnityEvent();

    public bool twoplayer;

    bool p1check = false;
    bool p2check = false;

    public GameObject playerOneInput, playerTwoInput;

    public InputStatus pOneInputStatus, pTwoInputStatus;

    private void Awake()
    {
        GameSettings.Player2Exists = true; // NEED TO CHANGE WHEN LEVEL IMPLEMENTED
        GameSettings.currentGameState = GameStates.PreGame;

        PlayerInput playerOne = PlayerInput.Instantiate(playerOneInput, controlScheme: "WASD", pairWithDevice: Keyboard.current);
        playerOne.gameObject.GetComponent<TutorialInputs>().tm = this;
        PlayerInput playerTwo = PlayerInput.Instantiate(playerTwoInput, controlScheme: "Arrows", pairWithDevice: Keyboard.current);
        playerTwo.gameObject.GetComponent<TutorialInputs>().tm = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        InvokeCurrentTutorialEvent();
    }

    // Update is called once per frame
    void Update()
    {
        //if (_eventsLeft && !_doingNext) // if there are still events leftover
        //{
        //    if (_tutorialEvents[_eventIndex]._doSlide)
        //    {
        //        if (CheckForDoubleAction(_tutorialEvents[_eventIndex])) // check  if it is the double tap to slide event
        //        {
        //            Debug.Log("Double Press");
        //            _doingNext = true;
        //            Invoke(nameof(NextTutorialEvent), 1f);
        //        }
        //    }
        //    else if (CheckForAction(_tutorialEvents[_eventIndex])) // check if required action is entered to proceed
        //    {
        //        Debug.Log("Correct Key Entered");
        //        _doingNext = true;
        //        Invoke(nameof(NextTutorialEvent), 1f);
        //        //NextTutorialEvent();
        //    }
        //}

        pOneInputStatus.actionComplete = p1check;
        pTwoInputStatus.actionComplete = p2check;

        if (_eventsLeft && !_doingNext)
        {
            if (CheckForInput(_tutorialEvents[_eventIndex]) && _tutorialEvents[_eventIndex]._inputType != inputType.none)
            {
                Debug.Log("Correct Key Entered");
                _doingNext = true;
                Invoke(nameof(NextTutorialEvent), 1f);
            }
        }

    }

    public bool CheckForInput(TutorialEvent _tutorialEvent)
    {
        if (twoplayer)
        {
            if(p1check && p2check)
            {
                return true;
            }
        }
        else
        {
            if (p1check)
            {
                return true;
            }
        }

        return false;
    }

    public void DoPlayerInput(playerNumber pNum, inputType input)
    {
        if(input == _tutorialEvents[_eventIndex]._inputType)
        {
            switch (pNum)
            {
                case playerNumber.playerOne: p1check = true; break;
                case playerNumber.playerTwo: p2check = true; break;
                default: break;
            }
        }
    }

    //public void OnMove(InputAction.CallbackContext context) // method that takes player input for movement keys and checks if it is a double press
    //{
    //    if (_tutorialEvents[_eventIndex]._doSlide)
    //    {
    //        moveInput = context.ReadValue<Vector2>();
    //        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
    //        //If there was an action performed
    //        if (context.performed)
    //        {
    //            //If the time between the last input and the current input is less than the double tap delay
    //            //And the move input is the same as the last input
    //            if (Time.time - _lastTime < _doubleInputDelay && moveInput == lastInput)
    //            {
    //                _doublePress = true;
    //            }
    //            secondLastInput = lastInput;
    //            lastInput = moveInput;
    //            _lastTime = Time.time; 
    //        }
    //    }
    //}

    void InvokeCurrentTutorialEvent()
    {
        _tutorialEvents[_eventIndex]._eventAction.Invoke();
    }

    //private bool CheckForDoubleAction(TutorialEvent tutorialEvent)
    //{
    //    if (twoplayer) // CHANGE TO GAME SETTINGS but essentially if the game is two player check for both player inputs
    //    {
    //        if (!p1check)
    //        {
    //            foreach (KeyCode key in tutorialEvent._requiredActionPlayerOne)
    //            {
    //                if (Input.GetKeyDown(key))
    //                {
    //                    if (Time.time - _lastTimePlayerOne < _doubleInputDelay && key == _lastInputPlayerOne)
    //                    {
    //                        Debug.Log("p1 true");
    //                        p1check = true;
    //                    }

    //                    _secondLastInputPlayerOne = _lastInputPlayerOne;
    //                    _lastInputPlayerOne = key;
    //                    _lastTimePlayerOne = Time.time;
    //                }
    //            }
    //        }
    //        if (!p2check)
    //        {
    //            foreach (KeyCode key in tutorialEvent._requiredActionPlayerTwo)
    //            {
    //                if (Input.GetKeyDown(key))
    //                {
    //                    if (Time.time - _lastTimePlayerTwo < _doubleInputDelay && key == _lastInputPlayerTwo)
    //                    {
    //                        Debug.Log("p2 true");
    //                        p2check = true;
    //                    }

    //                    _secondLastInputPlayerTwo = _lastInputPlayerTwo;
    //                    _lastInputPlayerTwo = key;
    //                    _lastTimePlayerTwo = Time.time;
    //                }
    //            }
    //        }

    //        return p1check ? p2check ? true : false : false;

    //    }
    //    else // if single player only check for player two inputs
    //    {
    //        foreach (KeyCode key in tutorialEvent._requiredActionPlayerOne)
    //        {
    //            if (Input.GetKeyDown(key))
    //            {
    //                if (Time.time - _lastTimePlayerOne < _doubleInputDelay && key == _lastInputPlayerOne)
    //                {
    //                    Debug.Log("p1 true");
    //                    p1check = true;
    //                    return true;
    //                }

    //                _secondLastInputPlayerOne = _lastInputPlayerOne;
    //                _lastInputPlayerOne = key;
    //                _lastTimePlayerOne = Time.time;
    //            }
    //        }
    //    }


    //    return false;
    //}

    //private bool CheckForAction(TutorialEvent tutorialEvent) // check if relevant keycode/s have been entered
    //{
    //    if (twoplayer) // CHANGE TO GAME SETTINGS but essentially if the game is two player check for both player inputs
    //    {
    //        if (!p1check)
    //        {
    //            foreach (KeyCode key in tutorialEvent._requiredActionPlayerOne)
    //            {
    //                if (Input.GetKey(key))
    //                {
    //                    Debug.Log("p1 true");
    //                    p1check = true;
    //                }
    //            }
    //        }
    //        if (!p2check)
    //        {
    //            foreach (KeyCode key in tutorialEvent._requiredActionPlayerTwo)
    //            {
    //                if (Input.GetKey(key))
    //                {
    //                    Debug.Log("p2 true");
    //                    p2check = true;
    //                }
    //            }
    //        }

    //        return p1check ? p2check ? true : false : false;
            
    //    }
    //    else // if single player only check for player two inputs
    //    {
    //        foreach (KeyCode key in tutorialEvent._requiredActionPlayerOne)
    //        {
    //            if (Input.GetKey(key))
    //            {
    //                Debug.Log("p1 true");
    //                p1check = true;
    //                return true;
    //            }
    //        }
    //    }
        

    //    return false;
    //}

    public void UpdateScore()
    {
        if (!_eventsLeft) // verify if during shakedown time
        {
            _score++;
            _scoreText.text = _score.ToString();
        }
    }

    private void NextTutorialEvent() // go to next event
    {
        _doingNext = false;
        _eventIndex++;
        p1check = false;
        p2check = false;
        if (_eventIndex > _tutorialEvents.Count - 1) // if no more events remaining
        {
            _eventsLeft = false;
        }
        else
        {
            InvokeCurrentTutorialEvent(); // otherwise go next event
        }

    }

    public void StartShakedown()
    {
        StartCoroutine(ShakedownTime());
        GameSettings.currentGameState = GameStates.InGame;
    }

    private IEnumerator ShakedownTime() // after the last event free time to get used to controls, lasts 30 seconds
    {
        float time = 0;

        while ( time < _shakedownDuration) 
        {
            _timerText.text = $"{_shakedownDuration - Mathf.Round(time)}"; // update timer text
            time += Time.deltaTime;
            yield return null;
        }

        _tutorialEnd.Invoke(); // end tutorial

        GameSettings.currentGameState = GameStates.PostGame;
    }
}
