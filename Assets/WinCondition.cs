using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
	[SerializeField] private GameObject m_winConditionUI;
	[SerializeField] private Text m_playerName;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Working");

		if(PhotonNetwork.IsMasterClient)
		{
			Debug.Log("ISMASTER");
			m_playerName.text = other.GetComponent<PlayerController>().NickName.text + " won!!";
			m_winConditionUI.SetActive(true);
		}
	}
}
