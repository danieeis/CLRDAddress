using System;
using System.Collections.Generic;
using System.Text;

namespace CLRDGAddress.GDAddress.JsonService
{
    public interface ISerializer
    {
        string Serialize<TEntity>(TEntity entity) where TEntity : class, new();
        TEntity Deserialize<TEntity>(string entity) where TEntity : class, new();
    }
}
