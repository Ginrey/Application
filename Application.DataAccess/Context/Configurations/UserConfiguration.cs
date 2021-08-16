using Application.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.DataAccess.Context.Configurations
{
    public sealed class UserConfiguration: IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(d => d.UserId);
            builder.Property(p => p.UserId)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Login)
                .HasMaxLength(20)
                .IsRequired();
            
            builder.Property(d => d.Name)
                .HasMaxLength(30); 
            
            builder.Property(d => d.Surname)
                .HasMaxLength(30); 
            
            builder.Property(d => d.LastName)
                .HasMaxLength(30);
            
            builder.Property(d => d.Email)
                .HasMaxLength(50);
            
            builder.Property(d => d.Phone)
                .HasMaxLength(20);
        }
    }
}