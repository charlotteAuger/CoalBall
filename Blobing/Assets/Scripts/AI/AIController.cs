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

    public void InitAITurn()
    {
        currentBall = PoolManager.instance.CreateBall(anchorPoint.position, false, 0).transform;
        rB2d = currentBall.GetComponent<Rigidbody2D>();
        currentStrategy = (PlayType)Random.Range(0, 3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }
    }

    public void Play()
    {
        InitAITurn();
        Vector3 t = ChooseTarget();
        print("target : " + t);
        Vector2 i = GetImpulseFromAim(t);
        print("impulse : " + i);
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
        print("distance : " + direction.magnitude);

        float factor = Mathf.Min(1,direction.magnitude / data.aimMaxDistance);
        print("factor : " + factor);

        impulse = direction.normalized * factor;

        return impulse;
    }

    private IEnumerator SlingshotAnimation(Vector2 impulse)
    {
        Vector2 pullVector = - impulse * data.maxDistance; print("pullVector : " + pullVector);
        Vector3 targetPosition = anchorPoint.position + new Vector3(pullVector.x, pullVector.y, 0); print("targetPullPosition : " + targetPosition);
        while (Vector3.Distance(targetPosition, currentBall.position) > 0.01f)
        {
            currentBall.position = Vector3.Lerp(currentBall.position, targetPosition, 0.2f);
            print(Vector3.Distance(targetPosition, currentBall.position));
            yield return new WaitForFixedUpdate();
        }

        print("endLoop");
        currentBall.position = targetPosition;
        LaunchBall(impulse);
    }

    private void LaunchBall(Vector2 _impulse)
    {
        Vector2 impulse = _impulse * data.maxForce;
        print("final impulse : " + impulse);
        rB2d.drag = data.ballLienarDrag;
        rB2d.AddForce(impulse, ForceMode2D.Impulse);
    }
}
