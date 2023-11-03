using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Persistence.Data.Configurations;
  public class TeamDriverConfiguration : IEntityTypeConfiguration<TeamDriver>
  {
     public void Configure(EntityTypeBuilder<TeamDriver> builder)
    {
  }
  }