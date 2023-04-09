using Issues.Manager.Application.Contracts;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Infrastructure.DBConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Issues.Manager.Infrastructure;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IAuthenticationStateService _userIdProvider;


	public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor, IAuthenticationStateService userIdProvider) : base(options)
	{
		_httpContextAccessor = httpContextAccessor;
		_userIdProvider = userIdProvider;
	}

	public DbSet<TaskEntity> Tickets { get; set; }

	public DbSet<Comment> Comments { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new RoleConfiguration());
		modelBuilder.Entity<TaskEntity>().HasQueryFilter(x => x.UserId == _userIdProvider.GetCurrentUserId());
		modelBuilder.Entity<Comment>().HasQueryFilter(x => x.UserId == _userIdProvider.GetCurrentUserId());
		modelBuilder.SeedDb();
	}

}