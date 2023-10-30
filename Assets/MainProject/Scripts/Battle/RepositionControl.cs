using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class RepositionControl : MonoBehaviour
    {
        private Collider2D coll_;

        private void Awake()
        {
            coll_ = GetComponent<Collider2D>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Area") == false)
                return;

            Vector3 playerPos = GameManager.Instance.player_.transform.position;
            Vector3 myPos = transform.position;


            if (transform.tag == "Ground")
            {
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                float dirX = diffX < 0 ? -1.0f : 1.0f;
                float dirY = diffY < 0 ? -1.0f : 1.0f;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 80.0f);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 80.0f);
                }
            }
            else if (transform.tag == "Enemy")
            {
                if (coll_.enabled == true)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2);
                }
            }
        }

    }
}