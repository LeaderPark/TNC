using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private LayerMask whatIsGround;

    [Header("플레이어 이동 스탯")]
    public float move = 1f;
    public float jump = 1f;
    public float claim = 1f;
    public float damage = 1f;
    public float currentHP = 100f;
    public float maxHP = 100f;
    public float unBeatTime = 2f;


    [Header("타일 체크")]
    public bool isladder = false;
    public bool isjumping = false;
    public bool isground = false;
    public bool iswall = false;
    public bool isTeleport = false;
    public bool isTeleport1 = false;
    public bool isWater = false;
    public bool isTouch = false;
    public bool isladderBlock = false;

    [Header("열기구 텔포 위치")]
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

    Vector3 telpos0 = Vector3.zero; // 열기구 위치
    Vector3 telpos1 = Vector3.zero;
    public bool iswall1 = false;
    public bool iswall2 = false;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D coll;
    
    //추가
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
        // 열기구 위치 찾기

        touch = GameObject.Find("Touch");
        lb = GameObject.Find("ladderBlock");

        //추가
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col) //is trigger일때 체크되어 코드 실행법
    {
        if (col.gameObject.tag == "ladder") //ladder라는 태그의 object에 닿았을때 실행
        {
            isladder = true;
            Debug.Log("사다리in");

        }
        if (col.gameObject.tag == "Wall") //Wall 이라는 태그의 object에 닿았을때 실행
        {
            iswall = true;
            Debug.Log("벽in");
        }
        if (col.gameObject.tag == "Teleport") //Teleport 이라는 태그의 object에 닿았을때 실행
        {
            isTeleport = true;
            Debug.Log("열기구in");
        }
        if (col.gameObject.tag == "Teleport1") //Teleport1 이라는 태그의 object에 닿았을때 실행
        {
            isTeleport1 = true;
            Debug.Log("열기구1in");
        }
        if (col.gameObject.tag == "Touch") //Touch 이라는 태그의 object에 닿았을때 실행
        {
            isTouch = true;
            Debug.Log("Touch in");
        }
        if (col.gameObject.tag == "ladderBlock") //Touch 이라는 태그의 object에 닿았을때 실행
        {
            isladderBlock = true;
            Debug.Log("ladderBlock  in");
        }
    }

    private void OnTriggerExit2D(Collider2D col) //object에서 나왔을때를 체크
    {
        if (col.gameObject.tag == "ladder") //ladder 이라는 태그의 object에 닿았을때 실행
        {
            isladder = false;
            Debug.Log("사다리out");
        }
        if (col.gameObject.tag == "Wall") //Wall 이라는 태그의 object에 닿았을때 실행
        {
            iswall = false;
            Debug.Log("벽out");
        }
        if (col.gameObject.tag == "Teleport") //Teleport 이라는 태그의 object에 닿았을때 실행
        {
            isTeleport = false;
            Debug.Log("열기구out");
        }
        if (col.gameObject.tag == "Teleport1") //Teleport1 이라는 태그의 object에 닿았을때 실행
        {
            isTeleport1 = false;
            Debug.Log("열기구1out");
        }
        if (col.gameObject.tag == "Touch") //Touch 이라는 태그의 object에 닿았을때 실행
        {
            isTouch = false;
            Debug.Log("Touch out");
        }
        if (col.gameObject.tag == "ladderBlock") //Touch 이라는 태그의 object에 닿았을때 실행
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
            Debug.Log("물in");
        }
    }


    void OnDamaged(Vector2 targetPos)
    {
        //추가
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
    //추가
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
                //추가
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
                Destroy(GameObject.Find("Wall")); //Wall 이라는 태그의 object를 제거
                Debug.Log("벽제거");
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
                    //추가
                    PlaySound("use");

                    gameObject.transform.position = telpos1; //텔포
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
                    //추가
                    PlaySound("use");

                    gameObject.transform.position = telpos0; //텔포
                    teleport1.gameObject.SetActive(true);
                    teleport2.gameObject.SetActive(false);
                    currentHP -= 15; //HP -15
                    hpslider.GetComponent<HPSlider>().sethp(15f);
                }
            }
        }

        if (isground) //Ground에 in 했을때 
        {
            if (Input.GetKeyDown(KeyCode.Space)) //화살표 space를 누를때 
            {
                //추가
                PlaySound("jump");

                rigid.AddForce(Vector2.up * jump* 20, ForceMode2D.Impulse);
            }
        }


        if (isladder)//flag에 in했을때
        {
            if (Input.GetKey(KeyCode.UpArrow)) //방향기 위를 누를때 
            {
                rigid.gravityScale = 0; //중력을 0으로 하고
                transform.Translate(Vector2.up * claim * Time.deltaTime); //위 방향으로 이동
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigid.gravityScale = 0; //중력을 0으로 하고 
                transform.Translate(Vector2.down * claim * Time.deltaTime); //아래 방향으로 이동
            }
        }
        else
        {
            rigid.gravityScale = 4f;
        }
 
        if (Input.GetKey(KeyCode.LeftArrow))    //왼쪽화살표 입력시 실행함
        {
            //추가
            //넣으면망함
            PlaySound("walk");

            spriteRenderer.flipX = true;
            anim.Play("run");

            transform.Translate(Vector2.left * move * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.Play("idle");
        }

        if (Input.GetKey(KeyCode.RightArrow))    //오른쪽화살표 입력시 실행함
        {
            //추가
            //넣으면망함
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
