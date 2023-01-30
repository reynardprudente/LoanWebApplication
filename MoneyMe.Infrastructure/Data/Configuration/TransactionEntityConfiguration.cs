using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMe.Infrastructure.Data.Configuration
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Product)
            .HasMaxLength(20);
        }
    }
}
