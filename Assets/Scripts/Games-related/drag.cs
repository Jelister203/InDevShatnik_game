using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Camera.Scripts{
public class drag : MonoBehaviour
{
    private CameraType cam;
    public void setPosition(Transform obj) {
        Vector2 curs = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(curs.ToString());
        obj.position = new Vector2(curs.x-0.75f, curs.y+0.5f);
    }
}
}