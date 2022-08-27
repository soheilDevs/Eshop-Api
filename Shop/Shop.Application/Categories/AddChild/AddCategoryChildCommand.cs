using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Categories.AddChild;

public record AddCategoryChildCommand(long ParentId,string Title, string Slug, SeoData SeoData) : IBaseCommand;