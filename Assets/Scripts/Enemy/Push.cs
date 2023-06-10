using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Push : MonoBehaviour
{
    private RaycastHit2D PushHit;
    private float PushDistant = 1.5f;
    private LayerMask SeePlayer;

    private void Start()
    {
        SeePlayer = LayerMask.GetMask("Player");
    }

    public void PushPlayer()
    {
        PushHit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, PushDistant, SeePlayer);
        if (PushHit.collider != null && PushHit.collider.CompareTag("Player"))
        {
            Player player = PushHit.collider.GetComponent<Player>();
            if (PushHit.collider.transform.position.x < transform.position.x)            
                StartCoroutine(ChangeValueOverTime(.05f, player, false));         
            else if (PushHit.collider.transform.position.x > transform.position.x)
                StartCoroutine(ChangeValueOverTime(.05f, player, true));     
        }
    }


    private IEnumerator ChangeValueOverTime(float duration, Player player, bool leftOrRight)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float DirectionPushing = !leftOrRight ? player.transform.position.x - elapsedTime : player.transform.position.x + elapsedTime;
            player.transform.position = new Vector3(DirectionPushing, player.transform.position.y, player.transform.position.z);
            yield return null;
        }
    }
}
