using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnimCatchUp : MonoBehaviour
{
    public Animator KeyAnimOne;
    public Animator KeyAnimTwo;
    private void Start()
    {
        KeyAnimTwo.SetBool("KeyPressed", true);
    }
    public void EndAnimPressedCatchUp()
    {
        if (KeyAnimTwo.GetBool("KeyPressed"))
        {
            KeyAnimOne.SetBool("KeyPressed", true);
            KeyAnimTwo.SetBool("KeyPressed", false);
            return;
        }
        else if (KeyAnimOne.GetBool("KeyPressed"))
        {
            KeyAnimOne.SetBool("KeyPressed", false);
            KeyAnimTwo.SetBool("KeyPressed", true);
            return;
        }

    }
}
