using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private GameObject sprite;
    [SerializeField] private AudioSource sfx;
    
    private CharacterController _controller;
    private SpriteRenderer _renderer;
    private Vector3 _move = new Vector3();

    private bool _dead = false;

    void Awake(){
        _controller = GetComponent<CharacterController>();
        _renderer = sprite.GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {

        if (_dead) return;

        _move.x = Input.GetAxis("Horizontal");
        _move.z = Input.GetAxis("Vertical");

        _move.y += gravity * Time.deltaTime;

        // if(_controller.isGrounded && _move.y < 0f){
        //     _move.y = 0f;
        // }

        if(_controller.isGrounded && Input.GetButtonDown("Jump")){
            _move.y = jumpForce;
        }

        if(Mathf.Abs(_move.x) > 0){
            _renderer.flipX = _move.x < 0;
        }

        _controller.Move(_move * speed * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit){
        if (_dead) return;

        if(hit.gameObject.CompareTag("Agua")){
            Debug.Log("Me ahogué");
            sfx.Play();
            _dead = true;
        }
    }
}
