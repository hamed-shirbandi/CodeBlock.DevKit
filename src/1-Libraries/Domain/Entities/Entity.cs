using CodeBlock.DevKit.Domain.ValueObjects;
using MongoDB.Bson;

namespace CodeBlock.DevKit.Domain.Entities;

/// <summary>
///
/// </summary>
public abstract class Entity
{
    public Entity()
    {
        SetId(ObjectId.GenerateNewId().ToString());

        CreationTime = CreationTime.CreateNowDateTime();
    }

    public string Id { get; private set; }

    public CreationTime CreationTime { get; private set; }

    protected void SetId(string id)
    {
        Id = id;
    }

    protected void Update()
    {
        CreationTime = CreationTime.UpdateModifiedDateTime();
    }
}
