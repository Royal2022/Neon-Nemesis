using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyAnimCatchUpThree : MonoBehaviour
{
    public Animator KeyAnimOne;
    public Animator KeyAnimTwo;
    public Animator KeyAnimThree;

    private void Start()
    {
        KeyAnimOne.SetBool("KeyPressed", true);
    }
    public void EndAnimPressedCatchUp()
    {
        if (KeyAnimOne.GetBool("KeyPressed"))
        {
            KeyAnimOne.SetBool("KeyPressed", false);
            KeyAnimTwo.SetBool("KeyPressed", true);
            KeyAnimThree.SetBool("KeyPressed", false);
            return;
        }
        else if (KeyAnimTwo.GetBool("KeyPressed"))
        {
            KeyAnimOne.SetBool("KeyPressed", false);
            KeyAnimTwo.SetBool("KeyPressed", false);
            KeyAnimThree.SetBool("KeyPressed", true);
            return;
        }
        else if (KeyAnimThree.GetBool("KeyPressed"))
        {
            KeyAnimOne.SetBool("KeyPressed", true);
            KeyAnimTwo.SetBool("KeyPressed", false);
            KeyAnimThree.SetBool("KeyPressed", false);
            return;
        }

    }
}
