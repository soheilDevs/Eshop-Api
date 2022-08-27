using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.CommentAgg;

public class Comment:AggregateRoot
{
    public long USerId { get; private set; }
    public long ProductId { get; private set; }
    public string Text { get; private set; }
    public CommentStatus Status { get; private set; }
    public DateTime UpdateDate { get; private set; }

    public Comment(long uSerId, long productId, string text)
    {
        NullOrEmptyDomainDataException.CheckString(text,nameof(text));
        USerId = uSerId;
        ProductId = productId;
        Text = text;
        Status = CommentStatus.Pending;
    }

    public void Edit(string text)
    {
        NullOrEmptyDomainDataException.CheckString(text, nameof(text));
        Text = text;
        UpdateDate = DateTime.Now;
    }

    public void ChangeStatus(CommentStatus status)
    {
        Status=status;
        UpdateDate=DateTime.Now;
    }
}

public enum CommentStatus
{
    Pending,
    Accepted,
    Rejected
}