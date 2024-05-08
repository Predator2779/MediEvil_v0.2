using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationSetter : MonoBehaviour
{
    [SerializeField] private AnimationClip[] _clips;
    
    private void Start()
    {
        var clip = Random.Range(0, _clips.Length);
        GetComponent<Animator>().CrossFade(_clips[clip].name, 0);
    }
}
