namespace sauceDemo.Base;

public class ItemPrice
{
	private string name;
	private decimal price;

	public ItemPrice(string name, decimal price)
	{
		this.name = name;
		this.price = price;
	}

	public string Name => this.name;
	public decimal Price => this.price;


}

