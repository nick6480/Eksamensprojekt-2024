// Base class (Nedarvning)
class Animal
{
    public string Name { get; set; }
    public Heart Heart { get; set; } // Komposition (Et dyr har et hjerte)

    public Animal(string name)
    {
        Name = name;
        Heart = new Heart(); // Opretter et hjerte til dyret
    }

    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name} makes a sound.");
    }
}