using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] ParticleSystem jumpParticles;

    [SerializeField] UnityEngine.UI.Text nicknameText;
    [SerializeField] float moveSpeed, jumpVelocity, boostForce;
    [SerializeField] Vector2 groundedCheckSize;
    [SerializeField] int jumpParticlesToEmit;


    private float horizontal;
    private float vertical;
    bool isBoosting;

    private void Start()
    {
        nicknameText.text = photonView.Owner.NickName;
        SetLocalColors();
    }

    private void SetLocalColors()
    {
        Color color = GetColorForPlayerById(photonView.OwnerActorNr);
        spriteRenderer.color = color;
        nicknameText.color = color;

        var main = jumpParticles.main;
        main.startColor = color;

        trailRenderer.startColor = color;
        color.a = 0;
        trailRenderer.endColor = color;
    }

    private Color GetColorForPlayerById(int id)
    {
        return Color.HSVToRGB((float)id / 10f, 1, 1);
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            var oldVertical = vertical;
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (IsGrounded() && vertical > 0)
            {
                Jump();
            }

            if (oldVertical <= 0 && vertical > 0)
            {
                StartBoosting();
            }
            else if (vertical <= 0 && oldVertical > 0)
            {
                StopBoosting();
            }
        }

        rigidbody.velocity = new Vector2(horizontal * moveSpeed, rigidbody.velocity.y);
        if (isBoosting)
        {
            rigidbody.AddForce(Vector2.up * boostForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void StartBoosting()
    {
        isBoosting = true;
        var emission = jumpParticles.emission;
        emission.rateOverTimeMultiplier = jumpParticlesToEmit;
    }

    private void StopBoosting()
    {
        isBoosting = false;
        var emission = jumpParticles.emission;
        emission.rateOverTimeMultiplier = 0;
    }

    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
    }

    private bool IsGrounded()
    {
        var hits = Physics2D.BoxCastAll(transform.position, groundedCheckSize, 0, Vector2.zero, 0);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, groundedCheckSize);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(horizontal);
            stream.SendNext(vertical);
            stream.SendNext(isBoosting);
        }
        else
        {
            horizontal = (float)stream.ReceiveNext();
            vertical = (float)stream.ReceiveNext();
            isBoosting = (bool)stream.ReceiveNext();
        }
    }
}
