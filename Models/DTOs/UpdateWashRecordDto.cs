using OptiWash.Models.Enums;

public class UpdateWashRecordDto
{
    public DateTime WashDate { get; set; }
    public bool InteriorCleaned { get; set; }
    public bool ExteriorCleaned { get; set; }
    public WashStatus Status { get; set; }
    public string Notes { get; set; }
}
