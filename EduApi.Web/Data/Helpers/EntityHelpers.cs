using System;
using EduApi.Web.Data.Models;

namespace EduApi.Web.Data.Helpers
{
    public static class EntityHelpers
    {
        public static void SetInsertAuditFields(this BaseEntity entity)
        {
            entity.CreatedDateTime = DateTime.UtcNow;
        }

        public static void SetUpdateAuditFields(this BaseEntity entity)
        {
            entity.LastUpdatedDateTime = DateTime.UtcNow;
        }
    }
}