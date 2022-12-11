var input = args.FirstOrDefault() ?? "Hello, World!";

var miniFuckString = LettersToMiniFuckString(input);

// check solution with the reverse implementation from wiki
var letters = MiniFuckStringToLetters(miniFuckString);

var assert = input == letters;

Console.WriteLine(assert ? miniFuckString : "Invalid implementation");

string LettersToMiniFuckString(string code)
{
    var instructions = new List<string>();
    var position = 0;
    foreach (var c in code)
    {
        // reset position to zero by incrementing memory to 256
        if (position > c)
        {
            var toZero = 256 - position;
            instructions.AddRange(GetMemoryIncrements(toZero));
            position = 0;
        }

        var positionDelta = c - position;
        instructions.AddRange(GetMemoryIncrements(positionDelta));
        position += positionDelta;

        instructions.Add(".");
    }

    return string.Join("", instructions);

    IEnumerable<string> GetMemoryIncrements(int size)
    {
        return Enumerable.Repeat("+", size);
    }
}

// Copy pasta from https://esolangs.org/wiki/MiniStringFuck#Implementation
string MiniFuckStringToLetters(string code)
{
    var result = "";
    code = code.Replace(((char)13).ToString(), "");

    var cell = 0;

    foreach (var c in code)
    {
        if (c == '+')
        {
            if (cell == 255)
                cell = 0;
            else
                cell++;
        }
        else if (c == '.') result += (char)cell;
    }

    return result;
}