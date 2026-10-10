// En klass som står för ett inlägg i loggboken
public class GuestbookEntry
{
    // Namn på personen som skriver inlägget
    public string Name { get; set;}

    // Texten som personen skriver
    public string Text {get; set;}

    // Konstruktorn tar emot namn och text när ett inlägg skapas
    public GuestbookEntry(string name, string text)
    {
        Name = name;
        Text = text;
    }
}