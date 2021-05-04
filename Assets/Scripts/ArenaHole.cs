using UnityEngine;
using System.Collections;

public class ArenaHole : MonoBehaviour
{
    [SerializeField] ArenaHoleType holeType;
    private Collider arenaFloorCollider;
    internal ArenaHoleType HoleType { set => holeType = value; }

    private void Start()
    {
        arenaFloorCollider = GameObject.Find("Arena Floor").GetComponent<Collider>(); // TODO:  cheating
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            Physics.IgnoreCollision(otherCollider, arenaFloorCollider, true);
        }
    }

    void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            Destroy(otherCollider.gameObject); // TODO: Pool

            Arena.Instance.BallEnteredHole(holeType);
        }

    }

}


public enum ArenaHoleType
{
    BackHole = 0,
    EndlessDebugHole = 1,
    GoalHole = 3,
}