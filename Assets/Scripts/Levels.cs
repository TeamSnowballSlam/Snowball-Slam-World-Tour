/// <remarks>
/// Author: Palin Wiseman
/// Date Created: 15/04/2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// Enum of levels in the game. Used to load the correct level and make sure it isn't misspelled
/// Only the Australia level is going to be in the game at the moment but I am adding them all for future use
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Levels
{
    None,
    Tutorial,
    Australia,
    Asia,
    SouthAmerica,
    Africa,
    Europe,
    NorthAmerica,
    Antarctica
}
