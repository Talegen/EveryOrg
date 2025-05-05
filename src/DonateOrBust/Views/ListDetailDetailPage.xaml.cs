namespace DonateOrBust.Views;

[QueryProperty(nameof(Item), "Item")]
public partial class ListDetailDetailPage : ContentPage
{
	SampleItem? item;

	public SampleItem? Item
	{
		get { return item; }
		set
		{
			item = value;

			DisplayedTitle.Text = item?.Title;
			DisplayedDescription.Text = item?.Description;
		}
	}

	public ListDetailDetailPage()
	{
		InitializeComponent();
	}
}
