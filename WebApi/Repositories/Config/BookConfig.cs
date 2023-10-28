using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "İnce Memed", Price = 75 },
                new Book { Id = 2, Title = "Suç ve Ceza", Price = 175 },
                new Book { Id = 3, Title = "Acımak", Price = 225 }
                );
        }
    }
}
