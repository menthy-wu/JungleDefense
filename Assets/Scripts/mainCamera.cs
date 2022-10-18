using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shake()
    {
        animator.SetBool("Shake", true);
        Invoke("stopShake", .5f);
    }
    private void stopShake()
    {
        animator.SetBool("Shake", false);
    }
    public void Pop()
    {
        animator.SetBool("Pop", true);
        Invoke("stopPop", .5f);
    }
    private void stopPop()
    {
        animator.SetBool("Pop", false);
    }
}
