using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public ZipLine zipLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Animator>().Play("idleZipLine");
            collision.GetComponent<WeaponHold>().WeaponSwitch.SetActive(false);
            StartCoroutine(zipLine.Zipline(zipLine.line, collision.GetComponent<Rigidbody2D>(), collision.gameObject));
            if (zipLine.StartPosition.position.x < zipLine.TheEndPositon.position.x)
                collision.transform.localScale = new Vector3(1, 1, 1);
            else
                collision.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
