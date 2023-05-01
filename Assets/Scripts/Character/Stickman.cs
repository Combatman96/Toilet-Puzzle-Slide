using UnityEngine;
using DG.Tweening;

public class Stickman : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] Transform m_animationRoot;
    [SerializeField] Rigidbody2D rb;
    public static readonly int s_idle = Animator.StringToHash("Idle");
    public static readonly int s_run = Animator.StringToHash("Run");
    public static readonly int s_happy = Animator.StringToHash("Happy");
    public static readonly int s_noPaper = Animator.StringToHash("No_Paper");

    public void SetState(int state)
    {
        PlayAnimation(state);
    }

    private void PlayAnimation(int state) 
    {
        m_animator.CrossFade(state, 0.3f, 0);
    }


    [SerializeField] PathType m_pathType;
    [SerializeField] PathMode m_pathMode;

    public void MoveAlongPath(Vector2[] path, float duration) 
    {
        rb.DOPath(path, duration, m_pathType, m_pathMode, 10, Color.green).OnComplete(()=>
        {
            //TODO: Call action when character reach the goal
        });
    }
}
