using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;

    Animator anim;
    [SerializeField]
    MeleeWeapon meleeWeapon;
    private void Start()
    {
        lastComboEnd = Time.time;
        lastClickedTime = Time.time;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            Attack();
        }
        ExitAttack();
    }
    void Attack()
    {
        if (Time.time - lastComboEnd > 0.2f && comboCounter < combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                meleeWeapon.damage = combo[comboCounter].damage;
                meleeWeapon.EnableTriggerBox();
                //add VFX here
                comboCounter++;
                lastClickedTime = Time.time;

                if(comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }
    void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            meleeWeapon.DisableTriggerBox();
            Invoke("EndCombo", 1);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
