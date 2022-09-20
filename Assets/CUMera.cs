using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUMera : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool is_left;
    //private Gay TransformPlayer;
    private int last_X;


    void start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(is_left);
    }
    public void FindPlayer(bool player_is_left)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        last_X = Mathf.RoundToInt(player.position.x);
        if(player_is_left)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }
    void update()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player)
        {
            int curent_X = Mathf.RoundToInt(player.position.x);

            if(curent_X > last_X)is_left = false;
            else if(curent_X < last_X)is_left = true;
            last_X = curent_X;
            Vector3 target;
            if(is_left)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }
            Vector3 curent_p = new Vector3(transform.position.x, target.y, dumping * Time.deltaTime);
            transform.position = curent_p;
        }
    }
}
