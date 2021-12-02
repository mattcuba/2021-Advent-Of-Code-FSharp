module Sequences

open System

// Things we can do with sequences.
let sequences =
    // load up the input lines
    let lines = IO.File.ReadLines @"C:\adventofcode\01\input.txt"
    // the enumerable can then be piped various ways to accomplish some goal
    // make a sequence of integers and total the sequence
    let ints = lines |> Seq.map int
    let total = ints |> Seq.sum
    printfn "The sum of all the integers is %d" total
    // sum the sequence using a function
    let total2 = ints |> Seq.sumBy (fun a -> 2 * a)
    printfn "The sumBy of all integers is %d" total2
    // obviously that function is so simple that we could have just done this
    let multBy2 = total * 2
    printfn "The multBy2 is %d" multBy2
    // here I'm computing the average, mapping the original integer sequence
    // to a sequence of doubles and then averaging the values
    let averageValue = ints |> Seq.map double |> Seq.average 
    printfn "The averageValue is %f" averageValue
