//BASE SWAP
//A program to change between number bases, 
//using any symbol mapping you can conceive of as a string
//
//
//
// begin_example_symbol_mappings
//
// { "0", "1" };                                                                            //base 2    binary standard encoding
// { "0", "1", "2" };                                                                       //base 3
// { "0", "1", "2", "3", "4", "5", "6" };                                                   //base 7
// { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };                                    //base 10   decimal standard encoding
// { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };      //base 16   HEX
// { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };      //base 16   hex (hex!!!)
// { "*", ".", ":", ".:", "::", ".::", ":::" };                                             //base 7    broken encoding (note that dots were used for counting in prehistoric times, prior to inventing arithmetic)
//
// end_example_symbol_mappings
//
//
//
//begin_example_declarations
//
//string[] input_base = { "0", "1", "2" };                          //base 3
//string[] output_base = { "0", "1", "2", "3", "4", "5", "6" };     //base 7
//string[] input_number = { "1", "1", "2", "0", "1" };              //11201 base 3, aka 127 base 10 should compute to 241 base 7
//
//string[] input_base = { "0", "1", "2", "3", "4", "5", "6" };          //base 7
//string[] output_base = { "*", ".", ":", ".:", "::", ".::", ":::" };   //base 7 broken
//string[] input_number = { "1", "1", "2", "0", "1" };                  //11201 base 7, aka 127 base 10 should compute to ..:*. base 7 broken
//
//string[] input_base = { "0", "1" };                                               //base 2
//string[] output_base = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };      //base 10
//string[] input_number = { "1", "0", "1" };                                        //101 base 2, aka 5 base 10 should compute to 5 base 10
//
//end_example_declarations






string[] input_base = { "0", "1", "2", "3", "4", "5", "6" };          //base 7
string[] output_base = { "*", ".", ":", ".:", "::", ".::", ":::" };   //base 7 broken
string[] input_number = { "1", "1", "2", "0", "1" };                  //11201 base 7, aka 127 base 10 should compute to ..:*. base 7 broken

//Another approach, not shown here, is:
//string decimal_number = ConverToDecimal(input_number, input_base);
//string output = ConvertFromDecimal(decimal_number, output_base);

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
        // ((((1*3 + 1)3 + 2)3 + 0)3 + 1)
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


        string[,] table_multiplication = buildMultiplicationTable(output_base);
        string[,] table_addition = buildAdditionTable(output_base);

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
    //TODO 
    //
    //
    // Base 7 multiplication table for symbol lookup, below
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
    return table;
}

string[,] buildAdditionTable(string[] number_base)
{
    //TODO
    //
    //
    // Base 7 addition table for symbol lookup, below
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
    //    .::     .::      :::       .*       ..      ..:      ..:
    //    :::     :::       .*       ..      ..:      .::      .::     ..::
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
    return table;
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