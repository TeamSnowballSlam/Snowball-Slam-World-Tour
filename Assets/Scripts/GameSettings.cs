using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static PenguinType Player1Penguin = PenguinType.None; // The penguin type for player 1
    public static PenguinType Player2Penguin = PenguinType.None; // The penguin type for player 2
    public static bool Player2Exists = false; // If player 2 exists. This is set in character select or when player 2 hot joins
    public static Levels SelectedLevel = Levels.Movement; // The level selected for the game
    //Hardcoded to Movement for testing
    public static GameStates currentGameState = GameStates.PreGame; // The current state of the game
}
