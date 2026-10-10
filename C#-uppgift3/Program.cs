// Använder för att hantera json
using System.Text.Json;
using System.IO;

// En lista som innehåller alla inlägg
List<GuestbookEntry> guestList = new List<GuestbookEntry>();

// Kollar om de finns en sparad json fil
if (File.Exists("guestbook.json"))
{
    // Läser innehållet av filen
    string savedJson = File.ReadAllText("guestbook.json");

    // Omvandlar json innehållet till en lista med inlägg
    guestList = JsonSerializer.Deserialize<List<GuestbookEntry>>(savedJson) ?? new List<GuestbookEntry>();
}

// Håller programmet igång tills använder avslutar
bool isRunning = true;

while (isRunning)
{
    // Visar menyn
    Console.WriteLine("Do you want to add an entry, remove an entry or show all entries?");
    Console.WriteLine("1. add an entry, 2. remove an entry, 3. show all entries, 0. exit");

    // Läser användarens val
    string choice = Console.ReadLine() ?? "";

    // Switch sats som utför olika åtgärder baserat på användarens val
    switch (choice)
    {
        case "0":
            // avslutar programmet
            isRunning = false;
        break;

        case "1":
            // Frågar efter namn och inlägg
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine() ?? "";

            Console.WriteLine("Whats your guestbook entry?");
            string entry = Console.ReadLine() ?? "";

            // Kollar att inget fält e tomt
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(entry))
            {
                Console.WriteLine("Name or entry can not be empty");
            }
            else
            {
                // Skapar nya inlägg och lägger till de i listan
               GuestbookEntry posts = new GuestbookEntry(name: name, text: entry);
               guestList.Add(posts);

                // Omvandlar listan till json och sparar den på fil
               string jsonString = JsonSerializer.Serialize(guestList);
               File.WriteAllText("guestbook.json", jsonString);

               Console.WriteLine("Entry added");
            }
        break;

        case "2":
            // kollar om gästboken saknar något inlägg
            if (guestList.Count == 0)
            {
                Console.WriteLine("there are no entries");
                break;
            }
            
            // visar alla inlägg med sina index
            for (int i = 0; i < guestList.Count; i++)
            {
                Console.WriteLine(i + ". " + guestList[i].Name + ": " + guestList[i].Text);
            }

            // Frågar vilket inlägg som ska tas bort
            Console.WriteLine("Enter the number of the entry you wish to remove");
            
            // Kollar om indexet är ett giltigt heltal i listan
            if (int.TryParse(Console.ReadLine(), out int number) && number >= 0 && number < guestList.Count)
            {
                // tar bort inlägget på valda index
                guestList.RemoveAt(number);

                // Sparar uppdaterade listan i json filen
                string jsonString = JsonSerializer.Serialize(guestList);
                File.WriteAllText("guestbook.json", jsonString);

                Console.WriteLine("Entry removed");
            } else
            {
                // Visar felmeddelande om indexet är ogiltigt
                Console.WriteLine("Enter a valid number");
            }
        break;

        case "3":
            // Kollar om gästboken saknar inlägg
            if (guestList.Count == 0)
            {
                Console.WriteLine("There are no entries");
                break;
            }
            // Visar alla inlägg med index, namn och text
            for (int i = 0; i < guestList.Count; i++)
            {
                Console.WriteLine(i + ". " + guestList[i].Name + ": " + guestList[i].Text);
            }
        break;

        default:
            // Visas bara om användaren väljer något alternativ som ej finns 
            Console.WriteLine("Invalid option, choose 1, 2, 3 or 0");
        break;
    } 
    // Väntar på nån tangent innan skärmen rensas.
    Console.ReadKey();
    Console.Clear();
}


