/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUMera : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 2f);
    public bool is_left;
    //private Gay TransformPlayer;
    private int last_X;


    void Start()
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
    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player)
        {
            int curent_X = Mathf.RoundToInt(player.position.x);

            if(curent_X > last_X)is_left = false;
            else if(curent_X < last_X)is_left = true;
            Vector2 target;
            if(is_left)
            {
                target = new Vector2(player.position.x - offset.x, player.position.y - offset.y);
            }
            else
            {
                target = new Vector2(player.position.x + offset.x, player.position.y + offset.y);
            }
            Vector3 curent_p = new Vector3(target.x, target.y, transform.position.z);
            transform.position = curent_p;
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera.Scripts
{
    public class CUMera:MonoBehaviour 
    {
    [Header("Parameters")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private string playerTag;
    [SerializeField] [Range(0.5f, 7.5f)] private float movingSpeed = 1.5f;
    [SerializeField] private float x_min = 0;
    [SerializeField] private float x_max = 20;
    [SerializeField] private float y_freeze = 0;

        private void Awake()
    {
        if (this.playerTransform == null)
        {
        if (this.playerTag == "")
        {
        this.playerTag = "Player";
        }

        this.playerTransform = GameObject.FindGameObjectWithTag(this.playerTag).transform;
        }

        this.transform.position = new Vector3()
        {
        x=this.playerTransform.position.x, y=y_freeze, z=this.playerTransform.position.z};
        }

        private void Update()
        {
        if(this.playerTransform)
        {
        Vector3 target = new Vector3() 
        {
        x=this.playerTransform.position.x, y=y_freeze, z=this.playerTransform.position.z-10};
        
        Vector3 pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);

        this.transform.position = pos;
        if (transform.position.x < x_min) {
            transform.position = new Vector3(x_min, y_freeze, transform.position.z);
        }
        else if (transform.position.x > x_max) {
            transform.position = new Vector3(x_max, y_freeze, transform.position.z);
        }
        }
        }
    }
    }