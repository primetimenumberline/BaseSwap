//BASE SWAP
//A program to change between number bases, 
//using any symbol mapping you can conceive of as a string
//
//
//
// Sample bases
// { "0", "1" };                                                                            //base 2    binary standard encoding
// { "0", "1", "2" };                                                                       //base 3
// { "0", "1", "2", "3", "4", "5", "6" };                                                   //base 7
// { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };                                    //base 10   decimal standard encoding
// { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };      //base 16   HEX
// { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };      //base 16   hex (hex!!!)
// { "*", ".", ":", ".:", "::", ".::", ":::" };                                             //base 7    broken encoding (symbols must be one char long, and must be unique)
//
//
//
//
// Sample declarations
// string[] input_base = { "0", "1", "2" };                          //base 3
// string[] output_base = { "0", "1", "2", "3", "4", "5", "6" };     //base 7
// string[] input_number = { "1", "1", "2", "0", "1" };              //11201 base 3, aka 127 base 10 should compute to 241 base 7
//
// string[] input_base = { "0", "1", "2", "3", "4", "5", "6" };          //base 7
// string[] output_base = { "*", ".", ":", ".:", "::", ".::", ":::" };   //base 7 broken
// string[] input_number = { "1", "1", "2", "0", "1" };                  //11201 base 7, aka 127 base 10 should compute to ..:*. base 7 broken
//
// string[] input_base = { "0", "1" };                                               //base 2
// string[] output_base = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };      //base 10
// string[] input_number = { "1", "0", "1" };                                        //101 base 2, aka 5 base 10 should compute to 5 base 10






string[] input_base = { "0", "1", "2" };                          //base 3
string[] output_base = { "0", "1", "2", "3", "4", "5", "6" };     //base 7
string[] input_number = { "1", "1", "2", "0", "1" };              //11201 base 3, aka 127 base 10 should compute to 241 base 7

//Another approach, not shown here, is:
//
//string decimal_number = ConverToDecimal(input_number, input_base);
//string output = ConvertFromDecimal(decimal_number, output_base);
//
//by using decimal as an intermediary value, we would not have to 
//build our own arithmetic functions such  as add() and mul()
//since support for these operations already exists in modern languages

string input = string.Join("", input_number);
string output = processNumber(ref input_number);

Console.WriteLine(input + " in base " + input_base.Length + " is");
Console.WriteLine(output + " in base " + output_base.Length);





string processNumber(ref string[] input_number)
{
    string result = "Sorry, not right now";
    if (input_base.Length == output_base.Length)
    {   
        //just symbol swap and return
        //we are not changing number base here
        //we are only changing the encoding of the numbers
        //aka their symbol mapping
        symbolSwap(ref input_number);
        result = String.Join("", input_number);
    }
    else if (input_base.Length < output_base.Length)
    {
        //in this scenario, the number base we are changing to uses more characters to represent the whole numbers
        //here, we just lookup mapping in output table since output table > input table, symbol will always map
        symbolSwap(ref input_number);


        //next, we note how numbers are formed:
        // Let's look at the number 11201 in base 3
        // which we will represent as (11201),3
        // 
        // (11201),3 = ( ),7
        //
        // 3^4 3^3 3^2 3^1 3^0
        //   1   1   2   0   1
        //
        // The "amount" of number we have in any given base
        // can be computed as per the following algorithm:
        //
        // begin_psuedocode
        // ans = 0;
        // ans = ( 1 * 3^4 ) + ans;
        // ans = ( 1 * 3^3 ) + ans;
        // ans = ( 2 * 3^2 ) + ans;
        // ans = ( 0 * 3^1 ) + ans;
        // ans = ( 1 * 3^0 ) + ans;
        // end_psuedocode
        //
        // or, otherwise stated:
        //
        // begin_pseudocode
        // ans = 1;
        // ans = ( ans * 3 ) + 1;
        // ans = ( ans * 3 ) + 2;
        // ans = ( ans * 3 ) + 0;
        // ans = ( ans * 3) + 1
        // end_pseudocode
        //
        // first step in converting to new base, is to
        // represent input number in input base, for this example, base 3
        // ((((1*10 + 1)10 + 2)10 + 0)10 + 1)
        //
        // since arithmetic works in any base
        // we simply convert each number to the new base
        // and our above equation will still hold true
        // the magic of aritmetic and math
        //
        // so the next step is to
        // map equation to the output base, for this example, base 7
        // ((((1*3 + 1)3 + 2)3 + 0)3 + 1)
        //
        // note that nothing changed in the equation, but if we would have been using broken notation,
        // we would have gotten:
        // ((((. mul .: add .) mul .: add :) mul .: add * ) mul .: add .)
        //
        // lastly, to determine the number in the new base,
        // we actually process each computation in the equation, in the new base
        //
        // ans = 1
        // ans = ans * 3 + 1 = (1*3) + 1 = 3 + 1 = 4
        // ans = ans * 3 + 2 = (4*3) + 2 = 15 + 2 = 20
        // ans = ans * 3 + 0 = (20*3) + 0 = 60 + 0 = 60
        // ans = ans * 3 + 1 = (60*3) + 1 = 240 + 1 = 241
        //
        // So finally, we have that
        // (11201),3 = (241),7

        string[,] table_addition = buildAdditionTable(output_base);
        string[,] table_multiplication = buildMultiplicationTable(output_base);
        

        //perform processing

        result = String.Join("", input_number);
    }
    else
    {
        //shitty, have to compute what the base looks like in the smaller system
    }
    return result;

}

