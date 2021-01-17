using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] UnityEngine.UI.Text nicknameText;
    [SerializeField] float moveSpeed, jumpVelocity;
    [SerializeField] Vector2 groundedCheckSize;

    private float horizontal;
    private float vertical;

    private void Start()
    {
        nicknameText.text = photonView.Owner.NickName;
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (IsGrounded() && vertical > 0)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
            }
        }

        rigidbody.velocity = new Vector2(horizontal * moveSpeed, rigidbody.velocity.y);
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
        }
        else
        {
            horizontal = (float)stream.ReceiveNext();
            vertical = (float)stream.ReceiveNext();
        }
    }
}
