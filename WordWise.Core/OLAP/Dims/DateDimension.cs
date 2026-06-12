using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordWise.Core.OLAP.Dims;

[Table("DateDimension")]
public sealed class DateDimension
{
    [Key]
    [Column(TypeName = "date")]
    public DateOnly GregorianDate { get; set; }

    public short GregorianYear { get; set; }
    public byte GregorianMonth { get; set; }
    public byte GregorianQuarter { get; set; }
    public byte GregorianDayOfMonth { get; set; }
    public short GregorianDayOfYear { get; set; }
    public byte GregorianDayOfWeek { get; set; }
    public byte GregorianWeekOfYear { get; set; }

    [Column(TypeName = "char(7)")]
    public string? GregorianYearMonthString { get; set; }

    public short JalaliYear { get; set; }
    public byte JalaliMonth { get; set; }
    public byte JalaliQuarter { get; set; }
    public byte JalaliDayOfMonth { get; set; }
    public short JalaliDayOfYear { get; set; }
    public byte JalaliDayOfWeek { get; set; }

    [Column(TypeName = "char(10)")]
    public string? JalaliFullDateString { get; set; }

    [Column(TypeName = "char(7)")]
    public string? JalaliYearMonthString { get; set; }

    public bool IsWeekend { get; set; }
    public bool IsHoliday { get; set; }
}
