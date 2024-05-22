using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class TutorialEvent
{
    public UnityEvent eventAction = new UnityEvent();
    public bool _doSlide;
    public KeyCode[] requiredAction;
}

//public enum action
//{
//    movement,
//    dash,
//    reload,
//    throwBall
//}

public class TutorialManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        InvokeCurrentTutorialEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (_eventsLeft)
        {
            if (_tutorialEvents[_eventIndex]._doSlide && _doublePress)
            {
                _doublePress = false;
                Debug.Log("Double Press");
                NextTutorialEvent();
            }
            else if (CheckForAction(_tutorialEvents[_eventIndex].requiredAction))
            {
                Debug.Log("Correct Key Entered");
                NextTutorialEvent();
            }
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
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
        _tutorialEvents[_eventIndex].eventAction.Invoke();
    }

    private bool CheckForAction(KeyCode[] keyCodes)
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

    

    private void NextTutorialEvent()
    {
        _eventIndex++;
        if(_eventIndex > _tutorialEvents.Count - 1)
        {
            Debug.Log("Done");
            _eventsLeft = false;
        }
        else
        {
            InvokeCurrentTutorialEvent();
        }

    }
}
