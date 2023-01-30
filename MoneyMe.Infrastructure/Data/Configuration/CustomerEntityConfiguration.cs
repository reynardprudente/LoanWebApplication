using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMe.Infrastructure.Data.Configuration
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
            .HasMaxLength(100);

            builder.Property(x => x.LastName)
            .HasMaxLength(100);

        }
    }
}
