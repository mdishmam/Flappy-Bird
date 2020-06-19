using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;


public class Bird : MonoBehaviour
{

    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;
    public Sprite birdCustomFlying;
    public Sprite birdNatural;

    private static Bird instance;
    private State state;


    public static Bird GetInstance()
    {
        return instance;
    }


    private const float JUMP_AMMOUNT = 100f;

    private Rigidbody2D birdrigidbody2D;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    private void Awake()
    {
        instance = this;
        birdrigidbody2D = GetComponent<Rigidbody2D>();
        birdrigidbody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = birdNatural;

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    state = State.Playing;
                    birdrigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    //Fly();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                    Fly();
                }
                break;
            case State.Dead:
                break;
        }

        transform.eulerAngles = new Vector3(0, 0, birdrigidbody2D.velocity.y * .15f);
        


    }



    private void Jump()
    {
        birdrigidbody2D.velocity = Vector2.up * JUMP_AMMOUNT;

        

    }

    void Fly()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == birdNatural)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = birdCustomFlying;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = birdNatural;
        }
    }

    void FlyAuto()
    {
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = birdCustomFlying;
        //StartCoroutine(ExecuteAfter(1f, () =>
        //{
        //    this.gameObject.GetComponent<SpriteRenderer>().sprite = birdCustomFlying;
        //}));
        //StartCoroutine(ExecuteAfter(1f, () =>
        //{
        //    this.gameObject.GetComponent<SpriteRenderer>().sprite = birdNatural;
        //}));
        ExecuteAfter(1f, () =>
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = birdCustomFlying;
        });
         ExecuteAfter(1f, () =>
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = birdNatural;
        });

    }

    private bool isCoroutineExecuting = false;
    IEnumerator ExecuteAfter(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        task();

        isCoroutineExecuting = false;

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
      //  CMDebug.TextPopupMouse("Dead!");
        birdrigidbody2D.bodyType = RigidbodyType2D.Static;
        if (OnDied != null) OnDied(this, EventArgs.Empty);
    }
}