string[,] buildMultiplicationTable(string[] number_base)
{
    //NOTE ASSUMPTION: number_base symbols must only be 1 char in length,
    //and number_base symbols must all be unique from each other
    //
    //
    // Example
    // The base 7 multiplication table is given below
    // Note for example, that 3*3 is still nine, but nine looks like 12 not 9
    // in base 7, so here is a quick ref of the table we are building
    // as we continue with the example of (11021),3 to ( ),7 so please enjoy:
    //
    //     0   1   2   3   4   5   6
    //  0  0
    //  1  0   1
    //  2  0   2   4
    //  3  0   3   6   12
    //  4  0   4  11   15  22
    //  5  0   5  13   21  26  34
    //  6  0   6  15   24  33  42  51
    //
    //
    // so (3),7 mul (3),7 eql (12),7
    // and (5),7 mul (2),7 eql (13),7 
    // and zero mul anything is still zero base seven
    // etc. etc. etc.
    //
    //
    // or if we enter the matrix and use this symbols set
    // for base 7 { "*", ".", ":", ".:", "::", ".::", ":::" } then
    // we will get the following multiplication table
    //
    //              *        .        :       .:       ::      .::      :::
    //      *       *
    //      .       *        .
    //      :       *        :       ::
    //     .:       *       .:      :::       .:
    //     ::       *       ::       ..     ..::       ::
    //    .::       *      .::      ..:       :.     ::::     .:::
    //    :::       *      :::     ..::      :::     .:.:      :::     .::.
    //
    //
    // Note: this above encoding is broken because parsing is impossible without encountering ambiguity
    // When building a number system, the symbols selected for the map, must not combine to form other
    // pre-existing symbols in the map, else this will cause lookup collisions and ambiguity
    //
    // for example, in the prior table, it was clear that three mul six eql eightteen, or "two-four" in base seven
    // (3),7 mul (6),7 eql (24),7
    //
    // and we can also see that five mul six eql thirty, or "four-two" in base seven
    // (5),7 mul (6),7 eql (42),7 
    //
    // and finally we can detect that
    // (2),7 mul (3),7 eql (6),7
    //
    // but when we move to the broken encoding, there is ambiguity
    // (.:),7 mul (:::),7 eql (:::),7
    // (.::),7 mul (:::),7 eql (:::),7
    // (:),7 mul (.:),7 eql (:::),7
    //
    // It is therefore critical to ensure that when selecting symbols for a number system,
    // that those symbols do not combine to form ambiguous representations
    // What number am I talking about when I say (:::),7
    // You'll never know for sure
    // Parsing becomes impossible under this system
    // Special delimiters would be needed to delimit every symbol accurately
    //
    // In conclusion: to avoid a broken number system,
    // base symbols must remain unique and can NEVER be combinations of other base symbols

    string[,] table = new string[number_base.Length, number_base.Length];

    //for multiplication table, we will use the add
    //function repeatedly, and we will cheat a bit
    //by pre-filling the zero index with zero's
    //since anything times zero is zero
    //and we will use a similar trick for the one's
    //then we will call add function for the rest

    for (int i = 0; i < table.GetLength(0); i++)
    {
        table[0, i] = number_base[0];
        table[i, 0] = number_base[0];
    }
    for (int i = 1; i < table.GetLength(0); i++)
    {
        table[1, i] = number_base[i];
        table[i, 1] = number_base[i];
    }

    //TODO

    printTable(table);
    return table;
}

