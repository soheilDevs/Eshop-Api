using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.RollAgg;

public class Role:AggregateRoot
{
    public string Title { get; private set; }
    public List<RolePermission> Permissions { get; private set; }


    private Role()
    {

    }
    public Role(string title, List<RolePermission> rolePermissions)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        Permissions = rolePermissions;
    }
    public Role(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        Permissions = new List<RolePermission>();
    }
    public void Edit(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
    }
    public void SetPermissions(List<RolePermission> permissions)
    {
        Permissions = permissions;
    }
}