using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mergeable : MonoBehaviour {

    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float scaleAmplitude;
    [SerializeField] private float animationTime;
    [SerializeField] private GameObject sprite;
    public Rigidbody2D rB2d;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Mergeable other = collision.gameObject.GetComponent<Mergeable>();

        if (other != null)
        {
            if (other.rB2d.velocity.magnitude < rB2d.velocity.magnitude)
            {
                Absorb();
                other.DestroyBall();
            }
        }
    }

    private IEnumerator ScaleWobble(float targetScale)
    {
        yield return null;
    }

    private void Absorb()
    {

    }

    public void DestroyBall()
    {

    }
}
