using UnityEngine;

public abstract class Weapon : MonoBehaviour, ISocketInterface
{

    [SerializeField] string AttachSocketName;

    public GameObject Owner
    {
        get;
        private set;
    }
    public void Init(GameObject owner)
    {
        Owner = owner;
    }

    public void Equip()
    {
        gameObject.SetActive(true);
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

}
