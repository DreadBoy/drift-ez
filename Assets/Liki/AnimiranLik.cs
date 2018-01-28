using System.Collections;
using UnityEngine;

public class AnimiranLik : MonoBehaviour
{
    public Animator animator;
    public Material[] materials;

    void Start()
    {
        int index = Random.Range(0, animator.runtimeAnimatorController.animationClips.Length);
        StartCoroutine(SetfterDelay(index, Random.Range(0, 2f)));

        if (materials.Length > 1)
            GetComponentInChildren<SkinnedMeshRenderer>().material = materials[Random.Range(0, materials.Length)];
    }

    IEnumerator SetfterDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetInteger("State", index);
    }
}
