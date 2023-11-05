namespace KGB_System.Models
{
  public class Payment
  {
    public int Id { get; set; }
    public int DebtId { get; set; }
    public DateTime? Date { get; set; }
    public decimal Amount { get; set; }
  }
}