string[,] buildAdditionTable(string[] number_base)
{
    //NOTE ASSUMPTION: number_base symbols must only be 1 char in length,
    //and number_base symbols must all be unique from each other
    //
    //
    // Example
    // The base 7 addition table is given below
    // Note for example, that 4+4 is still eigth, but eigth looks like 11 not 8
    // in base 7, so here is a quick ref of the table we are building
    // as we continue with the example of (11021),3 to ( ),7 so please enjoy:
    //
    //     0   1   2   3   4   5   6
    //  0  0
    //  1  1   2
    //  2  2   3   4
    //  3  3   4   5   6
    //  4  4   5   6  10  11
    //  5  5   6  10  11  12  13
    //  6  6  10  11  12  13  14  15
    //
    //
    // so (3),7 add (3),7 eql (6),7
    // and (5),7 add (2),7 eql (10),7 
    // and zero add anything is still that same number, base seven
    // etc. etc. etc.
    //
    //
    // or if we enter the matrix and use this symbols set
    // for base 7 { "*", ".", ":", ".:", "::", ".::", ":::" } then
    // we will get the following multiplication table
    //
    //              *        .        :       .:       ::      .::      :::
    //      *       *
    //      .       .        :
    //      :       :       .:       ::
    //     .:      .:       ::      .::      :::
    //     ::      ::      .::      :::       .*       ..
    //    .::     .::      :::       .*       ..       .:      ..:
    //    :::     :::       .*       ..       .:      ..:      .::     ..::
    //
    //
    //
    // Note: this above encoding is broken because parsing is impossible without encountering ambiguity
    // When building a number system, the symbols selected for the map, must not combine to form other
    // pre-existing symbols in the map, else this will cause lookup collisions and ambiguity
    //
    // for example, in the prior table, it was clear that zero add five eql five
    // (0),7 add (5),7 eql (5),7
    //
    // and we can also see that five mul six eql thirty, or "four-two" in base seven
    // (2),7 add (3),7 eql (5),7 
    //
    // and finally we can detect that
    // (5),7 add (6),7 eql (14),7
    //
    // but when we move to the broken encoding, there is ambiguity
    // (*),7 add (.::),7 eql (.::),7
    // (:),7 add (.:),7 eql (.::),7
    // (.::),7 mul (:::),7 eql (.::),7
    //
    // It is therefore critical to ensure that when selecting symbols for a number system,
    // that those symbols do not combine to form ambiguous representations
    // What number am I talking about when I say (.::),7
    // You'll never know for sure
    // Parsing becomes impossible under this system
    // Special delimiters would be needed to delimit every symbol accurately
    //
    // In conclusion: to avoid a broken number system,
    // base symbols must remain unique and can NEVER be combinations of other base symbols

    string[,] table = new string[number_base.Length,number_base.Length];

    //starting with the first symbol in our number base
    string[] sym = { number_base[0] };

    //begin filling the addition table
    //there are two options for filling the table
    //
    //Option 1
    //Work diagonally, noting the following simple pattern:
    //
    //Symbol 0 fills diagonal entries: [0,0]
    //Symbol 1 fills diagonal entries: [0,1] [1,0]
    //Symbol 2 fills diagonal entries: [0,2] [1,1] [2,0]
    //Symbol 3 fills diagonal entries: [0,3] [1,2] [2,1] [3,0]
    //Symbol 4 fills diagonal entries: [0,4] [1,3] [2,2] [3,1] [4,0]
    //Symbol 5 fills diagonal entries: [0,5] [1,4] [2,3] [3,2] [4,1] [5,0]
    //Symbol 6 fills diagonal entries: [0,6] [1,5] [2,4] [3,3] [4,2] [5,1] [6,0]
    //Symbol 7 fills diagonal entries:       [1,6] [2,5] [3,4] [4,3] [5,2] [6,1]
    //Symbol 8 fills diagonal entries:             [2,6] [3,5] [4,4] [5,3] [6,2]
    //Symbol 9 fills diagonal entries:                   [3,6] [4,5] [5,4] [6,3]
    //Symbol A fills diagonal entries:                         [4,6] [5,5] [6,4]
    //Symbol B fills diagonal entries:                               [5,6] [6,5]
    //Symbol C fills diagonal entries:                                     [6,6]
    //
    //Option 2
    //Copy the symbol map directly into first column, 
    //since anything added to zero is just itself,
    //and then with each subsequent column we construct, just copy
    //the data from the previous column, shifting up by one, and
    //being sure to update the last entry in the column,
    //which is the only new number as compared to the previous column
    //
    //      0
    //      1   2
    //      2   3   4
    //      3   4   5   6
    //      4   5   6  10  11
    //      5   6  10  11  12  13
    //      6  10  11  12  13  14  15
    //
    //with this method, don't forget to also fill
    //the symmetric location about the diagonal, while working
    //
    //
    //When completed, full table should look like this, no matter your approach:
    //
    //      0    1    2    3    4    5    6
    //      1    2    3    4    5    6   10
    //      2    3    4    5    6   10   11
    //      3    4    5    6   10   11   12
    //      4    5    6   10   11   12   13
    //      5    6   10   11   12   13   14
    //      6   10   11   12   13   14   15

    int symbol = 0;
    int total = (number_base.Length - 1) * 2;
    while (symbol <= total)
    {
        string number = string.Join("", sym);
        int i = 0;
        while (i <= symbol/2)
        {
            int j = symbol - i;

            if ( i < table.GetLength(0) &&
                 j < table.GetLength(1) &&
                 i + j == symbol )
            {
                table[i, j] = number;
            }
            if (j < table.GetLength(0) &&
                i < table.GetLength(1) &&
                i + j == symbol)
            {
                table[j, i] = number;
            }
            i++;
        }

        inc(ref sym, number_base);
        symbol++;
    }

    printTable(table);
    return table;
}

