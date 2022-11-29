namespace Domain;

public class Product
{
    public string Title { get; private set; }
    public int Cost { get; private set; }

    public Product(string title, int cost)
    {
        Title = title;
        Cost = cost;
    }
}