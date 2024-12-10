namespace Karim.ECommerce.Infrastructure.Persistence._Common
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class DbContextTypeAttribute : Attribute
    {
        public Type DbType { get; }
        public DbContextTypeAttribute(Type DbType)
        {
            this.DbType = DbType;
        }
    }
}
