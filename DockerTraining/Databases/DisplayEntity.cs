namespace DockerTraining.Databases;

internal class DisplayEntity
{
    public int DisplayId { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}
