using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRock : MonoBehaviour
{
    [SerializeField] bool sinks;
    private Animator animator;
    bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void LowerRock()
    {
        if (sinks)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                animator.Play("Lava Rock", 0, 0.0f);
                StartCoroutine(DisbleIsPlaying());
            }
        }
    }

    IEnumerator DisbleIsPlaying()
    {
        yield return new WaitForSeconds(3.0f);
        isPlaying = false;
    }
}
