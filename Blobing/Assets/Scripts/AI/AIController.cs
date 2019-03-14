using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayType { MaxBalls, BiggestBall, Sniper}

public class AIController : MonoBehaviour
{
    private Transform currentBall;
    private PlayType currentStrategy;
    private Rigidbody2D rB2d;
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private SlingshotData data;

    public Transform debugTarget;

    public void InitAIGame()
    {
        currentStrategy = (PlayType)Random.Range(0, 3);
    }

    public void InitAITurn()
    {
        currentBall = PoolManager.instance.CreateBall(anchorPoint.position, false, 0).transform;
        rB2d = currentBall.GetComponent<Rigidbody2D>();  
    }

    public void Play()
    {
        Vector3 t = ChooseTarget();
        Vector2 i = GetImpulseFromAim(t);
        StartCoroutine(SlingshotAnimation(i));
    }

    private Vector3 ChooseTarget()
    {
        return debugTarget.position;
    }

    private void ChooseStrategy()
    {
        switch (currentStrategy)
        {
            case (PlayType.MaxBalls):
                break;

            case (PlayType.BiggestBall):
                break;

            case (PlayType.Sniper):
                break;
        }
    }

    private Vector3 FindEmptyPosition()
    {
        return Vector3.zero;
    }

    private Vector2 GetImpulseFromAim(Vector3 target)
    {
        Vector2 impulse = Vector2.zero;
        Vector2 direction = target - anchorPoint.position;

        float factor = Mathf.Min(1,direction.magnitude / data.aimMaxDistance);

        impulse = direction.normalized * factor;

        return impulse;
    }

    private IEnumerator SlingshotAnimation(Vector2 impulse)
    {
        Vector2 pullVector = - impulse * data.maxDistance;
        Vector3 targetPosition = anchorPoint.position + new Vector3(pullVector.x, pullVector.y, 0);
        while (Vector3.Distance(targetPosition, currentBall.position) > 0.01f)
        {
            currentBall.position = Vector3.Lerp(currentBall.position, targetPosition, 0.2f);
            yield return new WaitForFixedUpdate();
        }

        currentBall.position = targetPosition;
        LaunchBall(impulse);
        StartCoroutine(TurnManager.Instance.EndTurn());
    }

    private void LaunchBall(Vector2 _impulse)
    {
        Vector2 impulse = _impulse * data.maxForce;

        rB2d.drag = data.ballLinearDrag;
        rB2d.AddForce(impulse, ForceMode2D.Impulse);
    }
}
