using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject Magic;
    public Transform pos;
    public float shootSpeed;
    float curTime;

    GameManager gm;
    MainMenu mm;

    // Start is called before the first frame update
    void Start()
    {
        mm = GameObject.FindObjectOfType<MainMenu>();
        gm = GameObject.FindObjectOfType<GameManager>();
        curTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curTime += Time.fixedDeltaTime;
        if(curTime >= shootSpeed)
        {
            if (Input.GetKey(KeyCode.Mouse0) && mm.isGameStart && !gm.isPauseOn && !gm.isDeath)
            {
                Instantiate(Magic, pos.position, transform.rotation);
                curTime = 0;
            }
        }
    }
}
