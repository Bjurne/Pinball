using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    [SerializeField] bool isActive;
    [SerializeField] Transform objectContainer;

    private List<Transform> caughtObjects;
    private bool initialized = false;

    private void Start()
    {
        caughtObjects = new List<Transform>();
        ArenaControlBoard.Instance.OnBroadcastButtonPressed += SetActive;
        initialized = true;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (!isActive)
            return;
        if (otherCollider.tag == "Player")
        {
            otherCollider.transform.SetParent(objectContainer, true);
            otherCollider.attachedRigidbody.isKinematic = true;
            otherCollider.attachedRigidbody.velocity = Vector3.zero;
            //otherCollider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            caughtObjects.Add(otherCollider.transform);
            //otherCollider.enabled = false;
        }
    }

    private void SetActive(GameplayAction key)
    {
        //if (key == GameplayAction.ActivateMagneticField)
        //    isActive = true;
        //else if (key == GameplayAction.DeactivateMagneticField)

        if (key == GameplayAction.RightSpecial)
        {
            if (isActive)
                ReleaseAllCaughtObjects();
            isActive = !isActive;
        }
    }

    private void ReleaseAllCaughtObjects()
    {
        for (int i = 0; i < caughtObjects.Count; i++)
        {
            var item = caughtObjects[i];
            if (item != null)
            {
                var rb = item.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                //rb.constraints = RigidbodyConstraints.None;
                //item.GetComponent<Collider>().enabled = true;
                item.SetParent(Arena.Instance.CurrentlyActivePropsSet());
                item.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }

        caughtObjects.Clear();
    }

    private void OnEnable()
    {
        if (initialized)
            ArenaControlBoard.Instance.OnBroadcastButtonPressed += SetActive;
    }

    private void OnDisable()
    {
        ArenaControlBoard.Instance.OnBroadcastButtonPressed -= SetActive;
    }
}
