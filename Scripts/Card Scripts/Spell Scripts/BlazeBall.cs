using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeBall : Spell
{   
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", 1);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = EAnimStates.Idle;
    }
    
    void Start() => spellData = (SpellData)spellData.GetNewCardData();
}