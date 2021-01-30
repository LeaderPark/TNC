using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private LayerMask whatIsGround;

    [Header("�÷��̾� �̵� ����")]
    public float move = 1f;
    public float jump = 1f;
    public float claim = 1f;
    public float damage = 1f;
    public float currentHP = 100f;
    public float maxHP = 100f;
    public float unBeatTime = 2f;


    [Header("Ÿ�� üũ")]
    public bool isladder = false;
    public bool isjumping = false;
    public bool isground = false;
    public bool iswall = false;
    public bool isTeleport = false;
    public bool isTeleport1 = false;
    public bool isWater = false;
    public bool isTouch = false;
    public bool isladderBlock = false;

    [Header("���ⱸ ���� ��ġ")]
    public float teleportX = 0.7f;
    public float teleportY = 0.6f;

    public float teleport1X = -1.1f;
    public float teleport1Y = -0.15f;

    [Header("GameObject")]
    public GameObject blurUI1;
    public GameObject blurUI2;
    public GameObject teleport1;
    public GameObject teleport2;
    private GameObject touch;
    private GameObject lb;

    public Slider hpslider;

    Vector3 telpos0 = Vector3.zero; // ���ⱸ ��ġ
    Vector3 telpos1 = Vector3.zero;
    public bool iswall1 = false;
    public bool iswall2 = false;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D coll;
    
    //�߰�
    [Header("Sound")]
    public AudioClip useSound = null;
    public AudioClip gameoverSound = null;
    public AudioClip jumpSound = null;
    public AudioClip walkSound = null;
    public AudioClip waterDamageSound = null;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        
        telpos0 = GameObject.Find("Teleport").transform.position;
        telpos1 = GameObject.Find("Teleport (1)").transform.position;
        // ���ⱸ ��ġ ã��

        touch = GameObject.Find("Touch");
        lb = GameObject.Find("ladderBlock");

        //�߰�
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col) //is trigger�϶� üũ�Ǿ� �ڵ� �����
    {
        if (col.gameObject.tag == "ladder") //ladder��� �±��� object�� ������� ����
        {
            isladder = true;
            Debug.Log("��ٸ�in");

        }
        if (col.gameObject.tag == "Wall") //Wall �̶�� �±��� object�� ������� ����
        {
            iswall = true;
            Debug.Log("��in");
        }
        if (col.gameObject.tag == "Teleport") //Teleport �̶�� �±��� object�� ������� ����
        {
            isTeleport = true;
            Debug.Log("���ⱸin");
        }
        if (col.gameObject.tag == "Teleport1") //Teleport1 �̶�� �±��� object�� ������� ����
        {
            isTeleport1 = true;
            Debug.Log("���ⱸ1in");
        }
        if (col.gameObject.tag == "Touch") //Touch �̶�� �±��� object�� ������� ����
        {
            isTouch = true;
            Debug.Log("Touch in");
        }
        if (col.gameObject.tag == "ladderBlock") //Touch �̶�� �±��� object�� ������� ����
        {
            isladderBlock = true;
            Debug.Log("ladderBlock  in");
        }
    }

    private void OnTriggerExit2D(Collider2D col) //object���� ���������� üũ
    {
        if (col.gameObject.tag == "ladder") //ladder �̶�� �±��� object�� ������� ����
        {
            isladder = false;
            Debug.Log("��ٸ�out");
        }
        if (col.gameObject.tag == "Wall") //Wall �̶�� �±��� object�� ������� ����
        {
            iswall = false;
            Debug.Log("��out");
        }
        if (col.gameObject.tag == "Teleport") //Teleport �̶�� �±��� object�� ������� ����
        {
            isTeleport = false;
            Debug.Log("���ⱸout");
        }
        if (col.gameObject.tag == "Teleport1") //Teleport1 �̶�� �±��� object�� ������� ����
        {
            isTeleport1 = false;
            Debug.Log("���ⱸ1out");
        }
        if (col.gameObject.tag == "Touch") //Touch �̶�� �±��� object�� ������� ����
        {
            isTouch = false;
            Debug.Log("Touch out");
        }
        if (col.gameObject.tag == "ladderBlock") //Touch �̶�� �±��� object�� ������� ����
        {
            isladderBlock = false;
            Debug.Log("ladderBlock  out");
        }
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Water")
        {
            OnDamaged(col.transform.position);
            Debug.Log("��in");
        }
    }


    void OnDamaged(Vector2 targetPos)
    {
        //�߰�
        PlaySound("waterdamage");

        currentHP -= 10f;
        hpslider.GetComponent<HPSlider>().sethp(10f);
        gameObject.layer = 10;

        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc*40, 0.1f), ForceMode2D.Impulse);

        Invoke("OffDamaged", unBeatTime);
    }

    void OffDamaged()
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void GroundCheck()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.01f, whatIsGround))
        {
            isground = true;
        }
        else
        {
            isground = false;
        }
    }
    //�߰�
    public void PlaySound(string idle)
    {
        switch(idle)
        {
            case "use":
                    audioSource.clip = useSound;
                break;

            case "gameover":
                    audioSource.clip = gameoverSound;
                break;

            case "jump":
                    audioSource.clip = jumpSound;
                break;

            case "walk":
                if (audioSource.isPlaying == false)
                {
                    audioSource.clip = walkSound;
                }
                break;

            case "waterdamage":
                    audioSource.clip = waterDamageSound;
                break;
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        GroundCheck();

        if(isTouch)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //�߰�
                PlaySound("use");

                blurUI1.gameObject.SetActive(false);
                blurUI2.gameObject.SetActive(true);
                currentHP -= 10;
                hpslider.GetComponent<HPSlider>().sethp(10f);
                touch.gameObject.tag = "TouchOn";
                isTouch = false;
            }
        }

        if (iswall)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(GameObject.Find("Wall")); //Wall �̶�� �±��� object�� ����
                Debug.Log("������");
                currentHP -= 5; //HP -5
                hpslider.GetComponent<HPSlider>().sethp(5f);
            }
        }

        if (isTeleport)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (currentHP <= 15)
                {
                    Camera.main.GetComponent < CameraShake > ().ShakeCamera(0.02f);
                }
                else
                {
                    //�߰�
                    PlaySound("use");

                    gameObject.transform.position = telpos1; //����
                    teleport1.gameObject.SetActive(false);
                    teleport2.gameObject.SetActive(true);
                    currentHP -= 15; //HP -15
                    hpslider.GetComponent<HPSlider>().sethp(15f);
                }
            }
        }

        if (isTeleport1)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (currentHP <= 15)
                {
                    Camera.main.GetComponent<CameraShake>().ShakeCamera(0.02f);
                }
                else
                {
                    //�߰�
                    PlaySound("use");

                    gameObject.transform.position = telpos0; //����
                    teleport1.gameObject.SetActive(true);
                    teleport2.gameObject.SetActive(false);
                    currentHP -= 15; //HP -15
                    hpslider.GetComponent<HPSlider>().sethp(15f);
                }
            }
        }

        if (isground) //Ground�� in ������ 
        {
            if (Input.GetKeyDown(KeyCode.Space)) //ȭ��ǥ space�� ������ 
            {
                //�߰�
                PlaySound("jump");

                rigid.AddForce(Vector2.up * jump* 20, ForceMode2D.Impulse);
            }
        }


        if (isladder)//flag�� in������
        {
            if (Input.GetKey(KeyCode.UpArrow)) //����� ���� ������ 
            {
                rigid.gravityScale = 0; //�߷��� 0���� �ϰ�
                transform.Translate(Vector2.up * claim * Time.deltaTime); //�� �������� �̵�
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigid.gravityScale = 0; //�߷��� 0���� �ϰ� 
                transform.Translate(Vector2.down * claim * Time.deltaTime); //�Ʒ� �������� �̵�
            }
        }
        else
        {
            rigid.gravityScale = 4f;
        }
 
        if (Input.GetKey(KeyCode.LeftArrow))    //����ȭ��ǥ �Է½� ������
        {
            //�߰�
            //���������
            PlaySound("walk");

            spriteRenderer.flipX = true;
            anim.Play("run");

            transform.Translate(Vector2.left * move * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.Play("idle");
        }

        if (Input.GetKey(KeyCode.RightArrow))    //������ȭ��ǥ �Է½� ������
        {
            //�߰�
            //���������
            PlaySound("walk");

            spriteRenderer.flipX = false;
            anim.Play("run");

            transform.Translate(Vector2.right * move * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.Play("idle");
        }

        if (isladderBlock)
        { 
            if(Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.S))
            {
                lb.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.S))
            {
                lb.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.Play("Crouch");
            coll.size = new Vector2(1.6f, 2f);
            coll.offset = new Vector2(0f, 1.08f);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.Play("idle");
            coll.size = new Vector2(1.6f, 2.9f);
            coll.offset = new Vector2(0f, 1.5f);
        }
    }
}
