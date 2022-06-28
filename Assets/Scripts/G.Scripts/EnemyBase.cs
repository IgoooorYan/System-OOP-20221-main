using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyBase : PhysicsController , IPoolable
{
    public float spawnCD { get; protected set; }
    public string name { get; protected set; }
    protected int maxHP 
    {
        get
        {
            return _maxHP;
        }
    }
    public int _maxHP;
    protected int HP
    {
        get
        {
            return _HP;
        }
        set { }
    }
    public int _HP;


    public float speed { get; protected set;}
    public float damage { get;  protected set; }
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected Transform pTrans;
    public EnemyDTO edto;

    protected Vector2 initialPos;
    protected override void Awake()
    {
        base.Awake();
        spawnCD = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
        pTrans = player.GetComponent<Transform>();
        InitDto(edto);
        gameObject.tag = "Enemy";
        rb.freezeRotation = true;
    }
    private void Start()
    {
        
    }

    protected virtual void LateUpdate()
    {
        FollowPlayer();
    }
    public void TakeDamage (int pDamage)
    {
        //recebe o damage do player e desconta do HP
        _HP =- pDamage;
    }

    public void DoDamage()
    {
        //pega o script do player e passa o dano como par�metro o damage como dano no jogador
    }
    public void FollowPlayer()
    {
        //Vector3 posFinal =  pTrans.position - tf.position ;
        //rb.MovePosition(tf.position + (posFinal.normalized*speed *Time.deltaTime));
        //faz a diferen�a entre a posi��o do inimigo e do player, e passa a posi��o atual do inimigo, com uma 
        //soma da diferen�a de posi��es para seguir o player. Precisa do time.deltatime sen�o buga. 
    }
    //nota pra mim mesmo - Como usar DTO: criar um DTO no c�digo, criar vari�veis dentro dentro, e criar um m�todo
    //inicializante pro dto, onde vamos passar um dto como par�metro, e as vari�veis que nele est�o ser�o passadas para
    //as vari�veis do c�digo. Iniciar ele no m�todo Awake, passando como par�metro a refer�ncia de dto criada.
    public virtual void InitDto(EnemyDTO dto)
    {
        this.name = dto.name;
        this.HP = dto._HP;
        this._maxHP = dto._maxHP;
        this.speed = dto.speed;
        this.damage = dto.damage;
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public void Recycle()
    {
        gameObject.SetActive(false);
    }

    protected void SendToPool()
    {
        //Factory.Instance.ReturnObject(FactoryItem.SquareEnemy, this);
    }

    //protected void init(float speed)
    //{
    //    this.speed=speed;

    //    initialPos = tf.position;
    //}

    public void init(Vector2 position, Quaternion rotation)
    {
        tf.position = position;
        tf.rotation = rotation;
        //initialPos = tf.position;
        //init(speed);
    }
    
     private void Death()
     {
         if(_HP <= 0)
         {
            SendToPool();
         }
     }
     
}
