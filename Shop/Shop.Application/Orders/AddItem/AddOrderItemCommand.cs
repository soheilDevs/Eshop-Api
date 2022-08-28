using Common.Application;

namespace Shop.Application.Orders.AddItem;

//public record AddOrderItemCommand(long InventoryId, int Count, long UserId) : IBaseCommand;

public class AddOrderItemCommand : IBaseCommand
{
    public AddOrderItemCommand(long userId, long inventoryId, int count)
    {
        UserId = userId;
        InventoryId = inventoryId;
        Count = count;
    }
    public long UserId { get; internal set; }
    public long InventoryId { get; private set; }
    public int Count { get; private set; }
}