void printTable(string[,] table)
{
    Console.WriteLine("Begin Table");
    for (int i = 0; i < table.GetLength(0); i++)
    {
        for (int j = 0; j < table.GetLength(1); j++)
        {
            Console.Write(table[i, j] + "\t");
        }
        Console.WriteLine();
    }
    Console.WriteLine("End Table");
}

string[] add(string[] a, string[] b, string[,] table_addition)
{
    int max = a.Length > b.Length ? a.Length : b.Length;
    string[] c = new string[max];
    string ci = table_addition[0, 0];
    string co = table_addition[0, 0];

    //note that numbers are stored in reverse order in the array
    //such that the most sig symbol resides in index length-1
    //and least sig symbol resides in index 0
    //thus addition must start at length-1 and iterate down the line
    //from least sig to most sig symbol
    //so for two sizes of numbers, we are solving the following
    //
    //   eg.
    //            0  1  2  3  4  5  6  7  8  9
    //     a     [] [] [] [] [] [] [] [] [] []     Most Sig Symbol on RHS
    //     b     [] [] [] []                       Most Sig Symbol on RHS
    //
    //    a[9] + b[3] = c[9] + carry out, carry in not shown
    //    a[8] + b[2] = c[8] + carry out, carry in not shown
    //    a[7] + b[1] = c[7] + carry out, carry in not shown
    //    a[6] + b[0] = c[6] + carry out, carry in not shown
    //    a[5] + ci   = c[5] + carry out
    //    a[4] + ci   = c[4] + co
    //    a[3] + ci   = c[3] + co
    //    a[2] + ci   = c[2] + co
    //    a[1] + ci   = c[1] + co
    //    a[0] + ci   = c[0] + co
    //           ci   = c[-1] aka resize array if needed, to add new digit
    //
    //numbers could have also been stored with their least sig digit at 0
    //and with their most sig digit at length-1, which would have made 
    //implementation a bit easier, but I went this way and I'm committed now
    //note that with the other endianess, the overhead would come
    //in printing the values to the console, but that should be easy to manage

    //TODO

    return c;
}

string[] mul(string[] a, string[] b, string[,] table_mulitiplication)
{
    //TODO
    string[] result = { };
    return result;
}

void inc(ref string[] sym, string[] map)
{
    int index = sym.Length - 1;
    bool done = false;

    while (!done)
    {
        //check current symbol and determine its position in our global map/list
        int loc = location(sym[index], map);

        //if we are at the last symbol in our base, increment will cause rollover
        if (loc == map.Length - 1)
        {
            sym[index] = map[0];
        }
        //if we are not at the last symbol, take next symbol
        else
        {
            sym[index] = map[loc + 1];
            done = true;
        }
        index--;

        //if final position in array is reached and we are not done,
        //then we must add new digit to continue counting
        if ((index == -1) && (!done))
        {
            Array.Resize(ref sym, sym.Length + 1);
            for (int i = sym.Length - 1; i > 0; i--)
            {
                sym[i] = sym[i - 1];
            }
            sym[0] = map[1];
            done = true;
        }
    }
}

void symbolSwap(ref string[] input_number)
{
    for (int i = 0; i < input_number.Length; i++)
    {
        int loc = location(input_number[i], input_base);
        if (loc < 0)
        {
            Console.WriteLine("Error -1");
            break;
        }
        else
        {
            input_number[i] = output_base[loc];
        }
    }
}

int location(string find, string[] map)
{
    for (int i = 0; i < map.Length; i++)
    {
        if (find == map[i])
        {
            return i;
        }
    }
    return -1;
}