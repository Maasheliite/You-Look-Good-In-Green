using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Player.position.x, Player.position.y, this.transform.position.z);
    }
}
