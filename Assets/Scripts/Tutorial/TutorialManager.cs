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

    public TextMeshProUGUI _scoreText;
    public TextMeshProUGUI _timerText;
    public GameObject tutorialUI;

    public float _shakedownDuration = 30;

    private int _score = 0;
    public UnityEvent _tutorialEnd = new UnityEvent();


    bool p1check = false;
    bool p2check = false;

    public GameObject playerOneInput, playerTwoInput;

    public InputStatus pOneInputStatus, pTwoInputStatus;

    public bool pOneInputHeld, pTwoInputHeld;
    bool heldBegun;

    private void Awake()
    {
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

        pOneInputStatus.UpdateValues(p1check, _tutorialEvents[_eventIndex]._inputType, pOneInputHeld, heldBegun);
        pTwoInputStatus.UpdateValues(p2check, _tutorialEvents[_eventIndex]._inputType, pTwoInputHeld, heldBegun);
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
        if (GameSettings.Player2Exists)
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

    public void IsHolding(bool holding, playerNumber pNum, inputType input)
    {
        if(holding && input == _tutorialEvents[_eventIndex]._inputType)
        {
            switch (pNum)
            {
                case playerNumber.playerOne: pOneInputHeld = true; break;
                case playerNumber.playerTwo: pTwoInputHeld = true; break;
                default: break;
            }
        }
        else
        {
            pOneInputHeld = false;
            pTwoInputHeld = false;
        }

        heldBegun = true;
    }

    void InvokeCurrentTutorialEvent()
    {
        _tutorialEvents[_eventIndex]._eventAction.Invoke();
    }

    public void UpdateScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }

    private void NextTutorialEvent() // go to next event
    {
        _doingNext = false;
        _eventIndex++;
        p1check = false;
        p2check = false;
        pOneInputHeld = false;
        pTwoInputHeld = false;

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
