using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
  [Table("constructions")]
  public class Construction
  {
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("location")]
    public string Location { get; set; }

    [Column("stage")]
    public int Stage { get; set; }

    [Column("category")]
    public string Category { get; set; }

    [Column("details")]
    public string Details { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    private DateTime _startDate;

    [Column("start_date")]
    public DateTime StartDate
    {
      get => _startDate;
      set => _startDate = value.ToUniversalTime(); // Ensure that value is stored in UTC
    }

    // Method to get StartDate in local time if needed
    public DateTime GetStartDateInLocalTime()
    {
      return TimeZoneInfo.ConvertTimeFromUtc(_startDate, TimeZoneInfo.Local);
    }

    // Method to set StartDate from local time
    public void SetStartDateFromLocalTime(DateTime? localTime)
    {
      if (localTime.HasValue)
      {
        _startDate = TimeZoneInfo.ConvertTimeToUtc(localTime.Value);
      }
      else
      {
        _startDate = DateTime.UtcNow;
      }
    }
  }
}