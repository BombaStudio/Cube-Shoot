using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationMenager : MonoBehaviour
{
    Animator animator;

    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatiorValues(float h, float v)
    {
        float snappedHorizontal;
        float snappedVertical;

        if (h > 0 && h < 0.55f) snappedHorizontal = 0.55f;
        else if (h > 0.55f) snappedHorizontal = 1;
        else if (h < 0 && h > -0.55f) snappedHorizontal = -0.55f;
        else if (h < - 0.55f) snappedHorizontal = -1;
        else snappedHorizontal = 0;
        
        if (v > 0 && v < 0.55f) snappedVertical = 0.55f;
        else if (v > 0.55f) snappedVertical = 1;
        else if (v < 0 && v > -0.55f) snappedVertical = -0.55f;
        else if (v < - 0.55f) snappedVertical = -1;
        else snappedVertical = 0;

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
