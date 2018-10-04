using BackSide2.DAO.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackSide2.BL.Mapping
{
    public class PersonMap
    {
        public PersonMap(EntityTypeBuilder<Person> entityBuilder)
        {
            entityBuilder.HasKey(b => b.Id);
            entityBuilder.Property(t => t.UserName).IsRequired();  
            entityBuilder.Property(t => t.Email).IsRequired();  
            entityBuilder.Property(t => t.Password).IsRequired();  
            entityBuilder.Property(t => t.Role).IsRequired();  
            entityBuilder.Property(t => t.FirstName);  
            entityBuilder.Property(t => t.Surname);  
            entityBuilder.Property(t => t.Gender);  
            entityBuilder.Property(t => t.Language);
            entityBuilder.ToTable("Persons");
        }

    }
}
