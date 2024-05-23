using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialInputs : MonoBehaviour
{
    public playerNumber playerNumber;

    public TutorialManager tm;

    float _lastTime;
    float _doubleInputDelay = .25f;
    private Vector2 moveInput;
    private Vector2 lastInput;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context) // method that takes player input for movement keys and checks if it is a double press
    {
        moveInput = context.ReadValue<Vector2>();
        //If there was an action performed
        if (context.performed)
        {
            //If the time between the last input and the current input is less than the double tap delay
            //And the move input is the same as the last input
            if (Time.time - _lastTime < _doubleInputDelay && moveInput == lastInput)
            {
                tm.DoPlayerInput(this.playerNumber, inputType.slide);
            }
            else
            {
                tm.DoPlayerInput(this.playerNumber, inputType.move); // only pressed once is move
            }

            lastInput = moveInput;
            _lastTime = Time.time;
             
        }
    }

    public void OnThrow(InputAction.CallbackContext context) // method that takes player input for throw key
    {
        //If there was an action performed
        if (context.performed)
        {
            tm.DoPlayerInput(this.playerNumber, inputType.throwing); // run method on tutorial manager
        }
    }

    public void OnInteract(InputAction.CallbackContext context) // method that takes player input for interact key
    {
        //If there was an action performed
        if (context.performed)
        {
            tm.DoPlayerInput(this.playerNumber, inputType.interact); // run method on tutorial manager
        }
    }

}
