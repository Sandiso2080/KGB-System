namespace KGB_System.Models
{
  public class Debt
  {
    public int Id { get; set; }
    public decimal Capital { get; set; }
    public int Interest { get; set; }
    public decimal Fee { get; set; }
    public int Commision { get; set; }
  }
}
