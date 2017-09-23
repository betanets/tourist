using Npgsql;

namespace Tourist
{
    public class AbstractTransaction
    {
        public NpgsqlTransaction transaction { get; set; }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }
    }
}
