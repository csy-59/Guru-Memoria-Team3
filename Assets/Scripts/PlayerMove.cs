using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//���� �������� �ӵ��� ������ ���� �����ôٸ� rigid �κп��� gravity~ ���� ������ �ּ���
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed;
    SpriteRenderer sr;

    //gm
    GameManager manager;

    //���� ����
    public float jumpPower = 10;
    public int maxJump = 1;
    int jumpCount = 0;

    public int maxHp;
    public int currentHp;

    //�� ���� ����
    public float invincibilityTime = 2;

    //NPC ���ͷ��� ����
    NPCInteraction npc;
    public Text interText;
    bool textShowed = false;
    GameObject npcScan;

    //���� ����Ʈ ����
    public Vector2[] startingPoint;

    //���� �÷��̾�
   // AudioSource audioSrc_jump;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        npc = GetComponent<NPCInteraction>();
        manager = GameObject.FindObjectOfType<GameManager>();
        startingPoint[0] = gameObject.transform.position;

        interText.color = new Color(interText.color.r, interText.color.g, interText.color.b, 0f);

        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //���� ��ȯ
        if (Input.GetButton("Horizontal"))
        {
            //sr.flipX = Input.GetAxisRaw("Horizontal") == -1;
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        if (Input.GetButton("Jump") && jumpCount < maxJump)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            jumpCount++;

            //audioSrc_jump.Play();
        }

        if (Input.GetKeyDown(KeyCode.E) && npcScan != null)
        {
            manager.NPCText(npcScan);
        }
    }

    private void FixedUpdate()
    {
        
        //��Ʈ�ѿ� ���� �̵�
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);

        //###�����ɽ�Ʈ Ȯ��
        //���� �÷��� Ȯ��
        if (rigid.velocity.y < 0)
        {
            Debug.DrawLine(transform.position, new Vector3(0, 1, 0), new Color(0, 1, 0));
            RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 3, LayerMask.GetMask("Platform")); 

            if (rayHitDown.collider != null)
            {
                if (rayHitDown.distance < 3)
                {
                    jumpCount = 0;
                }
            }
        }

        //NPC Ȯ��
        Debug.DrawLine(transform.position, new Vector3(1, 0, 0), new Color(1, 0, 0));
        RaycastHit2D rayHitNPC = Physics2D.Raycast(rigid.position, Vector3.right, 1, LayerMask.GetMask("InteractiveObject"));

        if (rayHitNPC.collider != null)
        {
            npcScan = rayHitNPC.collider.gameObject;
        }
        else
        {
            npcScan = null;
        }

        //�������� �̵� Ȯ��
        Debug.DrawLine(transform.position, new Vector3(1, 0, 0), new Color(1, 0, 0));
        RaycastHit2D rayHitDoor = Physics2D.Raycast(rigid.position, Vector3.right, 1, LayerMask.GetMask("Door"));

        if (rayHitNPC.collider != null)
        {
            npcScan = rayHitNPC.collider.gameObject;
        }
        else
        {
            npcScan = null;
        }
    }

    //�����۰� �浹 ��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionedObject = collision.gameObject;

        if (collisionedObject.CompareTag("Item"))
        {
            collisionedObject.SetActive(false);
            switch (collisionedObject.layer)
            {
                case 15:
                    manager.Doll++;
                    print("Doll");
                    break;
                case 16:
                    manager.unicornHorns++;
                    print("Unicorn Horns");
                    break;
                /*case 17:
                    manager.candle++;
                    print("candle");
                    break;
                */
                case 18:
                    manager.medicine++;
                    print("medicine");
                    break;
            }
        }
        else if (collisionedObject.CompareTag("NPC"))
        {
            collisionedObject.SetActive(false);

            if (!textShowed)
            {
                textShowed = true;
                interText.color = new Color(interText.color.r, interText.color.g, interText.color.b, 1f);
                Invoke("startFade", 2);
            }
        }
        else if (collisionedObject.CompareTag("StageEndPoint"))
        {
            Attacked(1, collisionedObject.transform.position);
            gameObject.transform.position = startingPoint[manager.nowStage];
        }
    }

    public void Attacked(int damage, Vector2 targetPos)
    {
        gameObject.layer = 25;
        sr.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1), ForceMode2D.Impulse);

        currentHp -= damage;
        if(currentHp < 0)
        {
            currentHp = 0;
        }

        Invoke("offDamaged", invincibilityTime);
    }

    void offDamaged()
    {
        gameObject.layer = 6;
        sr.color = new Color(1, 1, 1, 1);
    }

    void startFade()
    {
        StartCoroutine(FadeTextToZero());
    }
    public IEnumerator FadeTextToZero()
    {
        interText.color = new Color(interText.color.r, interText.color.g, interText.color.b, 1);
        while (interText.color.a > 0.0f)
        {
            interText.color = new Color(interText.color.r, interText.color.g, interText.color.b, interText.color.a - (Time.deltaTime / 1));
            yield return null;
        }
    }
}
