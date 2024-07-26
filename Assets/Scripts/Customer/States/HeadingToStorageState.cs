﻿using UnityEngine;
using Supermarket.Customers;
using Hieki.Utils;

public class HeadingToStorageState : CustomerStateBase
{
    [Viewable]
    Vector3 targetPosition;
    public HeadingToStorageState(Customer customer) : base(customer)
    {
        transitions = new CustomerTransition[1]
        {
            new CustomerTransition(typeof(ChoosingState), Reached)
        };
    }

    public override void OnStateEnter()
    {
        if(!Customer.currentStorage)
            Customer.currentStorage = SupermarketManager.Mine.GetAvailableStorage();
        if (!Customer.currentStorage)
            Customer.currentStorage = SupermarketManager.Mine.Storages.PickOne();
        Transform storageTrans = Customer.currentStorage.transform;
        targetPosition = storageTrans.position + storageTrans.forward * .75f + storageTrans.right * Random.Range(-.5f, .5f);
        targetPosition.y = Customer.transform.position.y;
    }

    public override void OnStateUpdate()
    {
        Customer.MoveTowards(targetPosition);

        Vector3 dir = (targetPosition - Transform.position);
        dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        Transform.rotation = Quaternion.Slerp(Transform.rotation, rot, Time.deltaTime * 3.2f);
    }

    bool Reached()
    {
        return (Transform.position - targetPosition).sqrMagnitude <= .1f * .1f;
    }

}
