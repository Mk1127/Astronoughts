using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRock : MonoBehaviour
{
    [SerializeField] float animationStartDelay;
    [SerializeField] bool sinks;
    private Animator animator;
    bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        if(sinks)
        {
            StartCoroutine(ToggleAnimator());
        }

    }

    public void LowerRock()
    {
        /*if (sinks)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                animator.Play("Rock_Sink", 0, 0.0f);
                StartCoroutine(DisbleIsPlaying());
            }
        }*/
    }

    IEnumerator DisbleIsPlaying()
    {
        yield return new WaitForSeconds(3.0f);
        isPlaying = false;
    }

    IEnumerator ToggleAnimator()
    {
        animator.enabled = false;
        yield return new WaitForSeconds(animationStartDelay);
        animator.enabled = true;
    }
}
