/// <remarks>
/// Author: Erika Stuart
/// </remarks>
/// <summary>
/// This script allows the player to throw snowballs.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowSnowballs : MonoBehaviour
{
    private bool invulnerable = false;

    public bool Invulnerable
    {
        get { return invulnerable; }
        set { 
            if (value == true)
            {
                invulnerable = value;
                StartCoroutine(InvulnerabilityTimer());
            }
         }
    }
    
    [SerializeField]
    private GameObject snowballPrefab;
    private GameObject snowball; // instantiate of snowballPrefab
    private Vector3 snowballPosition;
    public SnowInventory snowInventory;
    private Animator animator;
    
    private GameObject snowballWP;

    private bool canThrow;

    private const int SPEED = 20;

    private List<AudioClip> audioClips = new List<AudioClip>();
    public PlaySFX playSFX;
    
    private IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(1.5f);
        invulnerable = false;
    }

    void Start()
    {
        playSFX = GetComponent<PlaySFX>();
        snowInventory = GetComponent<SnowInventory>();
        animator = GetComponent<Animator>();
        if (animator == null) //If the animator is null it's in the children
        {
            animator = GetComponentInChildren<Animator>();
        }
        snowballWP = this.transform.Find("SnowballPosition").gameObject;
        canThrow = true;
        audioClips.AddRange(Resources.LoadAll<AudioClip>("SoundEffects/Throws"));
    }

    // Player
    public void ThrowSnowball(InputAction.CallbackContext context)
    {
        if (!canThrow) return; // if can't throw, don't throw snowball
        if (GameSettings.currentGameState != GameStates.InGame) return; // if game is over, don't throw snowball
        if (snowInventory.CurrentAmmo <= 0) return; // if no ammo, don't throw snowball
        if (context.phase != InputActionPhase.Started) return; // only throw snowball once--when phase is started
        if (GetComponent<PlayerMovement>().IsSliding) return; // if player is sliding, don't throw snowball
        animator.SetTrigger("doThrow"); // trigger animation
        snowInventory.CurrentAmmo--;
        canThrow = false;

        //StartCoroutine(ThrowCooldown());
    }

    public void ThrowSnowball()
    {
        if (GameSettings.currentGameState == GameStates.PostGame) return; // if game is over, don't throw snowball
        animator.SetTrigger("doThrow"); // trigger animation
        
    }

    // <summary>
    // Called during a frame of the throwing animation
    // </summary>
    public void SnowballAnimation(string name)
    {
        //Triggers a random throw sound using the name of a random object in the audioClips list
        playSFX.playSound(audioClips[Random.Range(0, audioClips.Count)].name);
        snowballPosition = snowballWP.transform.position;
        snowball = Instantiate(
            snowballPrefab,
            snowballPosition + transform.forward,
            Quaternion.identity
        );
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * SPEED, ForceMode.Impulse); // snowball moves at a constant rate

        if (name == "Player")
        {
            snowball.GetComponent<SnowballCollision>().owner = "Player"; // owner of snowball is the player
        }
        else if (name == "Enemy")
        {
            snowball.GetComponent<SnowballCollision>().owner = "Enemy"; // owner of snowball is the enemy
        }
    }

    public void SetCanThrow()
    {
        canThrow = true;
    }

}
