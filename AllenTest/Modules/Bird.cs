// Derived class 2 (Nedarvning)
class Bird : Animal
{
    public Bird(string name) : base(name) { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} sings: Tweet Tweet!");
    }
}