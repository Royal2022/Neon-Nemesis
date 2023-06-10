using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipLine : MonoBehaviour
{
    public LineRenderer line;
    public Transform StartPosition;
    public Transform TheEndPositon;
    public float speed = 5;


    private void Start()
    {
        line.SetPosition(0, StartPosition.position);
        line.SetPosition(1, TheEndPositon.position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            collision.GetComponent<Animator>().Play("idleZipLine");
            collision.GetComponent<WeaponHold>().WeaponSwitch.SetActive(false);
            StartCoroutine(Zipline(line, collision.GetComponent<Rigidbody2D>(), collision.gameObject));
            if (StartPosition.position.x < TheEndPositon.position.x)
                collision.transform.localScale = new Vector3(1, 1, 1);
            else
                collision.transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    public IEnumerator Zipline(LineRenderer line, Rigidbody2D rb, GameObject player)
    {
        Vector3 start = line.GetPosition(0) + new Vector3(0, -1f, 0);
        Vector3 des = line.GetPosition(1) + new Vector3(0, -1f, 0);


        float dist = Vector3.Distance(start, des);
        float duration = dist / speed;
        float currentDuration = 0;
        rb.simulated = false;
        player.GetComponent<Animator>().SetBool("ZipLine", true);

        while (currentDuration < duration && !Input.GetKey(KeyCode.Space))
        {
            float t = currentDuration / duration;
            t = t * t * (3f - 2f * t);
            player.transform.position = Vector3.Lerp(start, des, t);
            currentDuration += Time.deltaTime;

            yield return null;
        }
        //player.transform.position = des;

        rb.simulated = true;
        player.GetComponent<Animator>().SetBool("ZipLine", false);
        player.GetComponent<WeaponHold>().WeaponSwitch.SetActive(true);
    }
}
