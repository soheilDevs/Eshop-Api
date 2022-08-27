using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Categories.Create;

public record CreateProductCommand(string Title, string Slug, SeoData SeoData) : IBaseCommand;