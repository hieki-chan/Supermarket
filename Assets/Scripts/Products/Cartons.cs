﻿using Supermarket;
using Supermarket.Player;
using Supermarket.Customers;
using Supermarket.Products;
using System.Collections.Generic;
using UnityEngine;

public class Cartons : Interactable, IInteractButton01, IInteractButton02
{
    public static SimplePool<Cartons> Pool;
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()  //domain reloading
    {
        Pool = null;
    }
#endif

    public List<ProductOnSale> ProductInBox;

    public ArrangementGrid grid;

    [Header("Pick Up")]
    [SerializeField] private Vector3 handHoldPos;


    private const float PICK_UP_SPEED = 2;
    private const float THROW_FORCE = 500;
    private const float THROW_FORCE_UP = 500;

    Rigidbody rb;
    Collider col;

    Storage storage;
    Transform tier;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public override void OnInteract(PlayerController targetPlayer)
    {
        PickUp(targetPlayer.cameraTrans);
    }

    public override void OnHoverOther(Interactable other)
    {
        if (other is not Storage storage)
        {
            return;
        }
        if ((transform.position - storage.transform.position).sqrMagnitude >= 4)
            return;
        this.storage = storage;
    }

    public override void OnHoverOther(Collider collider)
    {
        tier = collider.transform;
    }

    public override void OnHoverOtherExit()
    {
        storage = null;
    }

    public override void OnInteractExit()
    {
        col.enabled = true;
        rb.isKinematic = false;
    }

    private void PickUp(Transform target)
    {
        transform.parent = target;
        transform.localPosition = handHoldPos;
        transform.localRotation = Quaternion.identity;

        col.enabled = false;
        rb.isKinematic = true;
    }

    public void Throw()
    {
        transform.parent = null;
        col.enabled = true;
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward * (THROW_FORCE * Time.deltaTime), ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.up * (THROW_FORCE_UP * Time.deltaTime), ForceMode.Impulse);

        OnNoInteraction?.Invoke(this);
    }

    public void PlaceItem()
    {
        ProductOnSale item = GetItem();

        if(storage.TryArrangeProduct(item, tier))
        {
            GetItemOut();
        }
    }

    public void PackItem(ProductOnSale product, int count)
    {
        for (int i = 0; i < count; i++)
        {
            ProductOnSale p = ProductOnSale.Pool.GetOrCreate(product.name, product, transform.position, Quaternion.identity);

            grid.Push(p);
        }
    }

    public ProductOnSale GetItem()
    {
        return grid.Peek();
    }

    public ProductOnSale GetItemOut()
    {
        return grid.Pop();
    }


    //BUTTONS

    public string GetButtonTitle01()
    {
        return "Place";
    }
    public bool GetButtonState01()
    {
        return storage && grid.Count > 0;
    }
    public void OnClick_Button01()
    {
        PlaceItem();
    }


    public string GetButtonTitle02()
    {
        return "Throw";
    }
    public bool GetButtonState02()
    {
        return true;
    }
    public void OnClick_Button02()
    {
        Throw();
    }


    private void OnDrawGizmosSelected()
    {
        grid.OnDrawGizmo();

        Gizmos.DrawWireSphere(transform.position + handHoldPos, .5f);
    }
}
