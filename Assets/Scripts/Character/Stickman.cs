using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Stickman : MonoBehaviour
{
    private Gameplay m_gameplay => FindObjectOfType<Gameplay>();
    [SerializeField] private Animator m_animator;
    [SerializeField] Transform m_animationRoot;
    [SerializeField] Rigidbody2D rb;
    public static readonly int s_idle = Animator.StringToHash("Idle");
    public static readonly int s_run = Animator.StringToHash("Run");
    public static readonly int s_happy = Animator.StringToHash("Happy");
    public static readonly int s_noPaper = Animator.StringToHash("No_Paper");
    [SerializeField] private Transform m_guidePoint;
    [SerializeField] private Rigidbody2D m_guideRb;
    public void SetState(int state)
    {
        PlayAnimation(state);
    }

    private void PlayAnimation(int state)
    {
        m_animator.CrossFade(state, 0.02f, 0);
    }


    [SerializeField] PathType m_pathType;
    [SerializeField] PathMode m_pathMode;

    private bool m_isRunning = false;

    public void MoveAlongPath(Vector2[] path, float duration)
    {
        m_isRunning = true;
        var seq = DOTween.Sequence();
        m_guidePoint.SetParent(null);
        seq.Insert(0f, m_guideRb.DOPath(path, duration, m_pathType, m_pathMode, 10, Color.green));
        seq.Insert(0.1f, rb.DOPath(path, duration, m_pathType, m_pathMode, 10, Color.green));
        seq.OnComplete(() =>
        {
            //TODO: Call action when character reach the goal
            m_isRunning = false;
            m_gameplay.OnReachGoal();
            m_guidePoint.SetParent(transform);
            m_animationRoot.localRotation = Quaternion.Euler(0f, 0f, 0f);
        });
    }

    private void Update()
    {
        if(m_isRunning)
        {
            Vector3 dir = m_guidePoint.position - transform.position;
            float facing = (dir.x > 0) ? 0f : 180f;
            m_animationRoot.localRotation = Quaternion.Euler(0f, facing, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        m_gameplay.hasPaper = true;
        other.gameObject.SetActive(false);
    }
}
