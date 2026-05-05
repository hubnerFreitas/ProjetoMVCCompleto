using AppMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Logradouro)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Numero)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Complemento)
                .HasMaxLength(250);

            builder.Property(p => p.Cep)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(p => p.Bairro)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(p => p.Cidade)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Estado)
                .IsRequired()
                .HasMaxLength(50);  



            builder.ToTable("Enderecos");
        }
    }
}
