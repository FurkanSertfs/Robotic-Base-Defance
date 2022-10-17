using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;

    public List<GameObject> enemies = new List<GameObject>();

    private Animator animator;

    [SerializeField]
    float fireSpeed;

    [SerializeField]
    Transform firePoint;
    [SerializeField]
    GameObject pikAxe;

    Mine _mine;

    public GameObject injuredSoldierParent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = this;
    }

    public void Mining()
    {
        _mine.Dig();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoldierPosition>() != null)
        {
            other.GetComponent<SoldierPosition>().Follow(GetComponent<PlayerSoldierManager>(), true);

        }

        if (other.GetComponent<Mine>() != null)
        {
            animator.SetBool("isMining", true);
            pikAxe.SetActive(true);
            _mine = other.GetComponent<Mine>();
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SoldierPosition>() != null)
        {
            other.GetComponent<SoldierPosition>().Follow(GetComponent<PlayerSoldierManager>(), false);

        }

        if (other.GetComponent<Mine>() != null)
        {
            animator.SetBool("isMining", false);
            pikAxe.SetActive(false);
        
        }



    }

    private void OnTriggerStay(Collider other)
    {


    }
}
