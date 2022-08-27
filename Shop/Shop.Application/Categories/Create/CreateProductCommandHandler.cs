using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Create;

public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
{
    private readonly ICategoryRepository _repository;
    private readonly ICategoryDomainService _domainService;

    public CreateProductCommandHandler(ICategoryRepository repository, ICategoryDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Title, request.Slug, request.SeoData, _domainService);
        await _repository.Add(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}