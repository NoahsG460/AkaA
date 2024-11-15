using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float knockbackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;
    public AnimationCurve knockbackCurve;

    private Rigidbody2D rb;

    private Coroutine knockbackCoroutine;

    public bool IsBeingKnockedBack { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator KnockbackAction(Vector2 hitDirecton, Vector2 constantForceDirection, float inputDirection)
    {
        IsBeingKnockedBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;

        _hitForce = hitDirecton * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;

        float _elapsedTime = 0f;
        while (_elapsedTime < knockbackTime)
        {
            _elapsedTime += Time.fixedDeltaTime;

            _knockbackForce = _hitForce + _constantForce;

            if (inputDirection != 0)
            {
                _combinedForce = _knockbackForce + new Vector2(inputDirection, 0f);
            }
            else
            {
                _combinedForce = _knockbackForce;
            }

            rb.velocity = _combinedForce;

            yield return new WaitForFixedUpdate();
        }

        IsBeingKnockedBack = false;
    }

    public void CallKnockback(Vector2 hitDirecton, Vector2 constantForceDirection, float inputDirection)
    {
        knockbackCoroutine = StartCoroutine(KnockbackAction(hitDirecton, constantForceDirection, inputDirection));
    }
}

