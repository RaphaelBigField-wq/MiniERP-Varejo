using Microsoft.EntityFrameworkCore;

namespace MiniERP.Api.Data;

public class MiniERPContext : DbContext
{
    public MiniERPContext(DbContextOptions<MiniERPContext> options)
        : base(options)
    {
    }
}