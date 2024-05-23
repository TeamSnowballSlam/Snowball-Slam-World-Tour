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
    public bool _doSlide;
    public KeyCode[] _requiredAction;
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public List<TutorialEvent> _tutorialEvents = new List<TutorialEvent>();
    int _eventIndex;
    bool _eventsLeft = true;

    float _lastTime;
    float _doubleInputDelay = .25f;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    private Vector2 secondLastInput;
    private Vector2 lastInput;
    private bool _doublePress;

    public TextMeshProUGUI _scoreText;
    public TextMeshProUGUI _timerText;

    public float _shakedownDuration = 30;

    private int _score;
    public UnityEvent _tutorialEnd = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        InvokeCurrentTutorialEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (_eventsLeft) // if there are still events leftover
        {
            if (_tutorialEvents[_eventIndex]._doSlide && _doublePress) // check  if it is the double tap to slide event
            {
                _doublePress = false;
                Debug.Log("Double Press");
                NextTutorialEvent();
            }
            else if (CheckForAction(_tutorialEvents[_eventIndex]._requiredAction)) // check if required action is entered to proceed
            {
                Debug.Log("Correct Key Entered");
                NextTutorialEvent();
            }
        }
        
    }

    public void OnMove(InputAction.CallbackContext context) // method that takes player input for movement keys and checks if it is a double press
    {

        moveInput = context.ReadValue<Vector2>();
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        //If there was an action performed
        if (context.performed)
        {
            //If the time between the last input and the current input is less than the double tap delay
            //And the move input is the same as the last input
            if (Time.time - _lastTime < _doubleInputDelay && moveInput == lastInput)
            {
                _doublePress = true;
            }
            secondLastInput = lastInput;
            lastInput = moveInput;
            _lastTime = Time.time;
        }
    }

    void InvokeCurrentTutorialEvent()
    {
        _tutorialEvents[_eventIndex]._eventAction.Invoke();
    }

    private bool CheckForAction(KeyCode[] keyCodes) // check if relevant keycode/s have been entered
    {
        foreach(KeyCode key in keyCodes)
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }

        return false;
    }

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
        _eventIndex++;
        if(_eventIndex > _tutorialEvents.Count - 1) // if no more events remaining
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
    }
}
