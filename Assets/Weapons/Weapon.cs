using UnityEngine;

public abstract class Weapon : MonoBehaviour, ISocketInterface
{

    [SerializeField] string AttachSocketName;
    [SerializeField] AnimatorOverrideController overrideController;
    [SerializeField] float attackAnimSpeedMult = 1f;


    public GameObject Owner
    {
        get;
        private set;
    }
    public void Init(GameObject owner)
    {
        Owner = owner;
        SocketManager socketManager = owner.GetComponent<SocketManager>();
        if (socketManager)
        {
            socketManager.FindAndAttachToSocket(this);
        }
        UnEquip();

    }

    public void Equip()
    {
        gameObject.SetActive(true);
        Animator ownerAnimator = Owner.GetComponent<Animator>();

        if (ownerAnimator && overrideController)
        {
            ownerAnimator.runtimeAnimatorController = overrideController;
            ownerAnimator.SetFloat("AttackSpeedMult", attackAnimSpeedMult);
        }
    }
    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    public string GetSocketName()
    {
        return AttachSocketName;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public abstract void Attack();

}
