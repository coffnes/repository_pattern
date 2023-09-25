namespace RepoTask.DataAccessLayer;

public abstract class TemperatureEntity<T> : Entity<T>, IEquatable<TemperatureEntity<T>>
{
    public int TemperatureC { get; set; }
    public long Date { get; set; }
    public string? City { get; set; }

    public bool Equals(TemperatureEntity<T> other)
    {
        if (other is null)
            return false;

        return this.TemperatureC == other.TemperatureC && this.Date == other.Date && this.City == other.City;
    }

    public override bool Equals(object obj) => Equals(obj as TemperatureEntity<T>);
    public override int GetHashCode() => Id.Timestamp.GetHashCode();
}