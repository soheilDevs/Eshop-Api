using Common.Domain;
using Shop.Domain.RollAgg.Enum;

namespace Shop.Domain.RollAgg;

public class RolePermission:BaseEntity
{
    public long RoleId { get; set; }
    public Permission Permission { get; set; }
}