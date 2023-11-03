using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Persistence.Data.Configurations;
  public class TeamConfiguration : IEntityTypeConfiguration<Team>
  {
     public void Configure(EntityTypeBuilder<Team> builder)
    {
          builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("teams");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            builder.HasMany(e => e.Drivers)
            .WithMany(p => p.Teams)
            .UsingEntity<TeamDriver>(
                j =>{
                 j
                    .HasOne(ep => ep.Driver)
                    .WithMany(e => e.TeamDrivers)
                    .HasForeignKey(ep => ep.IdDriver);
                 j
                    .HasOne(ep => ep.Team)
                    .WithMany(p => p.TeamDrivers)
                    .HasForeignKey(ep => ep.IdTeam);
                
                    j.HasKey(ep => new { ep.IdDriver , ep.IdTeam});
                }
            );
  }
  }