﻿using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.ValueObject;

namespace Shop.Domain.OrderAgg;

public class Order:AggregateRoot
{
    private Order()                   //This Is For EF Core
    {
        
    }
    public Order(long userId)
    {
        UserId=userId;
        Status = OrderStatus.Pending;
        Items = new List<OrderItem>();
    }
    public long UserId { get;private set; }
    public OrderStatus Status { get; private set; }
    public OrderDiscount? Discount { get; private set; }
    public OrderAddress? Address { get; private set; }
    public ShippingMethod? ShippingMethod { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public DateTime? LastUpdate { get; set; }

    public int TotalPrice
    {
        get
        {
            var totalPrice = Items.Sum(f => f.TotalPrice);
            if (ShippingMethod != null)
                totalPrice += ShippingMethod.ShippingCost;

            if (Discount != null)
                totalPrice -= Discount.DiscountAmount;

            return totalPrice;
        }
    }

    public int ItemCount => Items.Count;

    public void AddItem(OrderItem item)
    {
        ChangeOrderGuard();
        var oldItem = Items.FirstOrDefault(f => f.InventoryId == item.InventoryId);
        if (oldItem != null)
        {
            oldItem.ChangeCount(item.Count+oldItem.Count);
            return;
        }
        Items.Add(item);    
    }
    public void RemoveItem(long itemId)
    {
        ChangeOrderGuard();

        var current = Items.FirstOrDefault(f => f.Id == itemId);
        if(current!=null)
            Items.Remove(current);
    }
    public void ChangeCountItem(long itemId,int newCount)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.ChangeCount(newCount);
    }

    public void ChangeStatus(OrderStatus status)
    {
        Status=status;
        LastUpdate=DateTime.Now;
    }

    public void CheckOut(OrderAddress orderAddress)
    {
        ChangeOrderGuard();

        Address = orderAddress;

    }

    public void ChangeOrderGuard()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidDomainDataException("امکان ثبت محصول در این سفارش وجود ندارد");
    }
}