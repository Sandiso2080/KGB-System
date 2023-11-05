using KGB_System.Data;
using KGB_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNet.Identity;

namespace KGB_System.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private string _connectionString;
    DbContextOptionsBuilder<ApplicationDbContext> _optionsBuilder;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
      _logger = logger;
      _configuration = configuration;
      _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      _connectionString = _configuration.GetConnectionString("DefaultConnection");
      _optionsBuilder.UseSqlServer(_connectionString);
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
      var user = User.Identity.GetUserId();

      List<Debt> debts = new List<Debt>();

      using (var db = new ApplicationDbContext(_optionsBuilder.Options))
      {
        debts = await db.Debts.ToListAsync();
      }

      return View(debts);
    }

    [Authorize]
    public async Task<IActionResult> Details(int Id)
    {
      List<Payment> payments = new List<Payment>();
      Debt debt = new Debt();

      using (var db = new ApplicationDbContext(_optionsBuilder.Options))
      {
        debt = await db.Debts.SingleAsync(x => x.Id == Id);
        payments = await db.Payments.Where(x => x.DebtId == debt.Id).ToListAsync();
      }

      return View(payments);

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}